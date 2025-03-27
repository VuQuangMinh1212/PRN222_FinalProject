using MedicalSearchingPlatform.Business.Interfaces;
using MedicalSearchingPlatform.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using System.Security.Policy;

public class SignInModel : PageModel
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly IUserService _userService;

    public SignInModel(SignInManager<User> signInManager, UserManager<User> userManager, IUserService userService)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _userService = userService;
    }

    [BindProperty]
    public InputModel Input { get; set; } = new InputModel();

    public class InputModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public void OnGet() { }

    public async Task<IActionResult> OnPostLogoutAsync()
    {
        await _signInManager.SignOutAsync();
        return RedirectToPage("/Index");
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var user = await _userManager.FindByEmailAsync(Input.Email);

        if (user == null)
        {
            TempData["DeactivatedMessage"] = "Invalid login attempt.";
            return RedirectToPage("/Index");
        }

        if (!user.IsActive)
        {
            TempData["DeactivatedMessage"] = "Your account is deactivated. Please contact support.";
            return RedirectToPage("/Index");
        }

        var isPasswordValid = await _userManager.CheckPasswordAsync(user, Input.Password);
        if (!isPasswordValid)
        {
            TempData["DeactivatedMessage"] = "Invalid password";
            return RedirectToPage("/Index");
        }

        var result = await _signInManager.PasswordSignInAsync(user.UserName, Input.Password, false, false);

        if (result.Succeeded)
        {
            var userName = user.FullName ?? user.UserName;
            var roles = await _userManager.GetRolesAsync(user);
            var userRole = user.Role;
            Debug.WriteLine("------------------------------------------------" + userRole);
            HttpContext.Session.SetString("UserName", userName);
            HttpContext.Session.SetString("UserRole", userRole);

            return RedirectToPage("/Index");
        }

        TempData["DeactivatedMessage"] = "Invalid login attempt.";
        return RedirectToPage("/Index");
    }

    public IActionResult OnPostGoogleLoginAsync(string returnUrl = null)
    {
        Debug.WriteLine("Google login button clicked!");
        returnUrl ??= Url.Page("/Index");
        var redirectUrl = Url.Page("/Account/SignIn", pageHandler: "GoogleCallback", values: new { returnUrl });
        Debug.WriteLine($"Redirect URL: {redirectUrl}");
        var properties = _signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
        return new ChallengeResult("Google", properties);
    }

    public async Task<IActionResult> OnGetGoogleCallbackAsync(string returnUrl = null, string remoteError = null)
    {
        Debug.WriteLine("Google callback triggered");
        returnUrl ??= Url.Page("/Index");

        if (remoteError != null)
        {
            TempData["DeactivatedMessage"] = $"Error from Google: {remoteError}";
            return RedirectToPage("/SignIn");
        }

        var info = await _signInManager.GetExternalLoginInfoAsync();
        if (info == null)
        {
            TempData["DeactivatedMessage"] = "Error loading Google login information.";
            return RedirectToPage("/SignIn");
        }

        var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);
        if (result.Succeeded)
        {
            var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
            var userName = user.FullName ?? user.UserName;
            var roles = await _userManager.GetRolesAsync(user);
            var userRole = roles.FirstOrDefault() ?? "No Role Assigned";

            HttpContext.Session.SetString("UserName", userName);
            HttpContext.Session.SetString("UserRole", userRole);

            return Redirect(returnUrl);
        }

        var email = info.Principal.FindFirstValue(ClaimTypes.Email);
        var userByEmail = await _userManager.FindByEmailAsync(email);
        if (userByEmail == null)
        {
            var newUser = new User
            {
                UserName = email,
                Email = email,
                FullName = info.Principal.FindFirstValue(ClaimTypes.Name) ?? email.Split('@')[0],
                IsActive = true,
                Role = "Patient"
            };

            var createResult = await _userManager.CreateAsync(newUser);
            if (createResult.Succeeded)
            {
                await _userManager.AddLoginAsync(newUser, info);
                await _signInManager.SignInAsync(newUser, isPersistent: false);

                HttpContext.Session.SetString("UserName", newUser.FullName);
                HttpContext.Session.SetString("UserRole", newUser.Role);

                return Redirect(returnUrl);
            }

            TempData["DeactivatedMessage"] = "Failed to create account.";
            return RedirectToPage("/SignIn");
        }

        await _userManager.AddLoginAsync(userByEmail, info);
        await _signInManager.SignInAsync(userByEmail, isPersistent: false);

        HttpContext.Session.SetString("UserName", userByEmail.FullName ?? userByEmail.UserName);
        var rolesAfterLink = await _userManager.GetRolesAsync(userByEmail);
        HttpContext.Session.SetString("UserRole", rolesAfterLink.FirstOrDefault() ?? "No Role Assigned");

        return Redirect(returnUrl);
    }
}
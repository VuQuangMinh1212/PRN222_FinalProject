using MedicalSearchingPlatform.Business.Interfaces;
using MedicalSearchingPlatform.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

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


        // Check password manually
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
            var userRole = roles.FirstOrDefault() ?? "No Role Assigned";
            Debug.WriteLine("------------------------------------------------"+userRole);

            HttpContext.Session.SetString("UserName", userName);
            HttpContext.Session.SetString("UserRole", userRole);

            return RedirectToPage("/Index");
        }

        TempData["DeactivatedMessage"] = "Invalid login attempt.";
        return RedirectToPage("/Index");
    }


}

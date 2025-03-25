using MedicalSearchingPlatform.Business.Interfaces;
using MedicalSearchingPlatform.Data.Entities;
using MedicalSearchingPlatform.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MedicalSearchingPlatform.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly UserManager<User> _userManager;
        public RegisterModel(IUserService userService, UserManager<User> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }
        [BindProperty]
        public RegisterViewModel Input { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var user = new User(Input.FullName, Input.Email, "Patient");
            var result = await _userManager.CreateAsync(user, Input.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }
            return RedirectToPage("/Account/Signin");
        }
    }
}

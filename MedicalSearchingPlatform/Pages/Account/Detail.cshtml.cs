using MedicalSearchingPlatform.Data.DataContext;
using MedicalSearchingPlatform.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MedicalSearchingPlatform.Pages.Account
{
    public class DetailModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _dbContext;

        public DetailModel(UserManager<User> userManager, ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        public User UserDetail { get; set; } // Renamed to avoid conflict with `User` in PageModel
        public string Role { get; set; } // Store user's role

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                // Admins can view other users' profiles
                UserDetail = await _userManager.FindByIdAsync(id);
            }
            else
            {
                // Regular users can only view their own profile
                UserDetail = await _userManager.GetUserAsync(HttpContext.User);
            }

            if (UserDetail == null)
            {
                return RedirectToPage("/Account/SignIn");
            }

            // Fetch Role from Database (same as EditModel)
            Role = await _dbContext.Users
                .Where(u => u.Id == UserDetail.Id)
                .Select(u => u.Role)
                .FirstOrDefaultAsync() ?? "Unknown";

            ViewData["IsAdmin"] = HttpContext.User.IsInRole("Admin"); // Check if the logged-in user is Admin
            return Page();
        }
    }
}

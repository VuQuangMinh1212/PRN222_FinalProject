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

        public User UserDetail { get; set; }
        public string Role { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                UserDetail = await _userManager.FindByIdAsync(id);
            }
            else
            {
                UserDetail = await _userManager.GetUserAsync(HttpContext.User);
            }

            if (UserDetail == null)
            {
                return RedirectToPage("/Account/SignIn");
            }

            Role = await _dbContext.Users
                .Where(u => u.Id == UserDetail.Id)
                .Select(u => u.Role)
                .FirstOrDefaultAsync() ?? "Unknown";

            ViewData["IsAdmin"] = HttpContext.User.IsInRole("Admin");
            return Page();
        }
    }
}
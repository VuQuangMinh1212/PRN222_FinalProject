using MedicalSearchingPlatform.Data.DataContext;
using MedicalSearchingPlatform.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalSearchingPlatform.Pages.Account
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _dbContext;

        public IndexModel(UserManager<User> userManager, ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        public List<UserViewModel> Users { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public async Task OnGetAsync()
        {
            var query = _userManager.Users.Select(user => new UserViewModel
            {
                Id = user.Id,
                FullName = user.UserName, // Modify if FullName property exists
                Email = user.Email,
                IsActive = user.LockoutEnd == null || user.LockoutEnd <= System.DateTime.UtcNow
            });

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                query = query.Where(u => u.Email.Contains(SearchTerm) || u.FullName.Contains(SearchTerm));
            }

            var usersList = await query.ToListAsync();

            foreach (var user in usersList)
            {
                string role = await _dbContext.Users
                    .Where(u => u.Id == user.Id)
                    .Select(u => u.Role)
                    .FirstOrDefaultAsync() ?? "Unknown";

                user.Role = role;
            }

            Users = usersList;
        }

        public async Task<IActionResult> OnPostToggleStatusAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            user.LockoutEnd = user.LockoutEnd == null || user.LockoutEnd <= System.DateTime.UtcNow
                ? System.DateTime.UtcNow.AddYears(100)  // Deactivate user
                : null;  // Activate user

            await _userManager.UpdateAsync(user);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
            return RedirectToPage();
        }

        public class UserViewModel
        {
            public string Id { get; set; }
            public string FullName { get; set; }
            public string Email { get; set; }
            public string Role { get; set; }
            public bool IsActive { get; set; }
        }
    }
}

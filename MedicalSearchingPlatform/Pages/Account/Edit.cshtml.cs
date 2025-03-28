using MedicalSearchingPlatform.Data.DataContext;
using MedicalSearchingPlatform.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MedicalSearchingPlatform.Pages.Account
{
    public class EditModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ApplicationDbContext _dbContext;

        public EditModel(UserManager<User> userManager, RoleManager<IdentityRole> roleManager,
                         SignInManager<User> signInManager, ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _dbContext = dbContext;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new InputModel();

        public List<string> AvailableRoles { get; set; }

        public class InputModel
        {
            public string Id { get; set; }
            public string FullName { get; set; }
            public string Email { get; set; }
            public string Role { get; set; }
            public string PhoneNumber { get; set; }
            public bool IsActive { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            // Ensure the database context is available
            if (_dbContext == null)
            {
                return StatusCode(500, "Database context is null.");
            }

            // Fetch the role from the Users table
            string role = await _dbContext.Users
                .Where(u => u.Id == user.Id)
                .Select(u => u.Role)
                .FirstOrDefaultAsync() ?? "Unknown";

            string phoneNumber = await _dbContext.Users
                .Where(u => u.Id == user.Id)
                .Select(u => u.PhoneNumber)
                .FirstOrDefaultAsync() ?? "Unknown";

            Input = new InputModel
            {
                Id = user.Id,
                FullName = user.UserName,
                Email = user.Email,
                Role = role,
                PhoneNumber = phoneNumber,
                IsActive = user.IsActive
            };

            AvailableRoles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();

            // ✅ Add this: Fetch the logged-in user’s roles
            var currentUser = await _userManager.GetUserAsync(User);
            //var roles = await _userManager.GetRolesAsync(currentUser);
            string roles = await _dbContext.Users
                .Where(u => u.Id == currentUser.Id)
                .Select(u => u.Role)
                .FirstOrDefaultAsync() ?? "Unknown";

            // ✅ Store the role in ViewData for Razor Page
            ViewData["IsAdmin"] = roles.Contains("Admin");

            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.FindByIdAsync(Input.Id);
            if (user == null) return NotFound();

            user.UserName = Input.FullName;
            user.Email = Input.Email;

            if (User.IsInRole("Admin"))
            {
                var currentRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, currentRoles);
                await _userManager.AddToRoleAsync(user, Input.Role);
            }

            await _userManager.UpdateAsync(user);

            return User.IsInRole("Admin") ? RedirectToPage("Index") : RedirectToPage("Detail", new { id = user.Id });
        }

        public async Task<IActionResult> OnPostToggleStatusAsync()
        {
            var user = await _userManager.FindByIdAsync(Input.Id);
            if (user == null) return NotFound();

            user.IsActive = !user.IsActive;
            await _userManager.UpdateAsync(user);

            return RedirectToPage("Index");
        }

        public async Task<IActionResult> OnPostDeleteAccountAsync(string password)
        {
            var user = await _userManager.FindByIdAsync(Input.Id);
            if (user == null) return NotFound();

            if (!await _userManager.CheckPasswordAsync(user, password))
            {
                ModelState.AddModelError(string.Empty, "Incorrect password.");
                return Page();
            }

            await _userManager.DeleteAsync(user);
            await _signInManager.SignOutAsync();

            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnPostSaveChangesAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.FindByIdAsync(Input.Id);
            if (user == null)
            {
                return NotFound();
            }

            user.FullName = Input.FullName;
            user.Email = Input.Email;


            // Allow role change only for Admin
            if (User.IsInRole("Admin"))
            {
                var existingRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, existingRoles);
                await _userManager.AddToRoleAsync(user, Input.Role);
            }

            await _userManager.UpdateAsync(user);

            // Redirect based on role
            if (User.IsInRole("Admin"))
            {
                return RedirectToPage("Index"); // Admin goes to Account List
            }
            else
            {
                return RedirectToPage("Detail", new { id = user.Id }); // Others go to their profile
            }
        }

    }
}
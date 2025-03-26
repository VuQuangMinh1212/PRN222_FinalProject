using MedicalSearchingPlatform.Data.DataContext;
using MedicalSearchingPlatform.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MedicalSearchingPlatform.Pages.MedicalServicePage
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public DeleteModel(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public MedicalService MedicalService { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null) return NotFound();

            MedicalService = await _context.MedicalServices.FirstOrDefaultAsync(m => m.ServiceId == id);

            if (MedicalService == null) return NotFound();

            // Verify Role - Only "Admin" or "Staff" can delete
            var user = await _userManager.GetUserAsync(HttpContext.User);
            string role = await _context.Users
                .Where(u => u.Id == user.Id)
                .Select(u => u.Role)
                .FirstOrDefaultAsync() ?? "Unknown";

            if (role != "Admin" && role != "Staff")
            {
                return Forbid(); // Prevents unauthorized access
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null) return NotFound();

            MedicalService = await _context.MedicalServices.FindAsync(id);
            if (MedicalService == null) return NotFound();

            // Verify Role - Only "Admin" or "Staff" can delete
            var user = await _userManager.GetUserAsync(HttpContext.User);
            string role = await _context.Users
                .Where(u => u.Id == user.Id)
                .Select(u => u.Role)
                .FirstOrDefaultAsync() ?? "Unknown";

            if (role != "Admin" && role != "Staff")
            {
                return Forbid();
            }

            _context.MedicalServices.Remove(MedicalService);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

using MedicalSearchingPlatform.Data.DataContext;
using MedicalSearchingPlatform.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MedicalSearchingPlatform.Pages.MedicalServicePage
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public EditModel(ApplicationDbContext context, UserManager<User> userManager)
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

            // Role verification - Only Admin & Staff can edit
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            _context.Attach(MedicalService).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicalServiceExists(MedicalService.ServiceId)) return NotFound();
                else throw;
            }

            return RedirectToPage("Index");
        }

        private bool MedicalServiceExists(string id)
        {
            return _context.MedicalServices.Any(e => e.ServiceId == id);
        }
    }
}

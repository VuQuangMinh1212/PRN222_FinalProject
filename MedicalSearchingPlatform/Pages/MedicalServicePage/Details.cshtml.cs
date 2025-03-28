using MedicalSearchingPlatform.Data.DataContext;
using MedicalSearchingPlatform.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MedicalSearchingPlatform.Pages.MedicalServicePage
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public DetailsModel(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public MedicalService MedicalService { get; set; } = default!;
        public string UserRole { get; set; } = "Guest";

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null) return NotFound();

            var medicalService = await _context.MedicalServices.FirstOrDefaultAsync(m => m.ServiceId == id);
            if (medicalService == null) return NotFound();

            MedicalService = medicalService;

            // Get the logged-in user's role
            var user = await _userManager.GetUserAsync(User);
            UserRole = user != null ? (await _userManager.GetRolesAsync(user)).FirstOrDefault() ?? "Guest" : "Guest";

            return Page();
        }
    }
}

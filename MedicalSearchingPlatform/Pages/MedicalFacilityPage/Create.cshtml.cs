using MedicalSearchingPlatform.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MedicalSearchingPlatform.Pages.MedicalFacilityPage
{
    public class CreateModel : PageModel
    {
        private readonly MedicalSearchingPlatform.Data.DataContext.ApplicationDbContext _context;

        public CreateModel(MedicalSearchingPlatform.Data.DataContext.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public MedicalFacility MedicalFacility { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.MedicalFacilities.Add(MedicalFacility);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

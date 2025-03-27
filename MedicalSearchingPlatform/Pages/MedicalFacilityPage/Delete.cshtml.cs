using MedicalSearchingPlatform.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MedicalSearchingPlatform.Pages.MedicalFacilityPage
{
    public class DeleteModel : PageModel
    {
        private readonly MedicalSearchingPlatform.Data.DataContext.ApplicationDbContext _context;

        public DeleteModel(MedicalSearchingPlatform.Data.DataContext.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public MedicalFacility MedicalFacility { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicalfacility = await _context.MedicalFacilities.FirstOrDefaultAsync(m => m.FacilityId == id);

            if (medicalfacility == null)
            {
                return NotFound();
            }
            else
            {
                MedicalFacility = medicalfacility;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicalfacility = await _context.MedicalFacilities.FindAsync(id);
            if (medicalfacility != null)
            {
                MedicalFacility = medicalfacility;
                _context.MedicalFacilities.Remove(MedicalFacility);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

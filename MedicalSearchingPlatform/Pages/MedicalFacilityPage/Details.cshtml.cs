using MedicalSearchingPlatform.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MedicalSearchingPlatform.Pages.MedicalFacilityPage
{
    public class DetailsModel : PageModel
    {
        private readonly MedicalSearchingPlatform.Data.DataContext.ApplicationDbContext _context;

        public DetailsModel(MedicalSearchingPlatform.Data.DataContext.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MedicalSearchingPlatform.Data.DataContext;
using MedicalSearchingPlatform.Data.Entities;

namespace MedicalSearchingPlatform.Pages.MedicalRecordPage
{
    public class DeleteModel : PageModel
    {
        private readonly MedicalSearchingPlatform.Data.DataContext.ApplicationDbContext _context;

        public DeleteModel(MedicalSearchingPlatform.Data.DataContext.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public MedicalRecord MedicalRecord { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicalrecord = await _context.MedicalRecords.FirstOrDefaultAsync(m => m.MedicalRecordId == id);

            if (medicalrecord == null)
            {
                return NotFound();
            }
            else
            {
                MedicalRecord = medicalrecord;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicalrecord = await _context.MedicalRecords.FindAsync(id);
            if (medicalrecord != null)
            {
                MedicalRecord = medicalrecord;
                _context.MedicalRecords.Remove(MedicalRecord);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

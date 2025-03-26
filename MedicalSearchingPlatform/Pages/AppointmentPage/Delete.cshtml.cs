using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MedicalSearchingPlatform.Data.DataContext;
using MedicalSearchingPlatform.Data.Entities;

namespace MedicalSearchingPlatform.Pages.AppointmentPage
{
    public class DeleteModel : PageModel
    {
        private readonly MedicalSearchingPlatform.Data.DataContext.ApplicationDbContext _context;

        public DeleteModel(MedicalSearchingPlatform.Data.DataContext.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Appointment Appointment { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments.FirstOrDefaultAsync(m => m.AppointmentId == id);

            if (appointment == null)
            {
                return NotFound();
            }
            else
            {
                Appointment = appointment;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment != null)
            {
                Appointment = appointment;
                _context.Appointments.Remove(Appointment);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MedicalSearchingPlatform.Data.DataContext;
using MedicalSearchingPlatform.Data.Entities;

namespace MedicalSearchingPlatform.Pages.WorkingSchedulePage
{
    public class DetailsModel : PageModel
    {
        private readonly MedicalSearchingPlatform.Data.DataContext.ApplicationDbContext _context;

        public DetailsModel(MedicalSearchingPlatform.Data.DataContext.ApplicationDbContext context)
        {
            _context = context;
        }

        public WorkingSchedule WorkingSchedule { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workingschedule = await _context.WorkingSchedules.FirstOrDefaultAsync(m => m.ScheduleId == id);
            if (workingschedule == null)
            {
                return NotFound();
            }
            else
            {
                WorkingSchedule = workingschedule;
            }
            return Page();
        }
    }
}

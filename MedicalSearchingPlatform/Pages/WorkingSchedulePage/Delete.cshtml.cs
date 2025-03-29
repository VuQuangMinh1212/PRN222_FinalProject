using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MedicalSearchingPlatform.Data.DataContext;
using MedicalSearchingPlatform.Data.Entities;
using MedicalSearchingPlatform.Business.Interfaces;

namespace MedicalSearchingPlatform.Pages.WorkingSchedulePage
{
    public class DeleteModel : PageModel
    {
        private readonly IWorkingScheduleService _workingScheduleService;
        public DeleteModel(IWorkingScheduleService workingScheduleService)
        {
            _workingScheduleService = workingScheduleService;
        }

        [BindProperty]
        public WorkingSchedule WorkingSchedule { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            await _workingScheduleService.DeleteWorkingSchedule(id);
            return RedirectToPage("/WorkingSchedulePage/Index");
        }
    }
}

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
using MedicalSearchingPlatform.Business.Services;

namespace MedicalSearchingPlatform.Pages.WorkingSchedulePage
{
    public class DeleteModel : PageModel
    {
        private readonly IWorkingScheduleService _workingScheduleService;
        private readonly IAppointmentService _appointmentService;
        public DeleteModel(IWorkingScheduleService workingScheduleService, IAppointmentService appointmentService)
        {
            _workingScheduleService = workingScheduleService;
            _appointmentService = appointmentService;
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

        public async Task<JsonResult> OnPostAsync(string id)
        {
            var appointment = await _appointmentService.GetAppoimentByScheduleIdAsync(id);
            if (appointment != null)
            {
                return new JsonResult(new { isValid = false, message = "This slot have appointment can't Delete" });
            }
            if (id == null)
            {
                return new JsonResult(new { isValid = false, message = "Id is null" });
            }
            await _workingScheduleService.DeleteWorkingSchedule(id);
            return new JsonResult(new { isValid = true, message = "Deleted successfully!", redirect = "/WorkingSchedulePage/Index" });
        }
    }
}

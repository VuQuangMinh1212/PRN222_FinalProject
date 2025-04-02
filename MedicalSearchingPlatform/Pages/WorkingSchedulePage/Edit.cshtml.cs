using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MedicalSearchingPlatform.Data.DataContext;
using MedicalSearchingPlatform.Data.Entities;
using MedicalSearchingPlatform.Business.Interfaces;
using System.Security.Claims;

namespace MedicalSearchingPlatform.Pages.WorkingSchedulePage
{
    public class EditModel : PageModel
    {
        private readonly IWorkingScheduleService _workingScheduleService;
        private readonly IDoctorService _doctorService;
        private readonly IAppointmentService _appointmentService;
        public EditModel(IWorkingScheduleService workingScheduleService,
            IDoctorService doctorService, IAppointmentService appointmentService)
        {
            _workingScheduleService = workingScheduleService;
            _doctorService = doctorService;
            _appointmentService = appointmentService;
        }

        [BindProperty]
        public WorkingSchedule WorkingSchedule { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            WorkingSchedule = await _workingScheduleService.GetWorkingScheduleById(id);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var appointment = await _appointmentService.GetAppoimentByScheduleIdAsync(WorkingSchedule.ScheduleId);
            if (appointment != null)
            {
                return new JsonResult(new { isValid = false, message = "This slot have appointment can't edit" });
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }
            WorkingSchedule.EndTime = WorkingSchedule.StartTime.Add(TimeSpan.FromHours(1));

            await _workingScheduleService.UpdateWorkingSchedule(WorkingSchedule);

            return RedirectToPage("./Index");
        }
    }
}

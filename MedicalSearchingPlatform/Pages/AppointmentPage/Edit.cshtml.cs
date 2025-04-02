using MedicalSearchingPlatform.Business.Interfaces;
using MedicalSearchingPlatform.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace MedicalSearchingPlatform.Pages.AppointmentPage
{
    public class EditModel : PageModel
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IDoctorService _doctorService;
        public EditModel(IAppointmentService appointmentService, IDoctorService doctorService)
        {
            _appointmentService = appointmentService;
            _doctorService = doctorService;
        }

        [BindProperty]
        public Appointment Appointment { get; set; } = default!;
        public SelectList AppointmentStatus { get; set; }

        public async Task<IActionResult> OnGetDoctorEditStatusAsync(string id)
        {
            Appointment = await _appointmentService.GetAppointmentByIdAsync(id);
            if (Appointment.Status == "Pending")
            {
                AppointmentStatus = new SelectList(new List<Object>
                {
                    new{Value ="Accepted", Text ="Accept"},
                    new{Value ="Canceled", Text ="Cancel"},
                }, "Value", "Text");
            }
            else
            {
                AppointmentStatus = new SelectList(new List<Object>
                {
                    new{Value ="Canceled", Text ="Cancel"},
                    new{Value ="Completed", Text ="Complete"},
                }, "Value", "Text");
            }
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var existingAppointment = await _appointmentService.GetAppointmentByIdAsync(Appointment.AppointmentId);
            if (existingAppointment == null)
            {
                return NotFound();
            }
            existingAppointment.Status = Appointment.Status;
            await _appointmentService.UpdateAppointmentAsync(existingAppointment);

            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnGetCancleAsync(string id)
        {
            var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
            if (appointment == null)
            {
                return Page();
            }

            appointment.Status = "Cancled";

            await _appointmentService.UpdateAppointmentAsync(appointment);

            return RedirectToPage("./History");
        }
    }
}

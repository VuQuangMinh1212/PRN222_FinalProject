using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MedicalSearchingPlatform.Business.Interfaces;
using MedicalSearchingPlatform.Data.Entities;
using System.Security.Claims;
using MedicalSearchingPlatform.Models;
using Microsoft.AspNetCore.Mvc;

namespace MedicalSearchingPlatform.Pages.AppointmentPage
{
    public class IndexModel : PageModel
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IDoctorService _doctorService;

        [BindProperty]
        public List<AppointmentViewModel> Appointments { get; set; }

        public IndexModel(IAppointmentService appointmentService, IDoctorService doctorService)
        {
            _appointmentService = appointmentService;
            _doctorService = doctorService;
        }

        public async Task OnGetAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var appointments = await _appointmentService.GetAllAppointmentsAsync();

            var appointmentViewModels = appointments.Select(appointment => new AppointmentViewModel
            {
                AppointmentId = appointment.AppointmentId,
                AppointmentDate = appointment.AppointmentDate,
                PatientName = appointment.Patient.User.FullName,
                ScheduleDate = appointment.WorkingSchedule.WorkDate.ToString("yyyy-MM-dd"),
                ScheduleTime = $"{appointment.WorkingSchedule.StartTime.ToString(@"hh\:mm")} - {appointment.WorkingSchedule.EndTime.ToString(@"hh\:mm")}",
                AppointmentInfo = appointment.AppointmentInfo,
                Status = appointment.Status
            }).OrderByDescending(a => a.AppointmentDate).ToList();

            Appointments = appointmentViewModels;
        }

    }
}

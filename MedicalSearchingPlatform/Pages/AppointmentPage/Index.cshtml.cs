using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MedicalSearchingPlatform.Business.Interfaces;
using MedicalSearchingPlatform.Data.Entities;
using System.Security.Claims;

namespace MedicalSearchingPlatform.Pages.AppointmentPage
{
    public class IndexModel : PageModel
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IDoctorService _doctorService;

        public IndexModel(IAppointmentService appointmentService,IDoctorService doctorService)
        {
            _appointmentService = appointmentService;
            _doctorService = doctorService;
        }

        public IList<Appointment> Appointment { get; private set; } = new List<Appointment>();

        public async Task OnGetAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var appointments = await _appointmentService.GetAllAppointmentsAsync();
            Appointment = new List<Appointment>(appointments);
        }
    }
}

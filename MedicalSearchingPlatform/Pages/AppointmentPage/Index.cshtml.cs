using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MedicalSearchingPlatform.Business.Interfaces;
using MedicalSearchingPlatform.Data.Entities;

namespace MedicalSearchingPlatform.Pages.AppointmentPage
{
    public class IndexModel : PageModel
    {
        private readonly IAppointmentService _appointmentService;

        public IndexModel(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        public IList<Appointment> Appointment { get; private set; } = new List<Appointment>();

        public async Task OnGetAsync()
        {
            var appointments = await _appointmentService.GetAllAppointmentsAsync();
            Appointment = new List<Appointment>(appointments);
        }
    }
}

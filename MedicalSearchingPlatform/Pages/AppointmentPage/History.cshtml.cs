using MedicalSearchingPlatform.Business.Interfaces;
using MedicalSearchingPlatform.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MedicalSearchingPlatform.Pages.AppointmentPage
{
    public class HistoryModel : PageModel
    {
        private readonly IAppointmentService _appointmentService;
        private readonly UserManager<User> _userManager;

        public HistoryModel(IAppointmentService appointmentService, UserManager<User> userManager)
        {
            _appointmentService = appointmentService;
            _userManager = userManager;
        }

        public IList<Appointment> AppointmentHistory { get; private set; } = new List<Appointment>();

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return;

            var allAppointments = await _appointmentService.GetAllAppointmentsAsync();

            // Filter past appointments for the logged-in user
            AppointmentHistory = allAppointments
                .Where(a => a.Patient.UserId == user.Id)
                .OrderByDescending(a => a.AppointmentDate)
                .ToList();
        }
    }
}

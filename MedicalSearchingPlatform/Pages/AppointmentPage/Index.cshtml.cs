using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MedicalSearchingPlatform.Business.Interfaces;
using MedicalSearchingPlatform.Data.Entities;
using MedicalSearchingPlatform.Models;

namespace MedicalSearchingPlatform.Pages.AppointmentPage
{
    public class IndexModel : PageModel
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IUserService _userService; // Using IUserService

        public IList<AppointmentViewModel> Appointments { get; private set; } = new List<AppointmentViewModel>();

        public IndexModel(IAppointmentService appointmentService, IUserService userService)
        {
            _appointmentService = appointmentService;
            _userService = userService;
        }

        public async Task OnGetAsync()
        {
            var appointments = await _appointmentService.GetAllAppointmentsAsync();

            var appointmentViewModels = appointments.Select(appointment => new AppointmentViewModel
            {
                AppointmentId = appointment.AppointmentId,
                AppointmentDate = appointment.AppointmentDate,
                AppointmentInfo = appointment.AppointmentInfo,
                Status = appointment.Status
            }).ToList();

            Appointments = appointmentViewModels;
        }

    }
}

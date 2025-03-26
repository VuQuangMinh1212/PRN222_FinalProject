using MedicalSearchingPlatform.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MedicalSearchingPlatform.Pages.Dashboard
{
    public class IndexModel : PageModel
    {
        private readonly IDoctorService _doctorService;

        public IndexModel(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        public List<string> DoctorNames { get; set; } = new();
        public List<int> BookingCounts { get; set; } = new();
        public async Task OnGetAsync()
        {
            var topDoctors = await _doctorService.GetMostBookedDoctorsAsync(5); // Lấy top 5 bác sĩ được đặt lịch nhiều nhất

            DoctorNames = topDoctors.Select(d => d.User.FullName).ToList();
            BookingCounts = topDoctors.Select(d => d.Appointments.Count).ToList();
        }
    }
}

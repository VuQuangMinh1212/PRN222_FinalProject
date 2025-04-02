using MedicalSearchingPlatform.Business.Interfaces;
using MedicalSearchingPlatform.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MedicalSearchingPlatform.Pages.Dashboard
{
    public class IndexModel : PageModel
    {
        private readonly IDoctorService _doctorService;
        private readonly IArticleService _articleService;
        private readonly IAppointmentService _appointmentService;
        private readonly IPatientService _patientService;


        public IndexModel(IDoctorService doctorService, IArticleService articleService, IAppointmentService appointmentService, IPatientService patientService)
        {
            _doctorService = doctorService;
            _articleService = articleService;
            _appointmentService = appointmentService;
            _patientService = patientService;
        }
        public int TotalPatients { get; set; }
        public int TotalArticels { get; set; }

        public int TotalAppointments { get; set; }
        // Danh sách bác sĩ được đặt lịch nhiều nhất
        public List<string> DoctorNames { get; set; } = new();
        public List<int> BookingCounts { get; set; } = new();

        // Danh sách bài viết có nhiều lượt thích nhất
        public List<string> ArticleTitles { get; set; } = new();
        public List<int> ArticleLikes { get; set; } = new();

        // Báo cáo số lượng lịch hẹn theo tháng
        public List<string> Months { get; set; } = new();
        public List<int> AppointmentCounts { get; set; } = new();
        public async Task OnGetAsync()
        {

            // Lấy tổng số bệnh nhân
            TotalPatients = await _patientService.GetTotalPatientsAsync();

            //Lấy tổng số bài viết
            TotalArticels = await _articleService.GetTotalArticelsAsync();

            //Lấy tổng số lịch hẹn
            TotalAppointments = await _appointmentService.GetTotalAppointmentsAsync();
            // Lấy top 5 bác sĩ được đặt lịch nhiều nhất
            var topDoctors = await _doctorService.GetMostBookedDoctorsAsync(5);
            DoctorNames = topDoctors.Select(d => d.User.FullName).ToList();
            BookingCounts = topDoctors.Select(d => d.Appointments.Count).ToList();

            // Lấy top 5 bài viết có nhiều lượt thích nhất
            var topArticles = await _articleService.GetMostLikedArticlesAsync(5);

            ArticleTitles = topArticles.Select(a => a.Title).ToList();

            ArticleLikes = new List<int>();
            foreach (var article in topArticles)
            {
                int likeCount = await _articleService.GetArticleLikeCountAsync(article.ArticleId);
                ArticleLikes.Add(likeCount);
            }

            var monthlyAppointments = await _appointmentService.GetAppointmentCountByMonthAsync();
            Months = monthlyAppointments.Select(a => a.MonthName).ToList();
            AppointmentCounts = monthlyAppointments.Select(a => a.Count).ToList();
        }

    }
}

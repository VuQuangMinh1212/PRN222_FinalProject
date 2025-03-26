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

        public IndexModel(IDoctorService doctorService, IArticleService articleService)
        {
            _doctorService = doctorService;
            _articleService = articleService;
        }

        // Danh sách bác sĩ được đặt lịch nhiều nhất
        public List<string> DoctorNames { get; set; } = new();
        public List<int> BookingCounts { get; set; } = new();

        // Danh sách bài viết có nhiều lượt thích nhất
        public List<string> ArticleTitles { get; set; } = new();
        public List<int> ArticleLikes { get; set; } = new();
        public async Task OnGetAsync()
        {
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
        }

    }
}

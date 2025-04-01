using MedicalSearchingPlatform.Business.Interfaces;
using MedicalSearchingPlatform.Data.Entities;
using MedicalSearchingPlatform.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace MedicalSearchingPlatform.Pages.ReviewPage
{
    public class ReviewFacilityModel : PageModel
    {
        private readonly IPatientService _patientService;
        private readonly IReviewService _reviewService;
        public ReviewFacilityModel(IPatientService patientService, IReviewService reviewService)
        {
            _patientService = patientService;
            _reviewService = reviewService;
        }

        [BindProperty]
        public int RatingValue { get; set; }
        [BindProperty]
        public string Comment { get; set; }
        [BindProperty]
        public string FacilityId { get; set; }

        public void OnGet(string facilityId)
        {
            FacilityId = facilityId;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var patient = await _patientService.GetPatientByUserId(userId);

            var existsReview = (await _reviewService.GetAllReviewsAsync()).Where(x => x.FacilityId == FacilityId && x.PatientId == patient.PatientId).FirstOrDefault();
            if (existsReview == null)
            {
                Review review = new Review
                {
                    PatientId = patient.PatientId,
                    FacilityId = FacilityId,
                    Comment = Comment,
                    Rating = RatingValue,
                    CreatedDate = DateTime.Now,
                };
                await _reviewService.SubmitReviewAsync(review);
            }
            else
            {
                
            }
            return RedirectToPage("/MedicalFacilityPage/Index");
        }
    }
}

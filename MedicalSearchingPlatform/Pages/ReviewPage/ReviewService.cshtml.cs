using MedicalSearchingPlatform.Business.Interfaces;
using MedicalSearchingPlatform.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace MedicalSearchingPlatform.Pages.ReviewPage
{
    public class ReviewServiceModel : PageModel
    {
        private readonly IPatientService _patientService;
        private readonly IReviewService _reviewService;
        public ReviewServiceModel(IPatientService patientService, IReviewService reviewService)
        {
            _patientService = patientService;
            _reviewService = reviewService;
        }

        [BindProperty]
        public int RatingValue { get; set; }
        [BindProperty]
        public string Description { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var patient = await _patientService.GetPatientByUserId(userId);
            return RedirectToPage("./Index");
        }
    }
}

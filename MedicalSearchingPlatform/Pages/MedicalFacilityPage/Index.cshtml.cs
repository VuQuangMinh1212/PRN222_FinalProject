using MedicalSearchingPlatform.Business.Interfaces;
using MedicalSearchingPlatform.Data.Entities;
using MedicalSearchingPlatform.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace MedicalSearchingPlatform.Pages.MedicalFacilityPage
{
    public class IndexModel : PageModel
    {
        private readonly IMedicalFacilityService _facilityService;
        private readonly IReviewService _reviewService;
        private readonly IPatientService _patientService;

        public IndexModel(IMedicalFacilityService facilityService, IReviewService reviewService, IPatientService patientService)
        {
            _facilityService = facilityService;
            _reviewService = reviewService;
            _patientService = patientService;
        }

        public IList<MedicalFacility> MedicalFacility { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string SearchName { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchAddress { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchInfor { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchPhoneNumber { get; set; }

        public List<string?> ReviewFacility { get; set; }

        public async Task OnGetAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var patient = await _patientService.GetPatientByUserId(userId);
            if (patient != null)
            {
                var facilityReview = await _reviewService.GetReviewByPatientId(patient.PatientId);
                ReviewFacility = facilityReview.Select(x => x.FacilityId).ToList();
            }
            MedicalFacility = (await _facilityService.SearchFacilityAsync(
                SearchName, SearchAddress, SearchInfor,
                SearchPhoneNumber)).ToList();
        }
    }
}

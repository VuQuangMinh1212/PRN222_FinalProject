using MedicalSearchingPlatform.Business.Interfaces;
using MedicalSearchingPlatform.Data.Entities;
using MedicalSearchingPlatform.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MedicalSearchingPlatform.Pages.MedicalFacilityPage
{
    public class IndexModel : PageModel
    {
        private readonly IMedicalFacilityService _facilityService;
        private readonly IReviewService _reviewService;

        public IndexModel(IMedicalFacilityService facilityService, IReviewService reviewService)
        {
            _facilityService = facilityService;
            _reviewService = reviewService;
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

        public async Task OnGetAsync()
        {
            MedicalFacility = (await _facilityService.SearchFacilityAsync(
                SearchName, SearchAddress, SearchInfor,
                SearchPhoneNumber)).ToList();
        }
    }
}

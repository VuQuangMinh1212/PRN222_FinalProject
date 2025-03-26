using MedicalSearchingPlatform.Business.Interfaces;
using MedicalSearchingPlatform.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MedicalSearchingPlatform.Pages.DoctorPage
{
    public class IndexModel : PageModel
    {
        private readonly IDoctorService _doctorService;

        public IndexModel(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        public IList<Doctor> Doctors { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchName { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchSpecialty { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchFacility { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchExpertise { get; set; }
        [BindProperty(SupportsGet = true)]
        public double? SearchMinRating { get; set; }
        [BindProperty(SupportsGet = true)]
        public decimal? SearchMaxFee { get; set; }

        public async Task OnGetAsync()
        {
            Doctors = (await _doctorService.SearchDoctorsAsync(
                SearchName, SearchSpecialty, SearchFacility,
                SearchExpertise, SearchMinRating, SearchMaxFee)).ToList();
        }
    }
}
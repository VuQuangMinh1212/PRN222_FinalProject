using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MedicalSearchingPlatform.Data.DataContext;
using MedicalSearchingPlatform.Data.Entities;
using MedicalSearchingPlatform.Business.Interfaces;
using MedicalSearchingPlatform.Business.Services;

namespace MedicalSearchingPlatform.Pages.MedicalFacilityPage
{
    public class IndexModel : PageModel
    {
        private readonly IMedicalFacilityService _facilityService;

        public IndexModel(IMedicalFacilityService facilityService)
        {
            _facilityService = facilityService;
        }

        public IList<MedicalFacility> MedicalFacility { get;set; } = default!;
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

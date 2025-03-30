using MedicalSearchingPlatform.Data.Entities;
using MedicalSearchingPlatform.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicalSearchingPlatform.Pages.MedicalRecordPage
{
    public class IndexModel : PageModel
    {
        private readonly IMedicalRecordService _medicalRecordService;

        public IndexModel(IMedicalRecordService medicalRecordService)
        {
            _medicalRecordService = medicalRecordService;
        }

        public IList<MedicalRecord> MedicalRecord { get; set; } = default!;

        public async Task OnGetAsync()
        {
            MedicalRecord = (await _medicalRecordService.GetAllRecordsAsync()).ToList();
        }
    }
}
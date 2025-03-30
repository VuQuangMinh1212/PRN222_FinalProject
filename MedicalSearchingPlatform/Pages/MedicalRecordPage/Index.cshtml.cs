using MedicalSearchingPlatform.Data.Entities;
using MedicalSearchingPlatform.Services;
using Microsoft.AspNetCore.Mvc;
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

        [BindProperty(SupportsGet = true)]
        public string UserId { get; set; }

        public async Task OnGetAsync()
        {
            if (string.IsNullOrEmpty(UserId))
            {
                MedicalRecord = (await _medicalRecordService.GetAllRecordsAsync()).ToList();
            }
            else
            {
                var patient = await _medicalRecordService.GetPatientByUserIdAsync(UserId);
                if (patient != null)
                {
                    MedicalRecord = (await _medicalRecordService.GetPatientRecordsAsync(patient.PatientId)).ToList();
                }
                else
                {
                    MedicalRecord = new List<MedicalRecord>();
                }
            }
        }
    }
}
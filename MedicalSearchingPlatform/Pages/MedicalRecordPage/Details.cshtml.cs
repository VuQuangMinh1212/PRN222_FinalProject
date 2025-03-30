using MedicalSearchingPlatform.Data.Entities;
using MedicalSearchingPlatform.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace MedicalSearchingPlatform.Pages.MedicalRecordPage
{
    public class DetailsModel : PageModel
    {
        private readonly IMedicalRecordService _medicalRecordService;

        public DetailsModel(IMedicalRecordService medicalRecordService)
        {
            _medicalRecordService = medicalRecordService;
        }

        public MedicalRecord MedicalRecord { get; set; }
        public Patient Patient { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Debug.Write(id);
            Patient = await _medicalRecordService.GetPatientByUserIdAsync(id);

            MedicalRecord = await _medicalRecordService.GetMedicalRecordAsync(Patient.PatientId);
            if (MedicalRecord == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
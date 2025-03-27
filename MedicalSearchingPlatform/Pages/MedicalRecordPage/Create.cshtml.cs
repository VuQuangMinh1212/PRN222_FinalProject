using MedicalSearchingPlatform.Business.Interfaces;
using MedicalSearchingPlatform.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace MedicalSearchingPlatform.Pages.MedicalRecordPage
{
    public class CreateModel : PageModel
    {
        private readonly IMedicalRecordService _medicalRecordService;

        public CreateModel(IMedicalRecordService medicalRecordService)
        {
            _medicalRecordService = medicalRecordService;
        }

        [BindProperty]
        public MedicalRecord MedicalRecord { get; set; }

        public IActionResult OnGet(int patientId)
        {
            MedicalRecord = new MedicalRecord { PatientId = patientId };
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _medicalRecordService.AddMedicalRecordAsync(MedicalRecord);
            return RedirectToPage("./Index", new { patientId = MedicalRecord.PatientId });
        }
    }
}
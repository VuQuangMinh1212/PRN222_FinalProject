using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MedicalSearchingPlatform.Data.Entities;
using MedicalSearchingPlatform.Services;

namespace MedicalSearchingPlatform.Pages.MedicalRecordPage
{
    public class DetailsModel : PageModel
    {
        private readonly IMedicalRecordService _medicalRecordService;

        public DetailsModel(IMedicalRecordService medicalRecordService)
        {
            _medicalRecordService = medicalRecordService;
        }

        public MedicalRecord MedicalRecord { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            MedicalRecord = await _medicalRecordService.GetMedicalRecordAsync(id);

            if (MedicalRecord == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}

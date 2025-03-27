using MedicalSearchingPlatform.Business.Interfaces;
using MedicalSearchingPlatform.Data.Entities;
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

        public IList<MedicalRecord> MedicalRecords { get; set; }
        public int PatientId { get; set; }

        public async Task OnGetAsync(int patientId = 1)
        {
            PatientId = patientId;
            MedicalRecords = await _medicalRecordService.GetMedicalRecordsByPatientAsync(patientId);
        }
    }
}
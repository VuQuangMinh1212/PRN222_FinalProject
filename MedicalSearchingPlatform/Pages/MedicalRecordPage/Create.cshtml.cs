using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MedicalSearchingPlatform.Data.Entities;
using MedicalSearchingPlatform.Services;
using MedicalSearchingPlatform.Business.Interfaces;
using System.Diagnostics;
using System.Text.Json;

namespace MedicalSearchingPlatform.Pages.MedicalRecordPage
{
    public class CreateModel : PageModel
    {
        private readonly IMedicalRecordService _medicalRecordService;
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientService;

        public CreateModel(IMedicalRecordService medicalRecordService, IPatientService patientService, IDoctorService doctorService)
        {
            _medicalRecordService = medicalRecordService;
            _doctorService = doctorService;
            _patientService = patientService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            ViewData["DoctorId"] = new SelectList(
                await _doctorService.GetAllDoctorsAsync(),
                "DoctorId",
                "User.FullName"
            );
            ViewData["PatientId"] = new SelectList(
                await _patientService.GetAllPatientsAsync(),
                "PatientId",
                "User.FullName"
            );

            return Page();
        }

        [BindProperty]
        public MedicalRecord MedicalRecord { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            Debug.WriteLine("OnPostAsync called");
            ModelState.Remove("MedicalRecord.Patient");
            ModelState.Remove("MedicalRecord.Doctor");


            // Log raw form data
            foreach (var key in Request.Form.Keys)
            {
                Debug.WriteLine($"Form Key: {key}, Value: {Request.Form[key]}");
                Debug.WriteLine($"Bound MedicalRecord: {JsonSerializer.Serialize(MedicalRecord)}");

            }

            if (!ModelState.IsValid)
            {
                Debug.WriteLine("ModelState invalid");

                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Debug.WriteLine($"Validation error: {error.ErrorMessage}");
                }

                ViewData["DoctorId"] = new SelectList(
                    await _doctorService.GetAllDoctorsAsync(),
                    "DoctorId",
                    "User.FullName"
                );
                ViewData["PatientId"] = new SelectList(
                    await _patientService.GetAllPatientsAsync(),
                    "PatientId",
                    "User.FullName"
                );

                return Page();
            }

            Debug.WriteLine($"PatientId: {MedicalRecord.PatientId}, DoctorId: {MedicalRecord.DoctorId}");

            await _medicalRecordService.CreateMedicalRecordAsync(MedicalRecord);
            Debug.WriteLine("Record created, redirecting");

            return RedirectToPage("./Index");
        }

    }
}
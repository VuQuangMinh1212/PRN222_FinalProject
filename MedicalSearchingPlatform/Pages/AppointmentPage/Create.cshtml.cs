using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MedicalSearchingPlatform.Data.Entities;
using MedicalSearchingPlatform.Business.Interfaces;

namespace MedicalSearchingPlatform.Pages.AppointmentPage
{
    public class CreateModel : PageModel
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientService;

        [BindProperty]
        public Appointment Appointment { get; set; } = new Appointment();

        public string CurrentPatientName { get; set; } // New property for patient's name

        public CreateModel(IAppointmentService appointmentService, IDoctorService doctorService, IPatientService patientService)
        {
            _appointmentService = appointmentService;
            _doctorService = doctorService;
            _patientService = patientService;
        }

        public async Task<IActionResult> OnGet()
        {
            Appointment = new Appointment { Status = "Scheduled" };

            // Get current user's ID from authentication
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(userId))
            {
                var patient = await _patientService.GetPatientByUserIdAsync(userId);
                if (patient != null && HttpContext.Session.GetString("UserRole") == "Patient")
                {
                    Appointment.PatientId = patient.PatientId; // Set PatientId
                    CurrentPatientName = patient.User?.FullName ?? "Unnamed Patient"; // Set patient's name
                }
            }

            // Populate dropdowns
            var doctors = await _doctorService.GetAllDoctorsAsync() ?? new List<Doctor>();
            var patients = await _patientService.GetAllPatientsAsync() ?? new List<Patient>();

            ViewData["DoctorId"] = new SelectList(doctors.Select(d => new
            {
                d.DoctorId,
                Name = d.User?.FullName ?? "Unnamed Doctor"
            }), "DoctorId", "Name");

            ViewData["PatientId"] = new SelectList(patients.Select(p => new
            {
                p.PatientId,
                Name = p.User?.FullName ?? "Unnamed Patient"
            }), "PatientId", "Name");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            bool isBooked = await _appointmentService.BookAppointmentAsync(Appointment);
            if (!isBooked)
            {
                ModelState.AddModelError("", "Failed to book appointment. Please try again.");
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
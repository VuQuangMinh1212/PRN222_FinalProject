using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MedicalSearchingPlatform.Data.DataContext;
using MedicalSearchingPlatform.Data.Entities;
using MedicalSearchingPlatform.Business.Interfaces;
using System.Security.Claims;

namespace MedicalSearchingPlatform.Pages.AppointmentPage
{
    public class CreateModel : PageModel
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientService;
        private IWorkingScheduleService _workingScheduleService;

        [BindProperty]
        public Appointment Appointment { get; set; } = new Appointment();
        public SelectList WorkingSchedules { get; set; }

        public CreateModel(IAppointmentService appointmentService,
            IDoctorService doctorService,
            IPatientService patientService,
            IWorkingScheduleService workingScheduleService)
        {
            _appointmentService = appointmentService;
            _doctorService = doctorService;
            _patientService = patientService;
            _workingScheduleService = workingScheduleService;
        }


        public async Task<IActionResult> OnGet(string doctorId)
        {
            var schedules = await _workingScheduleService.GetAvailableWorkingScheduleOfDoctor(doctorId);
            var selectedItem = schedules.Select(ws => new SelectListItem
            {
                Value = ws.ScheduleId,
                Text = $"{ws.StartTime:hh\\:mm} - {ws.EndTime:hh\\:mm} | {ws.WorkDate:yyyy-MM-dd}"
            }).ToList();

            WorkingSchedules = new SelectList(selectedItem, "Value", "Text");

            Appointment = new Appointment { Status = "Scheduled" };
            var doctors = await _doctorService.GetAllDoctorsAsync() ?? new List<Doctor>();
            //var patients = await _patientService.GetAllPatientsAsync() ?? new List<Patient>();



            ViewData["DoctorId"] = new SelectList(doctors.Select(d => new
            {
                d.DoctorId,
                Name = d.User?.FullName ?? "Unnamed Doctor"
            }), "DoctorId", "Name");

            //ViewData["PatientId"] = new SelectList(patients.Select(p => new
            //{
            //    p.PatientId,
            //    Name = p.User?.FullName ?? "Unnamed Patient"
            //}), "PatientId", "Name");

            return Page();
        }


        public async Task<JsonResult> OnGetSchedulesAsync(string doctorId)
        {
            var schedules = await _workingScheduleService.GetAvailableWorkingScheduleOfDoctor(doctorId);

            var scheduleList = schedules.Select(ws => new SelectListItem
            {
                Value = ws.ScheduleId,
                Text = $"{ws.StartTime:hh\\:mm} - {ws.EndTime:hh\\:mm} | {ws.WorkDate:yyyy-MM-dd}"
            }).ToList();

            return new JsonResult(scheduleList);
        }


        public async Task<IActionResult> OnPostAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var patient = await _patientService.GetPatientByUserId(userId);
            Appointment.PatientId = patient.PatientId;
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

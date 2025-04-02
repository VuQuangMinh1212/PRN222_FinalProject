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
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using MedicalSearchingPlatform.Business.Services;
using System.Diagnostics;

namespace MedicalSearchingPlatform.Pages.AppointmentPage
{
    public class CreateModel : PageModel
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IDoctorService _doctorService;
        private readonly IMedicalFacilityService _facilityService;
        private readonly IPatientService _patientService;
        private IWorkingScheduleService _workingScheduleService;
        private readonly IMedicalServiceService _medicalServiceService;

        [BindProperty]
        public Appointment Appointment { get; set; } = new Appointment();
        public SelectList WorkingSchedules { get; set; }
        public List<string> SelectedServices { get; set; } = new List<string>(); 
        public SelectList AvailableServices { get; set; }

        public CreateModel(IAppointmentService appointmentService,
            IDoctorService doctorService,
            IPatientService patientService,
            IWorkingScheduleService workingScheduleService,
            IMedicalFacilityService facilityService,
             IMedicalServiceService medicalServiceService)
        {
            _appointmentService = appointmentService;
            _doctorService = doctorService;
            _patientService = patientService;
            _workingScheduleService = workingScheduleService;
            _facilityService = facilityService;
            _medicalServiceService = medicalServiceService;
        }


        public async Task<IActionResult> OnGet(string doctorId, string facilityId)
        {
            var schedules = await _workingScheduleService.GetAvailableWorkingScheduleOfDoctor(doctorId);
            var selectedItem = schedules.Select(ws => new SelectListItem
            {
                Value = ws.ScheduleId,
                Text = $"{ws.StartTime:hh\\:mm} - {ws.EndTime:hh\\:mm} | {ws.WorkDate:yyyy-MM-dd}"
            }).ToList();

            WorkingSchedules = new SelectList(selectedItem, "Value", "Text");

            Appointment = new Appointment { Status = "Scheduled" };

            var facilities = await _facilityService.GetAllFacilitiesAsync();
            ViewData["Facilities"] = new SelectList(facilities, "FacilityId", "FacilityName");

            // Chỉ lấy bác sĩ thuộc cơ sở đã chọn
            if (!string.IsNullOrEmpty(facilityId))
            {
                var doctors = (await _doctorService.GetAllDoctorsAsync())
                    .Where(d => d.FacilityId == facilityId)
                    .Select(d => new
                    {
                        d.DoctorId,
                        Name = d.User?.FullName ?? "Unnamed Doctor"
                    }).ToList();

                ViewData["DoctorId"] = new SelectList(doctors, "DoctorId", "Name");
            }
            else
            {
                ViewData["DoctorId"] = new SelectList(new List<SelectListItem>());
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(userId))
            {
                var patient = await _patientService.GetPatientByUserIdAsync(userId);
                if (patient != null && HttpContext.Session.GetString("UserRole") == "Patient")
                {
                    Appointment.PatientId = patient.PatientId;
                }

                var services = await _medicalServiceService.GetAllServicesAsync();
                AvailableServices = new SelectList(services, "ServiceId", "ServiceName");

                return Page(); 
            }
            return RedirectToPage("/Account/Login");
        }



        public async Task<JsonResult> OnGetSchedulesAsync(string doctorId)
        {
            var schedules = await _workingScheduleService.GetAvailableWorkingScheduleOfDoctor(doctorId);

            var groupedSchedules = schedules
                .Where(ws =>ws.WorkDate > DateTime.Now)
                .GroupBy(ws => ws.WorkDate.ToString("yyyy-MM-dd")) // Nhóm theo ngày
                .Select(g => new
                {
                    Date = g.Key,
                    Schedules = g.Select(ws => new
                    {
                        Value = ws.ScheduleId,
                        Text = $"{ws.StartTime:hh\\:mm} - {ws.EndTime:hh\\:mm}"
                    }).ToList()
                })
                .ToList();

            return new JsonResult(groupedSchedules);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var patient = await _patientService.GetPatientByUserId(userId);
            if (patient == null)
            {
                return new JsonResult(new {isValid = false,message = "Patient not found..."})
            }
            Appointment.PatientId = patient.PatientId;

            var currentBook = await _appointmentService.GetCurrentBookAppointment(Appointment.PatientId, Appointment.DoctorId, Appointment.ScheduleId);
            if (currentBook != null)
            {
                return new JsonResult(new { isValid = false, message = "This day you have booked" });
            }

            bool isBooked = await _appointmentService.BookAppointmentAsync(Appointment);
            
            if (!isBooked)
            {
                return new JsonResult(new { isValid = false, message = "Failed to book appointment. Please try again." });
            }

            return new JsonResult(new { isValid = true, message = "Book appointment success" });
        }

        public async Task<IActionResult> OnGetDoctorsAsync(string facilityId)
        {
            if (string.IsNullOrEmpty(facilityId))
            {
                return new JsonResult(new List<SelectListItem>());
            }

            var doctor = (await _doctorService.GetAllDoctorsAsync());

            var doctors = doctor
                .Where(d => d.FacilityId == facilityId)
                .Select(d => new SelectListItem { Value = d.DoctorId, Text = d.User.FullName }).ToList();

            return new JsonResult(doctors);
        }
    }
}
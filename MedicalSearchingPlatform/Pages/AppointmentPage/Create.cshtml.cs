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

namespace MedicalSearchingPlatform.Pages.AppointmentPage
{
    public class CreateModel : PageModel
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IDoctorService _doctorService;
        private readonly IMedicalFacilityService _facilityService;
        private readonly IPatientService _patientService;
        private IWorkingScheduleService _workingScheduleService;

        [BindProperty]
        public Appointment Appointment { get; set; } = new Appointment();
        public SelectList WorkingSchedules { get; set; }

        public CreateModel(IAppointmentService appointmentService,
            IDoctorService doctorService,
            IPatientService patientService,
            IWorkingScheduleService workingScheduleService,
            IMedicalFacilityService facilityService)
        {
            _appointmentService = appointmentService;
            _doctorService = doctorService;
            _patientService = patientService;
            _workingScheduleService = workingScheduleService;
            _facilityService = facilityService;
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

            return Page();
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
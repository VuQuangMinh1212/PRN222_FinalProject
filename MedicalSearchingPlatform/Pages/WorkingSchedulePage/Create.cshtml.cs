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
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace MedicalSearchingPlatform.Pages.WorkingSchedulePage
{
    public class CreateModel : PageModel
    {
        private readonly IWorkingScheduleService _workingScheduleService;
        private readonly IDoctorService _doctorService;

        public CreateModel(IWorkingScheduleService workingScheduleService, IDoctorService doctorService)
        {
            _workingScheduleService = workingScheduleService;
            _doctorService = doctorService;
        }

        [BindProperty]
        public WorkingSchedule WorkingSchedule { get; set; } = new WorkingSchedule();

        public string DoctorId { get; set; }

        [BindProperty]
        public string SelectedWeek { get; set; }

        [BindProperty]
        public List<string> SelectedDays { get; set; }

        [BindProperty]
        public List<string> SelectedTimeSlots { get; set; }

        [BindProperty]
        public bool IsAvailable { get; set; }

        public async Task<IActionResult> OnGet()
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var doctor = (await _doctorService.GetAllDoctorsAsync()).Where(x => x.UserId == userId).FirstOrDefault();
            if (doctor != null)
            {
                DoctorId = doctor.DoctorId;
            }
            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var doctor = (await _doctorService.GetAllDoctorsAsync())
                            .FirstOrDefault(x => x.UserId == userId);

            if (doctor != null)
            {
                DoctorId = doctor.DoctorId;
            }

            if (string.IsNullOrEmpty(SelectedWeek) || SelectedTimeSlots == null || SelectedTimeSlots.Count == 0 || SelectedDays.IsNullOrEmpty())
            {
                //ModelState.AddModelError("Error", "Vui lòng chọn tuần và ít nhất một khung giờ và ngày.");
                return new JsonResult(new { isValid = false, errorMessage = "Vui lòng chọn tuần và ít nhất một khung giờ và ngày." });
            }

            DateTime firstDayOfWeek = GetStartDateOfWeek(SelectedWeek);

            var dayMapping = new Dictionary<string, int>
            {
                { "Monday", 0 }, { "Tuesday", 1 }, { "Wednesday", 2 },
                { "Thursday", 3 }, { "Friday", 4 }, { "Saturday", 5 }, { "Sunday", 6 }
            };

            var existingSchedules = await _workingScheduleService.GetSchedulesByDoctorAndWeek(DoctorId, firstDayOfWeek);

            var schedulesToInsert = new List<WorkingSchedule>();

            foreach (var day in SelectedDays)
            {
                if (!dayMapping.ContainsKey(day)) continue;

                DateTime workDate = firstDayOfWeek.AddDays(dayMapping[day]);

                foreach (var startTimeStr in SelectedTimeSlots)
                {
                    TimeSpan startTime = TimeSpan.Parse(startTimeStr);
                    TimeSpan endTime = startTime.Add(TimeSpan.FromHours(1));

                    bool isDuplicate = existingSchedules.Any(s => s.WorkDate == workDate && s.StartTime == startTime);
                    if (isDuplicate) continue;

                    schedulesToInsert.Add(new WorkingSchedule
                    {
                        ScheduleId = Guid.NewGuid().ToString(),
                        DoctorId = DoctorId,
                        WorkDate = workDate,
                        StartTime = startTime,
                        EndTime = endTime,
                        IsAvailable = IsAvailable
                    });
                }
            }

            if (schedulesToInsert.Count > 0)
            {
                await _workingScheduleService.CreateWorkingScheduleRange(schedulesToInsert);
            }

            return RedirectToPage("./Index");
        }


        private DateTime GetStartDateOfWeek(string selectedWeek)
        {
            var yearAndWeek = selectedWeek.Split("-W");
            int year = int.Parse(yearAndWeek[0]);
            int week = int.Parse(yearAndWeek[1]);

            var jan1 = new DateTime(year, 1, 1);
            var startOfWeek = jan1.AddDays((week - 1) * 7);

            while (startOfWeek.DayOfWeek != DayOfWeek.Monday)
            {
                startOfWeek = startOfWeek.AddDays(-1);
            }

            return startOfWeek;
        }
    }
}

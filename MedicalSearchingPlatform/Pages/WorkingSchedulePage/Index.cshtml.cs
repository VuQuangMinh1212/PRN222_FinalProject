using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MedicalSearchingPlatform.Data.DataContext;
using MedicalSearchingPlatform.Data.Entities;
using MedicalSearchingPlatform.Business.Interfaces;
using MedicalSearchingPlatform.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Security.Claims;

namespace MedicalSearchingPlatform.Pages.WorkingSchedulePage
{
    public class IndexModel : PageModel
    {
        private readonly IWorkingScheduleService _workingScheduleService;
        private readonly IDoctorService _doctorService;
        public IndexModel(IWorkingScheduleService workingScheduleService, IDoctorService doctorService)
        {
            _workingScheduleService = workingScheduleService;
            _doctorService = doctorService;
        }

        public IList<WorkingSchedule> WorkingSchedule { get; set; }
        public FilterScheduleViewModel FilterScheduleViewModel { get; set; } = new FilterScheduleViewModel();
        public string DoctorId { get; set; }

        public async Task OnGetAsync(string? selectedWeek)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var doctor = (await _doctorService.GetAllDoctorsAsync()).ToList().Where(x => x.UserId == userId).FirstOrDefault();
            DateTime currentMonday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 1);
            FilterScheduleViewModel.SelectedWeek = selectedWeek ?? currentMonday.ToString("yyyy-MM-dd");

            DateTime startOfWeek = DateTime.Parse(FilterScheduleViewModel.SelectedWeek);
            DateTime endOfWeek = startOfWeek.AddDays(6);

            FilterScheduleViewModel.Weeks = Enumerable.Range(-5, 10).Select(i => currentMonday.AddDays(i * 7)).ToList();

            if (doctor != null)
            {
                DoctorId = doctor.DoctorId;
                var workingSchedule = await _workingScheduleService.GetWorkingSchedulesByDoctorAndWeek(doctor.DoctorId, startOfWeek, endOfWeek);
                WorkingSchedule = workingSchedule.ToList();
            }
        }
    }
}

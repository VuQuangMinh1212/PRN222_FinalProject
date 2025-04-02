using MedicalSearchingPlatform.Data.Entities;

namespace MedicalSearchingPlatform.ViewModel
{
    public class FilterScheduleViewModel
    {
        public string? SelectedDoctorId { get; set; }
        public string? SelectedWeek { get; set; }
        public List<Doctor> Doctors { get; set; } = new();
        public List<DateTime> Weeks { get; set; } = new();
    }
}

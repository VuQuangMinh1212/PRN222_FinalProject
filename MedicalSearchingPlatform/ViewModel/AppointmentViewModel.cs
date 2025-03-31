namespace MedicalSearchingPlatform.Models
{
    public class AppointmentViewModel
    {
        public string AppointmentId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string AppointmentInfo { get; set; }
        public string Status { get; set; }
        public string DoctorName { get; set; }  // Extracted from Doctor
        public string PatientName { get; set; } // Extracted from Patient
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalSearchingPlatform.Data.Entities
{
    public class Appointment
    {
        [Key]
        public string AppointmentId { get; set; } = Guid.NewGuid().ToString();

        [Required, Display(Name = "Patient")]
        [ForeignKey("Patient")]
        public string PatientId { get; set; }
        public virtual Patient Patient { get; set; }

        [Required, Display(Name = "Doctor")]
        [ForeignKey("Doctor")]
        public string DoctorId { get; set; }
        public virtual Doctor Doctor { get; set; }

        [Required,ForeignKey("WorkingSchedule"),Display(Name = "Schedule")]
        public string ScheduleId { get; set; }
        public virtual WorkingSchedule WorkingSchedule { get; set; }

        [Required, Display(Name = " Appoiment Date")]
        public DateTime AppointmentDate { get; set; }

        [Required, Display(Name = "Appointment Info")]
        [MaxLength(500)]
        public string AppointmentInfo { get; set; }

        [Required]
        [MaxLength(50)]
        public string Status { get; set; } // Pending, Confirmed, Cancelled
    }
}

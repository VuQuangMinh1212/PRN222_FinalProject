using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalSearchingPlatform.Data.Entities
{
    public class Appointment
    {
        [Key]
        public string AppointmentId { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [ForeignKey("Patient")]
        public string PatientId { get; set; }
        public Patient Patient { get; set; }

        [Required]
        [ForeignKey("Doctor")]
        public string DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; }

        [MaxLength(500)]
        public string AppointmentInfo { get; set; }

        [Required]
        [MaxLength(50)]
        public string Status { get; set; } // Pending, Confirmed, Cancelled
    }
}

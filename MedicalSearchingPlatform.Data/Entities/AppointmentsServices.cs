using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MedicalSearchingPlatform.Data.Entities
{
    public class AppointmentsServices
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [ForeignKey("Appointment")]
        public string AppointmentId { get; set; }
        public virtual Appointment Appointment { get; set; }

        [Required]
        [ForeignKey("MedicalService")]
        public string ServiceId { get; set; }
        public virtual MedicalService MedicalService { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalSearchingPlatform.Data.Entities
{
    public class MedicalRecord
    {
        [Key]
        public string MedicalRecordId { get; set; } = Guid.NewGuid().ToString();

        [ForeignKey("Patient")]
        public string PatientId { get; set; }
        public Patient Patient { get; set; }

        [ForeignKey("Doctor")]
        public string DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        [Required]
        public DateTime RecordDate { get; set; } = DateTime.UtcNow;

        [MaxLength(1000)]
        public string Diagnosis { get; set; } 

        [MaxLength(1000)]
        public string Treatment { get; set; } 

        [MaxLength(1000)]
        public string Notes { get; set; }

        public string AttachmentUrl { get; set; } 

        public bool IsShared { get; set; } = false;
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalSearchingPlatform.Data.Entities
{
    public class Review
    {
        [Key]
        public string ReviewId { get; set; } = Guid.NewGuid().ToString("N");

        [Required]
        [ForeignKey("Patient")]
        public string PatientId { get; set; }
        public virtual Patient Patient { get; set; }

        [ForeignKey("Doctor")]
        public string? DoctorId { get; set; }
        public virtual Doctor? Doctor { get; set; }

        [ForeignKey("MedicalFacility")]
        public string? FacilityId { get; set; }
        public virtual MedicalFacility? MedicalFacility { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        [MaxLength(500)]
        [Required]
        public string Comment { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}

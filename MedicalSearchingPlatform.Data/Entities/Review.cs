using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalSearchingPlatform.Data.Entities
{
    public class Review
    {
        [Key]
        public string ReviewId { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [ForeignKey("Patient")]
        public string PatientId { get; set; }
        public Patient Patient { get; set; }

        [ForeignKey("Doctor")]
        public string? DoctorId { get; set; } // Nullable for facility reviews
        public Doctor? Doctor { get; set; }

        [ForeignKey("MedicalFacility")]
        public string? FacilityId { get; set; } // Nullable for doctor reviews
        public MedicalFacility? MedicalFacility { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        [MaxLength(500)]
        public string Comment { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}

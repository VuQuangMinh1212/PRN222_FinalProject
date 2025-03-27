using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalSearchingPlatform.Data.Entities
{
    public class Doctor
    {
        [Key]
        public string DoctorId { get; set; } = Guid.NewGuid().ToString();

        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("MedicalFacility")]
        public string FacilityId { get; set; }
        public MedicalFacility Facility { get; set; }

        public string Specialization { get; set; }
        public int ExperienceYears { get; set; }
        public string Qualifications { get; set; }
        public string Experience { get; set; }

        [MaxLength(255)]
        public string ImageUrl { get; set; } = "/img/doctors/doctors-1.jpg";

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // New fields
        [Column(TypeName = "decimal(10,2)")]
        public decimal Fee { get; set; }
        public ICollection<Review> Reviews { get; set; }

        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
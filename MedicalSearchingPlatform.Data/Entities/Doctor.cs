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
    }
}

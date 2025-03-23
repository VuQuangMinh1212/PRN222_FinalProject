using System.ComponentModel.DataAnnotations;

namespace MedicalSearchingPlatform.Data.Entities
{
    public class MedicalFacility
    {
        [Key]
        public string FacilityId { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(100)]
        public string FacilityName { get; set; }

        [Required]
        [MaxLength(255)]
        public string Address { get; set; }

        [MaxLength(15)]
        public string PhoneNumber { get; set; }

        public ICollection<MedicalFacilityService> FacilityServices { get; set; } = new List<MedicalFacilityService>();
    }
}

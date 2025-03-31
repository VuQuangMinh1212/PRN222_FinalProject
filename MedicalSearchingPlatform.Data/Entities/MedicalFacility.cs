using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [Required]
        [MaxLength(255)]
        public string Infor { get; set; }

        [MaxLength(15)]
        public string PhoneNumber { get; set; }

        [MaxLength(255)]
        public string ImageUrl { get; set; } = "/img/testimonials/departments-2.jpg";

        [NotMapped]
        public double AverageRating => Reviews?.Any() == true ? Reviews.Average(r => r.Rating) : 0;

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public virtual ICollection<MedicalFacilityService> FacilityServices { get; set; } = new List<MedicalFacilityService>();

        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}

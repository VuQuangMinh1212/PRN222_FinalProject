using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalSearchingPlatform.Data.Entities
{
    public class Patient
    {
        [Key]
        public string PatientId { get; set; } = Guid.NewGuid().ToString();

        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }
        public string MedicalHistory { get; set; }
        public string ConditionsToNote { get; set; }
        public ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();
    }
}

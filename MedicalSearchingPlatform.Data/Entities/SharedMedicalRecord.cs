using System.ComponentModel.DataAnnotations;

namespace MedicalSearchingPlatform.Data.Entities
{
    public class SharedMedicalRecord
    {
        [Key]
        public int SharedMedicalRecordId { get; set; }

        [Required(ErrorMessage = "Medical record is required.")]
        public int MedicalRecordId { get; set; }
        public MedicalRecord MedicalRecord { get; set; }

        [Required(ErrorMessage = "Doctor is required.")]
        public string DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        [Required(ErrorMessage = "Share date is required.")]
        [DataType(DataType.Date)]
        public DateTime ShareDate { get; set; }
    }
}
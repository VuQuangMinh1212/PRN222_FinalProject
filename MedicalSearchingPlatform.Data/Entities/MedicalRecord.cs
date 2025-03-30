using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalSearchingPlatform.Data.Entities
{
    public class MedicalRecord
    {
        [Key]
        public string MedicalRecordId { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [ForeignKey("Patient")]
        public string PatientId { get; set; }
        public Patient Patient { get; set; }

        [Required]
        [ForeignKey("Doctor")]
        public string DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        [Required]
        public DateTime RecordDate { get; set; } = DateTime.UtcNow;

        [MaxLength(1000)]
        public string Diagnosis { get; set; } // Chẩn đoán của bác sĩ

        [MaxLength(1000)]
        public string Treatment { get; set; } // Phương pháp điều trị

        [MaxLength(1000)]
        public string Notes { get; set; } // Ghi chú bổ sung

        // Quản lý tệp đính kèm (kết quả xét nghiệm, hình ảnh chẩn đoán)
        public string AttachmentUrl { get; set; } // Đường dẫn tới file (có thể lưu trên cloud như AWS S3)

        public bool IsShared { get; set; } = false; // Trạng thái chia sẻ với bác sĩ
    }
}
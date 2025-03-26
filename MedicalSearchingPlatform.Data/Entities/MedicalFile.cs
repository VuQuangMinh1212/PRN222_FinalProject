using System.ComponentModel.DataAnnotations;

namespace MedicalSearchingPlatform.Data.Entities
{
    public class MedicalFile
    {
        [Key]
        public int MedicalFileId { get; set; }

        [Required(ErrorMessage = "Medical record is required.")]
        public int MedicalRecordId { get; set; }
        public MedicalRecord MedicalRecord { get; set; }

        [Required(ErrorMessage = "File path is required.")]
        [StringLength(255, ErrorMessage = "File path cannot exceed 255 characters.")]
        public string FilePath { get; set; }

        [Required(ErrorMessage = "File type is required.")]
        [StringLength(50, ErrorMessage = "File type cannot exceed 50 characters.")]
        public string FileType { get; set; } // Ví dụ: "PDF", "Image"

        [Required(ErrorMessage = "Upload date is required.")]
        [DataType(DataType.Date)]
        public DateTime UploadDate { get; set; }
    }
}
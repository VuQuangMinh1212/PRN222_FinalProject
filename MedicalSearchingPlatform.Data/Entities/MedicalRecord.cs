using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MedicalSearchingPlatform.Data.Entities
{
    public class MedicalRecord
    {
        [Key]
        public int MedicalRecordId { get; set; }

        [Required(ErrorMessage = "Patient is required.")]
        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        [Required(ErrorMessage = "Diagnosis is required.")]
        [StringLength(500, ErrorMessage = "Diagnosis cannot exceed 500 characters.")]
        public string Diagnosis { get; set; }

        [Required(ErrorMessage = "Treatment is required.")]
        [StringLength(500, ErrorMessage = "Treatment cannot exceed 500 characters.")]
        public string Treatment { get; set; }

        [Required(ErrorMessage = "Record date is required.")]
        [DataType(DataType.Date)]
        public DateTime RecordDate { get; set; }

        public List<MedicalFile> MedicalFiles { get; set; } = new List<MedicalFile>();

        public List<SharedMedicalRecord> SharedMedicalRecords { get; set; } = new List<SharedMedicalRecord>();
    }
}
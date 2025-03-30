using MedicalSearchingPlatform.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicalSearchingPlatform.Services
{
    public interface IMedicalRecordService
    {
        Task<MedicalRecord> GetMedicalRecordAsync(string medicalRecordId);
        Task<IEnumerable<MedicalRecord>> GetPatientRecordsAsync(string patientId);
        Task<IEnumerable<MedicalRecord>> GetDoctorRecordsAsync(string doctorId);
        Task<MedicalRecord> CreateMedicalRecordAsync(MedicalRecord medicalRecord);
        Task UpdateMedicalRecordAsync(MedicalRecord medicalRecord);
        Task DeleteMedicalRecordAsync(string medicalRecordId);
        Task ShareRecordWithDoctorAsync(string medicalRecordId, bool isShared);
        Task UploadAttachmentAsync(string medicalRecordId, string attachmentUrl);
        Task<Patient> GetPatientByUserIdAsync(string userId);
        Task<IEnumerable<MedicalRecord>> GetAllRecordsAsync();
    }
}
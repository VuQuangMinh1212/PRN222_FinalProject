using MedicalSearchingPlatform.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicalSearchingPlatform.Business.Interfaces
{
    public interface IMedicalRecordService
    {
        Task<List<MedicalRecord>> GetMedicalRecordsByPatientAsync(int patientId);
        Task<MedicalRecord> GetMedicalRecordByIdAsync(int medicalRecordId);
        Task AddMedicalRecordAsync(MedicalRecord medicalRecord);
        Task UpdateMedicalRecordAsync(MedicalRecord medicalRecord);
        Task DeleteMedicalRecordAsync(int medicalRecordId);
        Task AddMedicalFileAsync(int medicalRecordId, string filePath, string fileType);
        Task ShareMedicalRecordWithDoctorAsync(int medicalRecordId, string doctorId);
        Task<List<SharedMedicalRecord>> GetSharedMedicalRecordsByDoctorAsync(string doctorId);
    }
}
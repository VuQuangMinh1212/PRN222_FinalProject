using MedicalSearchingPlatform.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicalSearchingPlatform.Data.Interfaces
{
    public interface IMedicalRecordRepository
    {
        Task<List<MedicalRecord>> GetMedicalRecordsByPatientAsync(int patientId);
        Task<MedicalRecord> GetMedicalRecordByIdAsync(int medicalRecordId);
        Task AddMedicalRecordAsync(MedicalRecord medicalRecord);
        Task UpdateMedicalRecordAsync(MedicalRecord medicalRecord);
        Task DeleteMedicalRecordAsync(int medicalRecordId);
        Task AddMedicalFileAsync(MedicalFile medicalFile);
        Task ShareMedicalRecordAsync(SharedMedicalRecord sharedMedicalRecord);
        Task<List<SharedMedicalRecord>> GetSharedMedicalRecordsByDoctorAsync(string doctorId);
    }
}
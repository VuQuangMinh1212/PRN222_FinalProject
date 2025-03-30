using MedicalSearchingPlatform.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicalSearchingPlatform.Data.Repositories
{
    public interface IMedicalRecordRepository
    {
        Task<MedicalRecord> GetByIdAsync(string medicalRecordId);
        Task<IEnumerable<MedicalRecord>> GetByPatientIdAsync(string patientId);
        Task<IEnumerable<MedicalRecord>> GetByDoctorIdAsync(string doctorId);
        Task<MedicalRecord> AddAsync(MedicalRecord medicalRecord);
        Task UpdateAsync(MedicalRecord medicalRecord);
        Task DeleteAsync(string medicalRecordId);
        Task<IEnumerable<MedicalRecord>> GetSharedRecordsAsync(string patientId);
        Task<Patient> GetPatientByUserIdAsync(string userId);
    }
}
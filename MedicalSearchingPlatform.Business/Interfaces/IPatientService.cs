using MedicalSearchingPlatform.Data.Entities;

namespace MedicalSearchingPlatform.Business.Interfaces
{
    public interface IPatientService
    {
        Task<Patient> GetPatientByUserId(string userId);
        Task<IEnumerable<Patient>> GetAllPatientsAsync();
        Task<Patient> GetPatientByIdAsync(string patientId);
        Task AddPatientAsync(Patient patient);
        Task UpdatePatientAsync(Patient patient);
        Task DeletePatientAsync(string patientId);
        Task<Patient> GetPatientByUserIdAsync(string userId);
    }
}

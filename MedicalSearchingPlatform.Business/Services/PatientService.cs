using MedicalSearchingPlatform.Business.Interfaces;
using MedicalSearchingPlatform.Data.Entities;
using MedicalSearchingPlatform.Data.IRepositories;

namespace MedicalSearchingPlatform.Business.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;

        public PatientService(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<IEnumerable<Patient>> GetAllPatientsAsync()
        {
            return await _patientRepository.GetAllPatientsAsync();
        }

        public async Task<Patient> GetPatientByIdAsync(string patientId)
        {
            return await _patientRepository.GetPatientByIdAsync(patientId);
        }

        public async Task<Patient> GetPatientByUserIdAsync(string patientId)
        {
            return await _patientRepository.GetPatientByUserIdAsync(patientId);
        }

        public async Task AddPatientAsync(Patient patient)
        {
            await _patientRepository.AddPatientAsync(patient);
        }

        public async Task UpdatePatientAsync(Patient patient)
        {
            await _patientRepository.UpdatePatientAsync(patient);
        }

        public async Task DeletePatientAsync(string patientId)
        {
            await _patientRepository.DeletePatientAsync(patientId);
        }
    }
}

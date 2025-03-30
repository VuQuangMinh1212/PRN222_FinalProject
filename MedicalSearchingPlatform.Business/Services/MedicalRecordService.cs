using MedicalSearchingPlatform.Data.Entities;
using MedicalSearchingPlatform.Data.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicalSearchingPlatform.Services
{
    public class MedicalRecordService : IMedicalRecordService
    {
        private readonly IMedicalRecordRepository _medicalRecordRepository;

        public MedicalRecordService(IMedicalRecordRepository medicalRecordRepository)
        {
            _medicalRecordRepository = medicalRecordRepository;
        }

        public async Task<MedicalRecord> GetMedicalRecordAsync(string medicalRecordId)
        {
            return await _medicalRecordRepository.GetByIdAsync(medicalRecordId);
        }

        public async Task<IEnumerable<MedicalRecord>> GetPatientRecordsAsync(string patientId)
        {
            return await _medicalRecordRepository.GetByPatientIdAsync(patientId);
        }

        public async Task<IEnumerable<MedicalRecord>> GetDoctorRecordsAsync(string doctorId)
        {
            return await _medicalRecordRepository.GetByDoctorIdAsync(doctorId);
        }

        public async Task<MedicalRecord> CreateMedicalRecordAsync(MedicalRecord medicalRecord)
        {
            return await _medicalRecordRepository.AddAsync(medicalRecord);
        }

        public async Task UpdateMedicalRecordAsync(MedicalRecord medicalRecord)
        {
            await _medicalRecordRepository.UpdateAsync(medicalRecord);
        }

        public async Task DeleteMedicalRecordAsync(string medicalRecordId)
        {
            await _medicalRecordRepository.DeleteAsync(medicalRecordId);
        }

        public async Task ShareRecordWithDoctorAsync(string medicalRecordId, bool isShared)
        {
            var record = await _medicalRecordRepository.GetByIdAsync(medicalRecordId);
            if (record != null)
            {
                record.IsShared = isShared;
                await _medicalRecordRepository.UpdateAsync(record);
            }
        }

        public async Task UploadAttachmentAsync(string medicalRecordId, string attachmentUrl)
        {
            var record = await _medicalRecordRepository.GetByIdAsync(medicalRecordId);
            if (record != null)
            {
                record.AttachmentUrl = attachmentUrl;
                await _medicalRecordRepository.UpdateAsync(record);
            }
        }
        public async Task<Patient> GetPatientByUserIdAsync(string userId)
        {
            return await _medicalRecordRepository.GetPatientByUserIdAsync(userId);
        }
        public async Task<IEnumerable<MedicalRecord>> GetAllRecordsAsync()
        {
            return await _medicalRecordRepository.GetAllRecordsAsync();
        }
        public async Task<IEnumerable<MedicalRecord>> GetAllMedicalRecordByDoctorIdAsync(string doctorId)
        {
            return await _medicalRecordRepository.GetAllMedicalRecordByDoctorIdAsync(doctorId);
        }
        public async Task<Doctor> GetDoctorByUserIdAsync(string userId)
        {
            return await _medicalRecordRepository.GetDoctorByUserIdAsync(userId);
        }
    }
}
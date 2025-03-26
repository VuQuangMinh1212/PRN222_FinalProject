using MedicalSearchingPlatform.Business.Interfaces;
using MedicalSearchingPlatform.Data;
using MedicalSearchingPlatform.Data.Entities;
using MedicalSearchingPlatform.Data.Interfaces;
using MedicalSearchingPlatform.Data.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicalSearchingPlatform.Business
{
    public class MedicalRecordService : IMedicalRecordService
    {
        private readonly IMedicalRecordRepository _medicalRecordRepository;
        private readonly IDoctorRepository _doctorRepository;

        public MedicalRecordService(IMedicalRecordRepository medicalRecordRepository, IDoctorRepository doctorRepository)
        {
            _medicalRecordRepository = medicalRecordRepository ?? throw new ArgumentNullException(nameof(medicalRecordRepository));
            _doctorRepository = doctorRepository;
        }

        public async Task<List<MedicalRecord>> GetMedicalRecordsByPatientAsync(int patientId)
        {
            return await _medicalRecordRepository.GetMedicalRecordsByPatientAsync(patientId);
        }

        public async Task<MedicalRecord> GetMedicalRecordByIdAsync(int medicalRecordId)
        {
            var record = await _medicalRecordRepository.GetMedicalRecordByIdAsync(medicalRecordId);
            if (record == null)
                throw new Exception("Medical record not found.");
            return record;
        }

        public async Task AddMedicalRecordAsync(MedicalRecord medicalRecord)
        {
            if (medicalRecord == null)
                throw new ArgumentNullException(nameof(medicalRecord));

            medicalRecord.RecordDate = DateTime.Now;
            await _medicalRecordRepository.AddMedicalRecordAsync(medicalRecord);
        }

        public async Task UpdateMedicalRecordAsync(MedicalRecord medicalRecord)
        {
            if (medicalRecord == null)
                throw new ArgumentNullException(nameof(medicalRecord));

            var existingRecord = await _medicalRecordRepository.GetMedicalRecordByIdAsync(medicalRecord.MedicalRecordId);
            if (existingRecord == null)
                throw new Exception("Medical record not found.");

            existingRecord.Diagnosis = medicalRecord.Diagnosis;
            existingRecord.Treatment = medicalRecord.Treatment;
            existingRecord.RecordDate = medicalRecord.RecordDate;

            await _medicalRecordRepository.UpdateMedicalRecordAsync(existingRecord);
        }

        public async Task DeleteMedicalRecordAsync(int medicalRecordId)
        {
            var record = await _medicalRecordRepository.GetMedicalRecordByIdAsync(medicalRecordId);
            if (record == null)
                throw new Exception("Medical record not found.");

            await _medicalRecordRepository.DeleteMedicalRecordAsync(medicalRecordId);
        }

        public async Task AddMedicalFileAsync(int medicalRecordId, string filePath, string fileType)
        {
            var record = await _medicalRecordRepository.GetMedicalRecordByIdAsync(medicalRecordId);
            if (record == null)
                throw new Exception("Medical record not found.");

            var medicalFile = new MedicalFile
            {
                MedicalRecordId = medicalRecordId,
                FilePath = filePath,
                FileType = fileType,
                UploadDate = DateTime.Now
            };

            await _medicalRecordRepository.AddMedicalFileAsync(medicalFile);
        }

        public async Task ShareMedicalRecordWithDoctorAsync(int medicalRecordId, string doctorId)
        {
            var record = await _medicalRecordRepository.GetMedicalRecordByIdAsync(medicalRecordId);
            if (record == null)
                throw new Exception("Medical record not found.");

            var doctor = await _doctorRepository.GetDoctorByIdAsync(doctorId);
            if (doctor == null)
                throw new Exception("Doctor not found.");

            var sharedRecord = new SharedMedicalRecord
            {
                MedicalRecordId = medicalRecordId,
                DoctorId = doctorId,
                ShareDate = DateTime.Now
            };

            await _medicalRecordRepository.ShareMedicalRecordAsync(sharedRecord);
        }

        public async Task<List<SharedMedicalRecord>> GetSharedMedicalRecordsByDoctorAsync(string doctorId)
        {
            return await _medicalRecordRepository.GetSharedMedicalRecordsByDoctorAsync(doctorId);
        }
    }
}
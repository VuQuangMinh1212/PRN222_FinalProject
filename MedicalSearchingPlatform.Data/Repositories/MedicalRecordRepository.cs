using MedicalSearchingPlatform.Data.DataContext;
using MedicalSearchingPlatform.Data.Entities;
using MedicalSearchingPlatform.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalSearchingPlatform.Data
{
    public class MedicalRecordRepository : IMedicalRecordRepository
    {
        private readonly ApplicationDbContext _context;

        public MedicalRecordRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<MedicalRecord>> GetMedicalRecordsByPatientAsync(int patientId)
        {
            return await _context.MedicalRecords
                .Include(mr => mr.MedicalFiles)
                .Include(mr => mr.Patient)
                .Where(mr => mr.PatientId == patientId)
                .OrderByDescending(mr => mr.RecordDate)
                .ToListAsync();
        }

        public async Task<MedicalRecord> GetMedicalRecordByIdAsync(int medicalRecordId)
        {
            return await _context.MedicalRecords
                .Include(mr => mr.MedicalFiles)
                .Include(mr => mr.Patient)
                .FirstOrDefaultAsync(mr => mr.MedicalRecordId == medicalRecordId);
        }

        public async Task AddMedicalRecordAsync(MedicalRecord medicalRecord)
        {
            _context.MedicalRecords.Add(medicalRecord);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMedicalRecordAsync(MedicalRecord medicalRecord)
        {
            _context.MedicalRecords.Update(medicalRecord);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMedicalRecordAsync(int medicalRecordId)
        {
            var medicalRecord = await _context.MedicalRecords.FindAsync(medicalRecordId);
            if (medicalRecord != null)
            {
                _context.MedicalRecords.Remove(medicalRecord);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddMedicalFileAsync(MedicalFile medicalFile)
        {
            _context.MedicalFiles.Add(medicalFile);
            await _context.SaveChangesAsync();
        }

        public async Task ShareMedicalRecordAsync(SharedMedicalRecord sharedMedicalRecord)
        {
            _context.SharedMedicalRecords.Add(sharedMedicalRecord);
            await _context.SaveChangesAsync();
        }

        public async Task<List<SharedMedicalRecord>> GetSharedMedicalRecordsByDoctorAsync(string doctorId)
        {
            return await _context.SharedMedicalRecords
                .Include(smr => smr.MedicalRecord)
                .ThenInclude(mr => mr.MedicalFiles)
                .Include(smr => smr.Doctor)
                .Where(smr => smr.DoctorId == doctorId)
                .OrderByDescending(smr => smr.ShareDate)
                .ToListAsync();
        }
    }
}
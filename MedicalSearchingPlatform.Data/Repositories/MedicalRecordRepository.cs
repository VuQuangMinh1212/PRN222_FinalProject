using MedicalSearchingPlatform.Data.DataContext;
using MedicalSearchingPlatform.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicalSearchingPlatform.Data.Repositories
{
    public class MedicalRecordRepository : IMedicalRecordRepository
    {
        private readonly ApplicationDbContext _context;

        public MedicalRecordRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<MedicalRecord> GetByIdAsync(string medicalRecordId)
        {
            return await _context.MedicalRecords
                .Include(m => m.Patient)
                .Include(m => m.Doctor)
                .FirstOrDefaultAsync(m => m.MedicalRecordId == medicalRecordId);
        }

        public async Task<IEnumerable<MedicalRecord>> GetByPatientIdAsync(string patientId)
        {
            return await _context.MedicalRecords
                .Include(m => m.Doctor)
                .Where(m => m.PatientId == patientId)
                .ToListAsync();
        }

        public async Task<IEnumerable<MedicalRecord>> GetByDoctorIdAsync(string doctorId)
        {
            return await _context.MedicalRecords
                .Include(m => m.Patient)
                .Where(m => m.DoctorId == doctorId)
                .ToListAsync();
        }

        public async Task<IEnumerable<MedicalRecord>> GetSharedRecordsAsync(string patientId)
        {
            return await _context.MedicalRecords
                .Where(m => m.PatientId == patientId && m.IsShared)
                .ToListAsync();
        }

        public async Task<MedicalRecord> AddAsync(MedicalRecord medicalRecord)
        {
            _context.MedicalRecords.Add(medicalRecord);
            await _context.SaveChangesAsync();
            return medicalRecord;
        }

        public async Task UpdateAsync(MedicalRecord medicalRecord)
        {
            _context.MedicalRecords.Update(medicalRecord);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string medicalRecordId)
        {
            var record = await _context.MedicalRecords.FindAsync(medicalRecordId);
            if (record != null)
            {
                _context.MedicalRecords.Remove(record);
                await _context.SaveChangesAsync();
            }
        }
    }
}
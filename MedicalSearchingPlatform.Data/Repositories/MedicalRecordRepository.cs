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
                .Include(m => m.Patient).ThenInclude(p => p.User)
                .Include(m => m.Doctor).ThenInclude(p => p.User)
                .FirstOrDefaultAsync(m => m.MedicalRecordId == medicalRecordId);
        }

        public async Task<IEnumerable<MedicalRecord>> GetByPatientIdAsync(string patientId)
        {
            return await _context.MedicalRecords
                .Include(m => m.Doctor).ThenInclude(d => d.User) 
                .Include(m => m.Patient).ThenInclude(p => p.User)
                .Where(m => m.PatientId == patientId)
                .ToListAsync();
        }

        public async Task<IEnumerable<MedicalRecord>> GetByDoctorIdAsync(string doctorId)
        {
            return await _context.MedicalRecords
                .Include(m => m.Doctor).ThenInclude(d => d.User) 
                .Include(m => m.Patient).ThenInclude(p => p.User)
                .Where(m => m.DoctorId == doctorId)
                .ToListAsync();
        }

        public async Task<IEnumerable<MedicalRecord>> GetSharedRecordsAsync(string patientId)
        {
            return await _context.MedicalRecords
                .Include(m => m.Doctor).ThenInclude(d => d.User) 
                .Include(m => m.Patient).ThenInclude(p => p.User)
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
        public async Task<Patient> GetPatientByUserIdAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException(nameof(userId));
            }

            return await _context.Patients
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.UserId == userId);
        }

        public async Task<IEnumerable<MedicalRecord>> GetAllRecordsAsync()
        {
            return await _context.MedicalRecords
                .Include(m => m.Doctor).ThenInclude(d => d.User) 
                .Include(m => m.Patient).ThenInclude(p => p.User)
                .ToListAsync();
        }
        public async Task<IEnumerable<MedicalRecord>> GetAllMedicalRecordByDoctorIdAsync(string doctorId)
        {
            if (string.IsNullOrEmpty(doctorId))
            {
                throw new ArgumentNullException(nameof(doctorId));
            }

            return await _context.MedicalRecords
                .Include(m => m.Patient).ThenInclude(p => p.User)
                .Include(m => m.Doctor).ThenInclude(d => d.User)
                .Where(m => m.DoctorId == doctorId)
                .ToListAsync();
        }

        public async Task<Doctor> GetDoctorByUserIdAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException(nameof(userId));
            }

            return await _context.Doctors
                 .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.UserId == userId);
        }

    }
}
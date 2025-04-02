using MedicalSearchingPlatform.Data.DataContext;
using MedicalSearchingPlatform.Data.Entities;
using MedicalSearchingPlatform.Data.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace MedicalSearchingPlatform.Data.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly ApplicationDbContext _context;

        public PatientRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Patient>> GetAllPatientsAsync()
        {
            return await _context.Patients.Include(p => p.User).ToListAsync();
        }

        public async Task<Patient> GetPatientByIdAsync(string patientId)
        {
            return await _context.Patients.Include(p => p.User)
                                          .FirstOrDefaultAsync(p => p.PatientId == patientId);
        }

        public async Task<Patient> GetPatientByUserIdAsync(string userId)
        {
            return await _context.Patients.Include(p => p.User)
                                          .FirstOrDefaultAsync(p => p.UserId == userId);
        }

        public async Task AddPatientAsync(Patient patient)
        {
            await _context.Patients.AddAsync(patient);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePatientAsync(Patient patient)
        {
            _context.Patients.Update(patient);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePatientAsync(string patientId)
        {
            var patient = await _context.Patients.FindAsync(patientId);
            if (patient != null)
            {
                _context.Patients.Remove(patient);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Patient> GetPatientByUserId(string userId)
        {
            return await _context.Patients.FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task<int> GetTotalPatientsAsync()
        {
            return await _context.Patients.CountAsync();
        }
    }
}

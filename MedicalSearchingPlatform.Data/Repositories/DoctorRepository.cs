using MedicalSearchingPlatform.Data.DataContext;
using MedicalSearchingPlatform.Data.Entities;
using MedicalSearchingPlatform.Data.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace MedicalSearchingPlatform.Data.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly ApplicationDbContext _context;

        public DoctorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Doctor>> GetAllDoctorsAsync()
        {
            return await _context.Doctors
                .Include(d => d.User)
                .Include(d => d.Facility)
                .OrderByDescending(d => d.CreatedAt)
                .ToListAsync();
        }

        public async Task<Doctor> GetDoctorByIdAsync(string doctorId)
        {
            return await _context.Doctors
                .Include(d => d.User)
                .Include(d => d.Facility)
                .FirstOrDefaultAsync(d => d.DoctorId == doctorId);
        }

        public async Task AddDoctorAsync(Doctor doctor)
        {
            doctor.CreatedAt = DateTime.UtcNow;
            doctor.ImageUrl = $"/img/doctors/doctors-{new Random().Next(1, 5)}.jpg";
            await _context.Doctors.AddAsync(doctor);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDoctorAsync(Doctor doctor)
        {
            _context.Doctors.Update(doctor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDoctorAsync(string doctorId)
        {
            var doctor = await _context.Doctors.FindAsync(doctorId);
            if (doctor != null)
            {
                _context.Doctors.Remove(doctor);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Doctor>> SearchDoctorsAsync(
            string name = null,
            string specialty = null,
            string facility = null,
            string expertise = null,
            double? minRating = null,
            decimal? maxFee = null)
        {
            var query = _context.Doctors
                .Include(d => d.User)
                .Include(d => d.Facility)
                .Include(d => d.Reviews)
                .AsQueryable();

            // Search by name (User.FullName)
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(d => d.User.FullName.Contains(name, StringComparison.OrdinalIgnoreCase));
            }

            // Search by specialty
            if (!string.IsNullOrEmpty(specialty))
            {
                query = query.Where(d => d.Specialization.Contains(specialty, StringComparison.OrdinalIgnoreCase));
            }

            // Search by facility (FacilityName)
            if (!string.IsNullOrEmpty(facility))
            {
                query = query.Where(d => d.Facility.FacilityName.Contains(facility, StringComparison.OrdinalIgnoreCase));
            }

            // Search by expertise (Experience or Qualifications)
            if (!string.IsNullOrEmpty(expertise))
            {
                query = query.Where(d => d.Experience.Contains(expertise, StringComparison.OrdinalIgnoreCase)
                    || d.Qualifications.Contains(expertise, StringComparison.OrdinalIgnoreCase));
            }

            // Search by minimum rating (average of Reviews.Rating)
            if (minRating.HasValue)
            {
                query = query.Where(d => d.Reviews.Any() ? d.Reviews.Average(r => r.Rating) >= minRating.Value : false);
            }

            // Search by maximum fee
            if (maxFee.HasValue)
            {
                query = query.Where(d => d.Fee <= maxFee.Value);
            }

            return await query.OrderByDescending(d => d.CreatedAt).ToListAsync();
        }
    }
}
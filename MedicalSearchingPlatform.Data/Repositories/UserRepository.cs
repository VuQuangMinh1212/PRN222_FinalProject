using MedicalSearchingPlatform.Data.DataContext;
using MedicalSearchingPlatform.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalSearchingPlatform.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public User GetUserById(int userId)
        {
            return _context.Users.Find(userId);
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void DeleteUser(int userId)
        {
            var user = _context.Users.Find(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        public async Task<User> GetUserByDoctorIdAsync(string doctorId)
        {
            return await _context.Users
                .Include(u => u.Doctor)
                .Where(u => u.Doctor != null && u.Doctor.DoctorId == doctorId)
                .FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByPatientIdAsync(string patientId)
        {
            return await _context.Users
                .Include(u => u.Patient)
                .Where(u => u.Patient != null && u.Patient.PatientId == patientId)
                .FirstOrDefaultAsync();
        }
    }
}

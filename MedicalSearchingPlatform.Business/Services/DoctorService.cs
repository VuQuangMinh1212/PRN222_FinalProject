using MedicalSearchingPlatform.Business.Interfaces;
using MedicalSearchingPlatform.Data.Entities;
using MedicalSearchingPlatform.Data.IRepositories;

namespace MedicalSearchingPlatform.Business.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorService(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public async Task<IEnumerable<Doctor>> GetAllDoctorsAsync()
        {
            var doctors = await _doctorRepository.GetAllDoctorsAsync();
            return doctors.OrderByDescending(d => d.CreatedAt);
        }

        public async Task<Doctor> GetDoctorByIdAsync(string doctorId)
        {
            return await _doctorRepository.GetDoctorByIdAsync(doctorId);
        }

        public async Task AddDoctorAsync(Doctor doctor)
        {
            doctor.CreatedAt = DateTime.UtcNow;
            doctor.ImageUrl = $"/img/doctors/doctors-{new Random().Next(1, 5)}.jpg";
            await _doctorRepository.AddDoctorAsync(doctor);
        }

        public async Task UpdateDoctorAsync(Doctor doctor)
        {
            await _doctorRepository.UpdateDoctorAsync(doctor);
        }

        public async Task DeleteDoctorAsync(string doctorId)
        {
            await _doctorRepository.DeleteDoctorAsync(doctorId);
        }

        public async Task<IEnumerable<Doctor>> SearchDoctorsAsync(
            string name = null,
            string specialty = null,
            string facility = null,
            string expertise = null,
            double? minRating = null,
            decimal? maxFee = null)
        {
            var doctors = await _doctorRepository.SearchDoctorsAsync(
                name, specialty, facility, expertise, minRating, maxFee);
            return doctors.OrderByDescending(d => d.CreatedAt);
        }

        public async Task<IEnumerable<Doctor>> GetMostBookedDoctorsAsync(int top)
        {
            return await _doctorRepository.GetMostBookedDoctorsAsync(top);
        }

        public async Task<Doctor> GetDoctorByUserId(string userId)
        {
            return await _doctorRepository.GetDoctorByUserId(userId);
        }
    }
}
using MedicalSearchingPlatform.Data.Entities;

namespace MedicalSearchingPlatform.Data.IRepositories
{
    public interface IDoctorRepository
    {
        Task<IEnumerable<Doctor>> GetAllDoctorsAsync();
        Task<Doctor> GetDoctorByIdAsync(string doctorId);
        Task AddDoctorAsync(Doctor doctor);
        Task UpdateDoctorAsync(Doctor doctor);
        Task DeleteDoctorAsync(string doctorId);

        Task<IEnumerable<Doctor>> SearchDoctorsAsync(string name,
            string specialty,
            string facility,
            string expertise,
            double? minRating,
            decimal? maxFee);

        Task<IEnumerable<Doctor>> GetMostBookedDoctorsAsync(int top);
        Task<Doctor> GetDoctorByUserIdAsync(string userId);
    }
}

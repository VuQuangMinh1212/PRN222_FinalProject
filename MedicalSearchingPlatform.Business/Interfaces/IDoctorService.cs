﻿using MedicalSearchingPlatform.Data.Entities;

namespace MedicalSearchingPlatform.Business.Interfaces
{
    public interface IDoctorService
    {
        Task<IEnumerable<Doctor>> GetAllDoctorsAsync();
        Task<Doctor> GetDoctorByUserId(string userId);
        Task<Doctor> GetDoctorByIdAsync(string doctorId);
        Task AddDoctorAsync(Doctor doctor);
        Task UpdateDoctorAsync(Doctor doctor);
        Task DeleteDoctorAsync(string doctorId);
        Task<IEnumerable<Doctor>> SearchDoctorsAsync(
        string name,
        string specialty,
        string facility,
        string expertise,
        double? minRating,
        decimal? maxFee);

        Task<IEnumerable<Doctor>> GetMostBookedDoctorsAsync(int top);
        Task<Doctor> GetDoctorByUserIdAsync(string userId);
    }


}

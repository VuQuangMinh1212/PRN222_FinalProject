﻿using MedicalSearchingPlatform.Data.Entities;

namespace MedicalSearchingPlatform.Data.IRepositories
{
    public interface IAppointmentRepository
    {
        Task<IEnumerable<Appointment>> GetAllAppointmentsAsync();
        Task<Appointment> GetCurrentBookAppointment(string patientId, string docterId, string scheduleId);
        Task<Appointment> GetAppointmentByIdAsync(string appointmentId);
        Task<Appointment> GetAppointmentByScheduleIdAsync(string scheduleId);
        Task<IEnumerable<Appointment>> GetAppointmentsByPatientIdAsync(string patientId);
        Task<IEnumerable<Appointment>> GetAppointmentsByDoctorIdAsync(string doctorId);
        Task AddAppointmentAsync(Appointment appointment);
        Task UpdateAppointmentAsync(Appointment appointment);
        Task DeleteAppointmentAsync(string appointmentId);
        Task<IEnumerable<(string MonthName, int Count)>> GetAppointmentCountByMonthAsync();

        Task<int> GetTotalAppointmentsAsync();

    }
}

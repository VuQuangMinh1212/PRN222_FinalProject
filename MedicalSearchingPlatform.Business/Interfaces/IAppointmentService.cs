using MedicalSearchingPlatform.Data.Entities;

namespace MedicalSearchingPlatform.Business.Interfaces
{
    public interface IAppointmentService
    {
        Task<IEnumerable<Appointment>> GetAllAppointmentsAsync();
        Task<Appointment> GetAppointmentByIdAsync(string appointmentId);
        Task<IEnumerable<Appointment>> GetAppointmentsByPatientIdAsync(string patientId);
        Task<IEnumerable<Appointment>> GetAppointmentsByDoctorIdAsync(string doctorId);
        Task<bool> BookAppointmentAsync(Appointment appointment);
        Task<bool> UpdateAppointmentAsync(Appointment appointment);
        Task<bool> CancelAppointmentAsync(string appointmentId);
        Task<IEnumerable<(string MonthName, int Count)>> GetAppointmentCountByMonthAsync();

    }
}

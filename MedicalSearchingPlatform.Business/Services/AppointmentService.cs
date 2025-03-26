using MedicalSearchingPlatform.Business.Interfaces;
using MedicalSearchingPlatform.Data.Entities;
using MedicalSearchingPlatform.Data.IRepositories;

namespace MedicalSearchingPlatform.Business.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<IEnumerable<Appointment>> GetAllAppointmentsAsync()
        {
            return await _appointmentRepository.GetAllAppointmentsAsync();
        }

        public async Task<Appointment> GetAppointmentByIdAsync(string appointmentId)
        {
            return await _appointmentRepository.GetAppointmentByIdAsync(appointmentId);
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByPatientIdAsync(string patientId)
        {
            return await _appointmentRepository.GetAppointmentsByPatientIdAsync(patientId);
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByDoctorIdAsync(string doctorId)
        {
            return await _appointmentRepository.GetAppointmentsByDoctorIdAsync(doctorId);
        }

        public async Task<bool> BookAppointmentAsync(Appointment appointment)
        {
            if (appointment == null) return false;

            await _appointmentRepository.AddAppointmentAsync(appointment);
            return true;
        }

        public async Task<bool> UpdateAppointmentAsync(Appointment appointment)
        {
            if (appointment == null) return false;

            await _appointmentRepository.UpdateAppointmentAsync(appointment);
            return true;
        }

        public async Task<bool> CancelAppointmentAsync(string appointmentId)
        {
            await _appointmentRepository.DeleteAppointmentAsync(appointmentId);
            return true;
        }
    }
}

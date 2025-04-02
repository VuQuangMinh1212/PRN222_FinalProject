using MedicalSearchingPlatform.Business.Hubs;
using MedicalSearchingPlatform.Business.Interfaces;
using MedicalSearchingPlatform.Data.Entities;
using MedicalSearchingPlatform.Data.IRepositories;
using Microsoft.AspNetCore.SignalR;

namespace MedicalSearchingPlatform.Business.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IHubContext<SignalRServer> _hubContext;

        public AppointmentService(IAppointmentRepository appointmentRepository, IHubContext<SignalRServer> hubContext)
        {
            _appointmentRepository = appointmentRepository;
            _hubContext = hubContext;
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
            await _hubContext.Clients.Group("Doctor").SendAsync("LoadDoctorAppointments");
            await _hubContext.Clients.Group("Patient").SendAsync("LoadPatientAppointments");
            return true;
        }

        public async Task<bool> UpdateAppointmentAsync(Appointment appointment)
        {
            if (appointment == null) return false;

            await _appointmentRepository.UpdateAppointmentAsync(appointment);
            await _hubContext.Clients.Group("Doctor").SendAsync("LoadDoctorAppointments");
            await _hubContext.Clients.Group("Patient").SendAsync("LoadPatientAppointments");
            return true;
        }

        public async Task<bool> CancelAppointmentAsync(string appointmentId)
        {
            await _appointmentRepository.DeleteAppointmentAsync(appointmentId);
            return true;
        }

        public async Task<IEnumerable<(string MonthName, int Count)>> GetAppointmentCountByMonthAsync()
        {
            return await _appointmentRepository.GetAppointmentCountByMonthAsync();
        }

        public async Task<Appointment> GetCurrentBookAppointment(string patientId, string doctorId, string scheduleId)
        {
            return await _appointmentRepository.GetCurrentBookAppointment(patientId, doctorId, scheduleId);
        }

        public async Task<Appointment> GetAppoimentByScheduleIdAsync(string scheduleId)
        {
            return await _appointmentRepository.GetAppointmentByScheduleIdAsync(scheduleId);
        }
    }
}

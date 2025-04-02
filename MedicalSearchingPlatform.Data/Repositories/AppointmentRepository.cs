using MedicalSearchingPlatform.Data.DataContext;
using MedicalSearchingPlatform.Data.Entities;
using MedicalSearchingPlatform.Data.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace MedicalSearchingPlatform.Data.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ApplicationDbContext _context;

        public AppointmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Appointment>> GetAllAppointmentsAsync()
        {
            return await _context.Appointments.Include(a => a.Patient).Include(a => a.Doctor).ThenInclude(a => a.User).ToListAsync();
        }

        public async Task<Appointment> GetAppointmentByIdAsync(string appointmentId)
        {
            return await _context.Appointments.Include(a => a.Patient)
                                              .ThenInclude(p => p.User)
                                              .Include(a => a.Doctor)
                                              .ThenInclude(d => d.User)
                                              .FirstOrDefaultAsync(a => a.AppointmentId == appointmentId);
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByPatientIdAsync(string patientId)
        {
            return await _context.Appointments.Where(a => a.PatientId == patientId).ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByDoctorIdAsync(string doctorId)
        {
            return await _context.Appointments
                .Include(x => x.Patient)
                    .ThenInclude(p => p.User)
                .Where(a => a.DoctorId == doctorId)
                .ToListAsync();
        }

        public async Task AddAppointmentAsync(Appointment appointment)
        {
            await _context.Appointments.AddAsync(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAppointmentAsync(Appointment appointment)
        {
            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAppointmentAsync(string appointmentId)
        {
            var appointment = await _context.Appointments.FindAsync(appointmentId);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<(string MonthName, int Count)>> GetAppointmentCountByMonthAsync()
        {
            return await Task.Run(() =>
         _context.Appointments
             .GroupBy(a => new { Year = a.AppointmentDate.Year, Month = a.AppointmentDate.Month })
             .Select(g => new
             {
                 Month = g.Key.Month,
                 Year = g.Key.Year,
                 Count = g.Count()
             })
             .OrderBy(e => e.Year)
             .ThenBy(e => e.Month)
             .AsEnumerable()
             .Select(e => ($"{e.Month}/{e.Year}", e.Count))
             .ToList()
     );
        }

        public async Task<Appointment> GetCurrentBookAppointment(string patientId, string docterId, string scheduleId)
        {
            return await _context.Appointments
                .Where(a => a.PatientId == patientId 
                    && a.DoctorId == docterId 
                    && a.ScheduleId == scheduleId)
                .FirstOrDefaultAsync();
        }
    }
}

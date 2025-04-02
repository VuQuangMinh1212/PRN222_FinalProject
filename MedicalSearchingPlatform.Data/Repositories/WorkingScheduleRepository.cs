using MedicalSearchingPlatform.Data.DataContext;
using MedicalSearchingPlatform.Data.Entities;
using MedicalSearchingPlatform.Data.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace MedicalSearchingPlatform.Data.Repositories
{
    public class WorkingScheduleRepository : IWorkingScheduleRepository
    {
        public readonly ApplicationDbContext _context;
        public WorkingScheduleRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task CreateWorkingSchedule(WorkingSchedule workingSchedule)
        {
            await _context.WorkingSchedules.AddAsync(workingSchedule);
            await _context.SaveChangesAsync();
        }

        public async Task CreateWorkingScheduleRange(List<WorkingSchedule> schedules)
        {
            await _context.WorkingSchedules.AddRangeAsync(schedules);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteWorkingSChedule(string id)
        {
            var workingSchedule = _context.WorkingSchedules.FirstOrDefault(x => x.ScheduleId == id);
            if (workingSchedule != null)
            {
                _context.WorkingSchedules.Remove(workingSchedule);
                _context.SaveChanges();
            }
        }

        public async Task<IEnumerable<WorkingSchedule>> GetAllWorkingSchedule()
        {
            return await _context.WorkingSchedules.ToListAsync();
        }

        public IQueryable<WorkingSchedule> GetAllWorkingScheduleQueryable()
        {
            return _context.WorkingSchedules.AsQueryable();
        }

        public async Task<List<WorkingSchedule>> GetAvailableWorkingScheduleOfDoctor(string doctorId)
        {
            var today = DateTime.Today;

            return await _context.WorkingSchedules
                .Where(ws => ws.DoctorId == doctorId
                             && ws.IsAvailable
                             && ws.WorkDate >= today)
                .OrderBy(ws => ws.WorkDate)
                .ThenBy(ws => ws.StartTime)
                .ToListAsync();
        }

        public async Task<List<WorkingSchedule>> GetSchedulesByDoctorAndWeek(string doctorId, DateTime weekStart)
        {
            DateTime weekEnd = weekStart.AddDays(6);

            return await _context.WorkingSchedules
                .Where(ws => ws.DoctorId == doctorId
                             && ws.WorkDate >= weekStart
                             && ws.WorkDate <= weekEnd)
                .ToListAsync();
        }

        public async Task<IList<WorkingSchedule>> GetWorkingScheduleByDoctorAndWeek(string doctorId, DateTime startOfWeek, DateTime endOfWeek)
        {
            return await _context.WorkingSchedules
                 .Where(ws => ws.DoctorId == doctorId &&
                              ws.WorkDate.Date >= startOfWeek.Date &&
                              ws.WorkDate.Date <= endOfWeek.Date)
                 .ToListAsync();
        }

        public async Task<WorkingSchedule> GetWorkingScheduleById(string id)
        {
            return await _context.WorkingSchedules.FindAsync(id);
        }

        public async Task UpdateWorkingSChedule(WorkingSchedule workingSchedule)
        {
            _context.WorkingSchedules.Update(workingSchedule);
            await _context.SaveChangesAsync();
        }
    }
}

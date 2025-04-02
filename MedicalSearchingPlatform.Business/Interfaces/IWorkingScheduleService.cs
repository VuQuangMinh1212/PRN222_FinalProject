using MedicalSearchingPlatform.Data.Entities;

namespace MedicalSearchingPlatform.Business.Interfaces
{
    public interface IWorkingScheduleService
    {
        Task<IList<WorkingSchedule>> GetWorkingSchedulesByDoctorAndWeek(string doctorId, DateTime startOfWeek, DateTime endOfweek);
        Task<List<WorkingSchedule>> GetAvailableWorkingScheduleOfDoctor(string doctorId);
        Task<List<WorkingSchedule>> GetSchedulesByDoctorAndWeek(string doctorId, DateTime weekStart);
        IQueryable<WorkingSchedule> GetAllWorkingScheduleQueryable();
        Task<IEnumerable<WorkingSchedule>> GetAllWorkingSchedule();
        Task<WorkingSchedule> GetWorkingScheduleById(string id);
        Task CreateWorkingSchedule(WorkingSchedule schedule);
        Task DeleteWorkingSchedule(string id);
        Task UpdateWorkingSchedule(WorkingSchedule schedule);
        Task CreateWorkingScheduleRange(List<WorkingSchedule> schedules);
    }
}

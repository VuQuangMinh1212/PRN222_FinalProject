using MedicalSearchingPlatform.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalSearchingPlatform.Data.IRepositories
{
    public interface IWorkingScheduleRepository
    {
        Task<IList<WorkingSchedule>> GetWorkingScheduleByDoctorAndWeek(string doctorId, DateTime startOfWeek, DateTime endOfWeek);
        Task<List<WorkingSchedule>> GetAvailableWorkingScheduleOfDoctor(string doctorId);
        Task<List<WorkingSchedule>> GetSchedulesByDoctorAndWeek(string doctorId, DateTime weekStart);
        IQueryable<WorkingSchedule> GetAllWorkingScheduleQueryable();
        Task<IEnumerable<WorkingSchedule>> GetAllWorkingSchedule();
        Task CreateWorkingSchedule(WorkingSchedule workingSchedule);
        Task<WorkingSchedule> GetWorkingScheduleById(string id);
        Task UpdateWorkingSChedule(WorkingSchedule workingSchedule);
        Task DeleteWorkingSChedule(string id);
        Task CreateWorkingScheduleRange(List<WorkingSchedule> schedules);
    }
}

using MedicalSearchingPlatform.Business.Hubs;
using MedicalSearchingPlatform.Business.Interfaces;
using MedicalSearchingPlatform.Data.Entities;
using MedicalSearchingPlatform.Data.IRepositories;
using Microsoft.AspNetCore.SignalR;

namespace MedicalSearchingPlatform.Business.Services
{
    public class WorkingScheduleService : IWorkingScheduleService
    {
        public readonly IWorkingScheduleRepository _workingScheduleRepository;
        public readonly IHubContext<SignalRServer> _hubContext;
        public WorkingScheduleService(IWorkingScheduleRepository workingScheduleRepository, IHubContext<SignalRServer> hubContext)
        {
            _workingScheduleRepository = workingScheduleRepository;
            _hubContext = hubContext;
        }
        public async Task CreateWorkingSchedule(WorkingSchedule schedule)
        {
            await _workingScheduleRepository.CreateWorkingSchedule(schedule);
        }

        public async Task CreateWorkingScheduleRange(List<WorkingSchedule> schedules)
        {
            await _workingScheduleRepository.CreateWorkingScheduleRange(schedules);
            await _hubContext.Clients.All.SendAsync("LoadAllSchedule");
        }

        public async Task DeleteWorkingSchedule(string id)
        {
            await _workingScheduleRepository.DeleteWorkingSChedule(id);
            await _hubContext.Clients.All.SendAsync("LoadAllSchedule");
        }

        public async Task<IEnumerable<WorkingSchedule>> GetAllWorkingSchedule()
        {
            return await _workingScheduleRepository.GetAllWorkingSchedule();
        }

        public IQueryable<WorkingSchedule> GetAllWorkingScheduleQueryable()
        {
            return _workingScheduleRepository.GetAllWorkingScheduleQueryable();
        }

        public async Task<List<WorkingSchedule>> GetAvailableWorkingScheduleOfDoctor(string doctorId)
        {
            return await _workingScheduleRepository.GetAvailableWorkingScheduleOfDoctor(doctorId);
        }

        public async Task<List<WorkingSchedule>> GetSchedulesByDoctorAndWeek(string doctorId, DateTime weekStart)
        {
            return await _workingScheduleRepository.GetSchedulesByDoctorAndWeek(doctorId, weekStart);
        }

        public Task<WorkingSchedule> GetWorkingScheduleById(string id)
        {
            return _workingScheduleRepository.GetWorkingScheduleById(id);
        }

        public async Task<IList<WorkingSchedule>> GetWorkingSchedulesByDoctorAndWeek(string doctorId, DateTime startOfWeek, DateTime endOfweek)
        {
            return await _workingScheduleRepository.GetWorkingScheduleByDoctorAndWeek(doctorId, startOfWeek, endOfweek);
        }

        public async Task UpdateWorkingSchedule(WorkingSchedule schedule)
        {
            await _workingScheduleRepository.UpdateWorkingSChedule(schedule);
            await _hubContext.Clients.All.SendAsync("LoadAllSchedule");
        }
    }
}

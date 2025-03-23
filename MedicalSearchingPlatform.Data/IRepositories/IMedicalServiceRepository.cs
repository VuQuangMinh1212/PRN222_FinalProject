using MedicalSearchingPlatform.Data.Entities;

namespace MedicalSearchingPlatform.Data.IRepositories
{
    public interface IMedicalServiceRepository
    {
        Task<IEnumerable<MedicalService>> GetAllAsync();
        Task<MedicalService> GetByIdAsync(string serviceId);
        Task AddAsync(MedicalService service);
        Task UpdateAsync(MedicalService service);
        Task DeleteAsync(string serviceId);
    }
}

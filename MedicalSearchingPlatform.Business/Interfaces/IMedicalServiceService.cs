using MedicalSearchingPlatform.Data.Entities;

namespace MedicalSearchingPlatform.Business.Interfaces
{
    public interface IMedicalServiceService
    {
        Task<IEnumerable<MedicalService>> GetAllServicesAsync();
        Task<MedicalService> GetServiceByIdAsync(string serviceId);
        Task AddServiceAsync(MedicalService service);
        Task UpdateServiceAsync(MedicalService service);
        Task DeleteServiceAsync(string serviceId);
    }
}

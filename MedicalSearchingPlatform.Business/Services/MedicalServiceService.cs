using MedicalSearchingPlatform.Business.Interfaces;
using MedicalSearchingPlatform.Data.Entities;
using MedicalSearchingPlatform.Data.IRepositories;

namespace MedicalSearchingPlatform.Business.Services
{
    public class MedicalServiceService : IMedicalServiceService
    {
        private readonly IMedicalServiceRepository _serviceRepo;

        public MedicalServiceService(IMedicalServiceRepository serviceRepo)
        {
            _serviceRepo = serviceRepo;
        }

        public async Task<IEnumerable<MedicalService>> GetAllServicesAsync() => await _serviceRepo.GetAllAsync();

        public async Task<MedicalService> GetServiceByIdAsync(string serviceId) => await _serviceRepo.GetByIdAsync(serviceId);

        public async Task AddServiceAsync(MedicalService service) => await _serviceRepo.AddAsync(service);

        public async Task UpdateServiceAsync(MedicalService service) => await _serviceRepo.UpdateAsync(service);

        public async Task DeleteServiceAsync(string serviceId) => await _serviceRepo.DeleteAsync(serviceId);
    }
}

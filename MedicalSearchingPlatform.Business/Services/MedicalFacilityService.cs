using MedicalSearchingPlatform.Business.Interfaces;
using MedicalSearchingPlatform.Data.Entities;
using MedicalSearchingPlatform.Data.IRepositories;

namespace MedicalSearchingPlatform.Business.Services
{
    public class MedicalFacilityService : IMedicalFacilityService
    {
        private readonly IMedicalFacilityRepository _facilityRepo;

        public MedicalFacilityService(IMedicalFacilityRepository facilityRepo)
        {
            _facilityRepo = facilityRepo;
        }

        public async Task<IEnumerable<MedicalFacility>> GetAllFacilitiesAsync() => await _facilityRepo.GetAllAsync();

        public async Task<MedicalFacility> GetFacilityByIdAsync(string facilityId) => await _facilityRepo.GetByIdAsync(facilityId);

        public async Task AddFacilityAsync(MedicalFacility facility) => await _facilityRepo.AddAsync(facility);

        public async Task UpdateFacilityAsync(MedicalFacility facility) => await _facilityRepo.UpdateAsync(facility);

        public async Task DeleteFacilityAsync(string facilityId) => await _facilityRepo.DeleteAsync(facilityId);
    }
}

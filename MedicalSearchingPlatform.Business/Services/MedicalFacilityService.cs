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

        public async Task<IEnumerable<MedicalFacility>> GetAllFacilitiesAsync()
        {
            var facilities = await _facilityRepo.GetAllAsync();
            return facilities.OrderByDescending(f => f.CreatedAt);
        }

        public async Task<MedicalFacility> GetFacilityByIdAsync(string facilityId)
        {
            return await _facilityRepo.GetByIdAsync(facilityId);
        }

        public async Task AddFacilityAsync(MedicalFacility facility)
        {
            facility.CreatedAt = DateTime.UtcNow;
            facility.ImageUrl = $"/img/departments/departments-{new Random().Next(1, 6)}.jpg";

            await _facilityRepo.AddAsync(facility);
        }

        public async Task UpdateFacilityAsync(MedicalFacility facility)
        {
            await _facilityRepo.UpdateAsync(facility);
        }

        public async Task DeleteFacilityAsync(string facilityId)
        {
            await _facilityRepo.DeleteAsync(facilityId);
        }

        public async Task<IEnumerable<MedicalFacility>> SearchFacilityAsync(
          string name,
          string address,
          string information,
          string phoneNumber)
        {
            var facilities = await _facilityRepo.SearchFacilityAsync(name, address, information, phoneNumber);
            return facilities.OrderByDescending(d => d.CreatedAt);
        }
    }
}

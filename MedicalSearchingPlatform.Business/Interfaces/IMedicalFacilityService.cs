using MedicalSearchingPlatform.Data.Entities;

namespace MedicalSearchingPlatform.Business.Interfaces
{
    public interface IMedicalFacilityService
    {
        Task<IEnumerable<MedicalFacility>> GetAllFacilitiesAsync();
        Task<MedicalFacility> GetFacilityByIdAsync(string facilityId);
        Task AddFacilityAsync(MedicalFacility facility);
        Task UpdateFacilityAsync(MedicalFacility facility);
        Task DeleteFacilityAsync(string facilityId);
        Task<IEnumerable<MedicalFacility>> SearchFacilityAsync(
          string name,
          string address,
          string information,
          string phoneNumber);
    }
}

using MedicalSearchingPlatform.Data.Entities;

namespace MedicalSearchingPlatform.Data.IRepositories
{
    public interface IMedicalFacilityRepository
    {
        Task<IEnumerable<MedicalFacility>> GetAllAsync();
        Task<MedicalFacility> GetByIdAsync(string facilityId);
        Task AddAsync(MedicalFacility facility);
        Task UpdateAsync(MedicalFacility facility);
        Task DeleteAsync(string facilityId);
        Task<IEnumerable<MedicalFacility>> SearchFacilityAsync(
          string name,
          string address,
          string information,
          string phoneNumber);
    }
}

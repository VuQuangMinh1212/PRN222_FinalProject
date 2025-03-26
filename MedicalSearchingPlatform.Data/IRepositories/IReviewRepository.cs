using MedicalSearchingPlatform.Data.Entities;

namespace MedicalSearchingPlatform.Data.Repositories
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetAllReviewsAsync();
        Task<Review> GetReviewByIdAsync(string reviewId);
        Task<IEnumerable<Review>> GetReviewsByDoctorIdAsync(string doctorId);
        Task<IEnumerable<Review>> GetReviewsByFacilityIdAsync(string facilityId);
        Task AddReviewAsync(Review review);
        Task UpdateReviewAsync(Review review);
        Task DeleteReviewAsync(string reviewId);
    }
}

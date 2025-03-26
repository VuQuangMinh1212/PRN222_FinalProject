using MedicalSearchingPlatform.Data.Entities;

namespace MedicalSearchingPlatform.Services
{
    public interface IReviewService
    {
        Task<IEnumerable<Review>> GetAllReviewsAsync();
        Task<Review> GetReviewByIdAsync(string reviewId);
        Task<IEnumerable<Review>> GetReviewsByDoctorIdAsync(string doctorId);
        Task<IEnumerable<Review>> GetReviewsByFacilityIdAsync(string facilityId);
        Task<bool> SubmitReviewAsync(Review review);
        Task<bool> DeleteReviewAsync(string reviewId);
    }
}

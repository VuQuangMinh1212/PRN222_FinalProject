using MedicalSearchingPlatform.Data.Entities;
using MedicalSearchingPlatform.Data.Repositories;
using MedicalSearchingPlatform.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicalSearchingPlatform.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<IEnumerable<Review>> GetAllReviewsAsync() => await _reviewRepository.GetAllReviewsAsync();

        public async Task<Review> GetReviewByIdAsync(string reviewId) => await _reviewRepository.GetReviewByIdAsync(reviewId);

        public async Task<IEnumerable<Review>> GetReviewsByDoctorIdAsync(string doctorId) => await _reviewRepository.GetReviewsByDoctorIdAsync(doctorId);

        public async Task<IEnumerable<Review>> GetReviewsByFacilityIdAsync(string facilityId) => await _reviewRepository.GetReviewsByFacilityIdAsync(facilityId);

        public async Task<bool> SubmitReviewAsync(Review review)
        {
            await _reviewRepository.AddReviewAsync(review);
            return true;
        }

        public async Task<bool> DeleteReviewAsync(string reviewId)
        {
            await _reviewRepository.DeleteReviewAsync(reviewId);
            return true;
        }
    }
}

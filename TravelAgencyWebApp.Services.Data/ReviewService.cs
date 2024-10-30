using TravelAgencyWebApp.Data.Models;
using TravelAgencyWebApp.Services.Data.Interfaces;

namespace TravelAgencyWebApp.Services.Data
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<IEnumerable<Review>> GetAllReviewsAsync()
        {
            return await _reviewRepository.GetAllAsync();
        }

        public async Task<Review?> GetReviewByIdAsync(int id)
        {
            return await _reviewRepository.GetByIdAsync(id);
        }

        public async Task AddReviewAsync(Review review)
        {
            await _reviewRepository.AddAsync(review);
        }

        public async Task UpdateReviewAsync(Review review)
        {
            await _reviewRepository.UpdateAsync(review);
        }

        public async Task DeleteReviewAsync(int id)
        {
        return await _reviewRepository.DeleteAsync(id);
        }

    }

}
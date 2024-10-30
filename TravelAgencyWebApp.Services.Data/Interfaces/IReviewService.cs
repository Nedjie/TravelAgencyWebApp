using TravelAgencyWebApp.Data.Models;

namespace TravelAgencyWebApp.Services.Data.Interfaces
{
    public interface IReviewService
    {
        Task<IEnumerable<Review>> GetAllReviewsAsync();
        Task<Review?> GetReviewByIdAsync(int id);
        Task AddReviewAsync(Review review);
        Task UpdateReviewAsync(Review review);
        Task DeleteReviewAsync(int id);
    }
}

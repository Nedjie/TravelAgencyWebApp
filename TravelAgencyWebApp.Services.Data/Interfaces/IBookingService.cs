using System.Linq.Expressions;
using TravelAgencyWebApp.Data.Models;
using TravelAgencyWebApp.ViewModels.Booking;

namespace TravelAgencyWebApp.Services.Data.Interfaces
{
    public interface IBookingService
    {
        Task<IEnumerable<BookingViewModel>> GetAllBookingsAsync();
        Task<BookingViewModel?> GetBookingByIdAsync(int id);
        Task<IEnumerable<BookingViewModel>> GetBookingsByUserIdAsync(Guid userId, params Expression<Func<Booking, object>>[] includes);
		Task<bool> CreateBookingAsync(CreateBookingViewModel model);
        Task<bool> UpdateBookingAsync(EditBookingViewModel model);
        Task DeleteBookingAsync(int id);
    }
}

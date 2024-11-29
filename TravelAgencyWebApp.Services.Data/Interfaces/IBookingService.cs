using TravelAgencyWebApp.Data.Models;
using TravelAgencyWebApp.ViewModels.Booking;

namespace TravelAgencyWebApp.Services.Data.Interfaces
{
    public interface IBookingService
    {
        Task<IEnumerable<BookingViewModel>> GetAllBookingsAsync();
        Task<BookingViewModel?> GetBookingByIdAsync(int id);
		Task<bool> CreateBookingAsync(CreateBookingViewModel model);
        Task UpdateBookingAsync(EditBookingViewModel model);
        Task DeleteBookingAsync(int id);
    }
}

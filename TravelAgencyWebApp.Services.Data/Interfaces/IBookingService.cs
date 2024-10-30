using TravelAgencyWebApp.Data.Models;

namespace TravelAgencyWebApp.Services.Data.Interfaces
{
    public interface IBookingService
    {
        Task<IEnumerable<Booking>> GetAllBookingsAsync();
        Task<Booking?> GetBookingByIdAsync(int id);
        Task AddBookingAsync(Booking booking);
        Task UpdateBookingAsync(Booking booking);
        Task DeleteBookingAsync(int id);
    }
}

using TravelAgencyWebApp.Common;
using TravelAgencyWebApp.Common.ErrorMessages;
using TravelAgencyWebApp.Data.Models;
using TravelAgencyWebApp.Data.Repository.Interfaces;
using TravelAgencyWebApp.Services.Data.Interfaces;
using TravelAgencyWebApp.ViewModels.Booking;

namespace TravelAgencyWebApp.Services.Data
{
    public class BookingService : IBookingService
    {
        private readonly IRepository<Booking, int> _bookingRepository;
        private readonly IRepository<Offer, int> _offerRepository;

        public BookingService(IRepository<Booking, int> bookingRepository, IRepository<Offer, int> offerRepository)
        {
            _bookingRepository = bookingRepository;
            _offerRepository = offerRepository;
        }

        public async Task<IEnumerable<BookingViewModel>> GetAllBookingsAsync()
        {
            var bookings = await _bookingRepository.GetAllAsync();
            return bookings.Select(b => new BookingViewModel
            {
                Id = b.Id,
                UserId = b.UserId,
                UserName = b.User != null ? b.User.Name : "Unknown",
                OfferId = b.OfferId,
                OfferTitle = b.Offer != null ? b.Offer.Title : "No Offer",
                CheckInDate = b.CheckInDate,
                CheckOutDate = b.CheckOutDate
            });
        }

        public async Task<BookingViewModel?> GetBookingByIdAsync(int id)
        {
            var booking = await _bookingRepository.GetByIdAsync(id);
            if (booking == null)
            {
                return null; // Returning null if no booking found
            }

            return new BookingViewModel
            {
                Id = booking.Id,
                UserId = booking.UserId,
                UserName = booking.User != null ? booking.User.Name : "Unknown",
                OfferId = booking.OfferId,
                OfferTitle = booking.Offer != null ? booking.Offer.Title : "No Offer",
                CheckInDate = booking.CheckInDate,
                CheckOutDate = booking.CheckOutDate
            };
        }

        public async Task AddBookingAsync(CreateBookingViewModel model)
        {
            if (model.CheckOutDate <= model.CheckInDate)
            {
                throw new ArgumentException(DataConstants.BookingCheckOutDateIsBeforeCheckInDateError);
            }

            var offer = await _offerRepository.GetByIdAsync(model.OfferId);
            if (offer == null)
            {
                throw new EntityNotFoundException($"Offer with ID {model.OfferId} not found.");
            }

            var booking = new Booking
            {
                UserId = model.UserId,
                CheckInDate = model.CheckInDate,
                CheckOutDate = model.CheckOutDate,
                OfferId = model.OfferId
            };

            await _bookingRepository.AddAsync(booking);
        }

        public async Task UpdateBookingAsync(EditBookingViewModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var existingBooking = await _bookingRepository.GetByIdAsync(model.Id);
            if (existingBooking == null)
            {
                throw new EntityNotFoundException($"Booking with ID {model.Id} not found.");
            }

            if (model.CheckOutDate <= model.CheckInDate)
            {
                throw new ArgumentException(DataConstants.BookingCheckOutDateIsBeforeCheckInDateError);
            }

            existingBooking.UserId = model.UserId;
            existingBooking.CheckInDate = model.CheckInDate;
            existingBooking.CheckOutDate = model.CheckOutDate;
            existingBooking.OfferId = model.OfferId; 

            await _bookingRepository.UpdateAsync(existingBooking);
        }

        public async Task DeleteBookingAsync(int id)
        {
            var booking = await _bookingRepository.GetByIdAsync(id);
            if (booking == null)
            {
                throw new EntityNotFoundException($"Booking with ID {id} not found.");
            }

            await _bookingRepository.DeleteAsync(booking);
        }

    }
}

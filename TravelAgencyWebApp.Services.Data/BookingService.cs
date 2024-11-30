using System.Linq.Expressions;
using TravelAgencyWebApp.Common;
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
        private readonly IRepository<ApplicationUser,Guid> _userRepository;

        public BookingService(IRepository<Booking, int> bookingRepository,
            IRepository<Offer, int> offerRepository,IRepository<ApplicationUser,Guid> userRepository)
        {
            _bookingRepository = bookingRepository;
            _offerRepository = offerRepository;
            _userRepository = userRepository;

        }

        public async Task<IEnumerable<BookingViewModel>> GetAllBookingsAsync()
        {
            var bookings = await _bookingRepository
                .GetAllIncludingAsync(b => b.Offer!); 

            return bookings.Select(b => new BookingViewModel
            {
                Id = b.Id,
                UserId = b.UserId.ToString(),
                UserName = b.User != null ? b.User.UserName : "Unknown",
                OfferId = b.OfferId,
                OfferTitle = b.Offer != null ? b.Offer.Title : "No Offer",
                CheckInDate = b.CheckInDate,
                CheckOutDate = b.CheckOutDate
            });
        }

		public async Task<BookingViewModel?> GetBookingByIdAsync(int id)
        {
            var booking = await _bookingRepository.GetIncludingAsync(id);
            if (booking == null)
            {
                return null; 
            }

            return new BookingViewModel
            {
                Id = booking.Id,
                UserId = booking.UserId.ToString(),
                UserName = booking.User != null ? booking.User.UserName : "Unknown",
                OfferId = booking.OfferId,
                OfferTitle = booking.Offer != null ? booking.Offer.Title : "No Offer",
                CheckInDate = booking.CheckInDate,
                CheckOutDate = booking.CheckOutDate
            };
        }

		public async Task<IEnumerable<BookingViewModel>> GetBookingsByUserIdAsync(Guid userId, params Expression<Func<Booking, object>>[] includes)
		{
			var bookings = await _bookingRepository.GetByUserIdAsync(userId,b => b.User, b => b.Offer);

			return bookings.Select(b => new BookingViewModel
			{
				Id = b.Id,
				CheckInDate = b.CheckInDate,
				CheckOutDate = b.CheckOutDate,
				OfferTitle = b.Offer?.Title ?? "No Offer Title" 
																
			}).ToList();
		}

		public async Task<bool> CreateBookingAsync(CreateBookingViewModel model)
        {
			var user = await _userRepository.GetByIdAsync(model.UserId);
			
            if (user == null)
			{
				throw new ArgumentException("User not found.");
			}

			if (model.CheckOutDate <= model.CheckInDate)
            {
                throw new ArgumentException(DataConstants.BookingCheckOutDateIsBeforeCheckInDateError);
            }

			var booking = new Booking
			{
				UserId = model.UserId,
				OfferId = model.OfferId,
				CheckInDate = model.CheckInDate,
				CheckOutDate = model.CheckOutDate,
				User=user
			};
			
			await _bookingRepository.AddAsync(booking);
            return true;
		}

        public async Task<bool> UpdateBookingAsync(EditBookingViewModel model)
        {
            ArgumentNullException.ThrowIfNull(model);          

            var existingBooking = await _bookingRepository.GetByIdAsync(model.Id);
            ArgumentNullException.ThrowIfNull(existingBooking);           

            if (model.CheckOutDate <= model.CheckInDate)
            {
                throw new ArgumentException(DataConstants.BookingCheckOutDateIsBeforeCheckInDateError);
            }

            Console.WriteLine($"Existing UserId: {existingBooking.UserId}");
            
            //existingBooking.UserId = model.UserId;
            existingBooking.CheckInDate = model.CheckInDate;
            existingBooking.CheckOutDate = model.CheckOutDate;
            existingBooking.OfferId = model.OfferId; 

            await _bookingRepository.UpdateAsync(existingBooking);
            return true;
        }

		public async Task<Booking?> GetBookingByIdIncludingUserAndOfferAsync(int id)
		{
			return await _bookingRepository.GetIncludingAsync(id, b => b.User!, b => b.Offer!);
		}

		public async Task<bool> DeleteBookingAsync(int id)
		{
			var booking = await _bookingRepository.GetByIdAsync(id);

			if (booking == null)
			{
				return false; 
			}

			return await _bookingRepository.DeleteAsync(booking);
		}
    }
}

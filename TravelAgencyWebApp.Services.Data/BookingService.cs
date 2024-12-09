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
        private readonly IRepository<ApplicationUser, Guid> _userRepository;
        private readonly IRepository<Agent, Guid> _agentRepository;

        public BookingService(IRepository<Booking, int> bookingRepository,
            IRepository<Offer, int> offerRepository, IRepository<ApplicationUser,
                Guid> userRepository, IRepository<Agent, Guid> agentRepository)
        {
            _bookingRepository = bookingRepository;
            _offerRepository = offerRepository;
            _userRepository = userRepository;
            _agentRepository = agentRepository;

        }

        public async Task<IEnumerable<BookingViewModel>> GetAllBookingsAsync()
        {
            var bookings = await _bookingRepository
                .GetAllIncludingAsync(b => b.Offer!);


            return bookings.Select(b => new BookingViewModel
            {
                Id = b.Id,
                UserId = b.UserId.ToString(),
                UserName = b.User!.FullName,
                ReservedByName = b.Agent!.FullName != null ? b.Agent.FullName : b.User.FullName,
                OfferId = b.OfferId,
                OfferTitle = b.Offer != null ? b.Offer.Title : "No Offer",
                CheckInDate = b.CheckInDate,
                CheckOutDate = b.CheckOutDate
            });
        }

        public async Task<IEnumerable<BookingViewModel>> GetBookingsForAgentAsync(Guid userId)
        {
            var bookings = await _bookingRepository.GetAllIncludingAsync(b => b.Offer!, b => b.Agent!, b => b.User!);

            var agents = await _agentRepository.GetByUserIdAsync(userId);

            var agent = agents.FirstOrDefault();

            if (agent == null)
            {
                Console.WriteLine($"No agent found for UserId: {userId}");
            }

            var filteredBookings = bookings
                 .Where(b => b.UserId == userId || b.AgentId == agent!.Id)
                .ToList();

            return filteredBookings.Select(b => new BookingViewModel
            {
                Id = b.Id,
                FullName = b.FullName,
                Email = b.Email,
                ReservedByName = (b.Agent != null && !string.IsNullOrEmpty(b.Agent.FullName))
                         ? b.Agent.FullName
                         : (b.User != null ? b.User.FullName : "Unknown User"),
                OfferId = b.OfferId,
                OfferTitle = b.Offer != null ? b.Offer.Title : "No Offer",
                CheckInDate = b.CheckInDate,
                CheckOutDate = b.CheckOutDate
            });
        }

        public async Task<BookingViewModel?> GetBookingByIdAsync(int id)
        {
            var booking = await _bookingRepository.GetIncludingAsync(id, b => b.User!, b => b.Agent!, b => b.Offer!);
            if (booking == null)
            {
                return null;
            }
            string reservedByName = booking.Agent != null && !string.IsNullOrWhiteSpace(booking.Agent.FullName)
                            ? booking.Agent.FullName
                            : (booking.User != null && !string.IsNullOrWhiteSpace(booking.User.FullName)
                            ? booking.User.FullName
                            : "No Information Available");

            return new BookingViewModel
            {
                Id = booking.Id,
                UserId = booking.UserId.ToString(),
                UserName = booking.User?.FullName ?? "Unknown User",
                ReservedByName = reservedByName,
                OfferId = booking.OfferId,
                OfferTitle = booking.Offer != null ? booking.Offer.Title : "No Offer",
                CheckInDate = booking.CheckInDate,
                CheckOutDate = booking.CheckOutDate
            };
        }

        public async Task<IEnumerable<BookingViewModel>> GetBookingsByUserIdAsync(Guid userId, params Expression<Func<Booking, object>>[] includes)
        {
            var bookings = await _bookingRepository.GetByUserIdAsync(userId, b => b.User!, b => b.Offer!);

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
            var user = await _userRepository.GetByIdAsync(model.UserId!.Value);
            if (model.UserId.HasValue)
            { 
                if (user == null)
                {
                    throw new ArgumentException("User not found.");
                }
            }

            if (Guid.TryParse(model.AgentId, out Guid parsedUserId))
            {
                var agents = await _agentRepository.GetByUserIdAsync(parsedUserId);
                if (agents == null || !agents.Any())
                {
                    Console.WriteLine($"No agent found for UserId: {parsedUserId}");
                    return false;
                }

                var agent = agents.FirstOrDefault();
                if (agent == null)
                {
                    Console.WriteLine($"No agent found for UserId: {parsedUserId}");
                    return false;
                }

                if (model.CheckOutDate <= model.CheckInDate)
                {
                    throw new ArgumentException(DataConstants.BookingCheckOutDateIsBeforeCheckInDateError);
                }
                if (model.UserId != null)
                {
                    var booking = new Booking
                    {
                        OfferId = model.OfferId,
                        CheckInDate = model.CheckInDate,
                        CheckOutDate = model.CheckOutDate,
                        FullName = user!.FullName,
                        Email = model.UserEmail,
                        AgentId = agent.Id,
                        UserId = model.UserId
                        
                    };
                    await _bookingRepository.AddAsync(booking);
                    return true;
                }
                else
                {
                    var booking = new Booking
                    {
                        OfferId = model.OfferId,
                        CheckInDate = model.CheckInDate,
                        CheckOutDate = model.CheckOutDate,
                        FullName = model.UserFullName,
                        Email = model.UserEmail,
                        AgentId = agent.Id                      
                    };
                    await _bookingRepository.AddAsync(booking);
                    return true;
                }
            }
            else
            {
                Console.WriteLine("Invalid format for AgentId");
            }
            return false;
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

            existingBooking.CheckInDate = model.CheckInDate;
            existingBooking.CheckOutDate = model.CheckOutDate;
            existingBooking.OfferId = model.OfferId;
            existingBooking.FullName = model.FullName;

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

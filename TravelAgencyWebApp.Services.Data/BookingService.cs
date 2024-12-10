using System.Linq.Expressions;
using TravelAgencyWebApp.Common;
using TravelAgencyWebApp.Data.Models;
using TravelAgencyWebApp.Data.Repository.Interfaces;
using TravelAgencyWebApp.Services.Data.Interfaces;
using TravelAgencyWebApp.ViewModels.Booking;
using TravelAgencyWebApp.ViewModels.Offer;

namespace TravelAgencyWebApp.Services.Data
{
	public class BookingService : IBookingService
	{
		private readonly IRepository<Booking, int> _bookingRepository;
		private readonly IRepository<ApplicationUser, Guid> _userRepository;
		private readonly IRepository<Agent, Guid> _agentRepository;

		public BookingService(IRepository<Booking, int> bookingRepository, IRepository<ApplicationUser,
				Guid> userRepository, IRepository<Agent, Guid> agentRepository)
		{
			_bookingRepository = bookingRepository;
			_userRepository = userRepository;
			_agentRepository = agentRepository;
		}

		public async Task<IEnumerable<BookingViewModel>> GetAllBookingsAsync()
		{
			var bookings = await _bookingRepository
				.GetAllIncludingAsync(b => b.Agent!, b => b.Offer!);

			return bookings.Select(b => new BookingViewModel
			{
				Id = b.Id,
				UserId = b.UserId != null ? b.UserId.ToString() : "Unregistered User",

				// Safely access User information
				UserName = b.User?.FullName ?? "Unknown User", // Provide a default value if User is null

				// Check for Agent and User FullName
				ReservedByName = b.Agent?.FullName ?? b.User?.FullName ?? "Unknown User", // Provide default value

				FullName=b.FullName,

				OfferId = b.OfferId,

				// Safeguard for the Offer Title
				OfferTitle = b.Offer?.Title ?? "No Offer", // Use null-conditional operator to safely access

				CheckInDate = b.CheckInDate,
				CheckOutDate = b.CheckOutDate
			}).ToList();
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

		public async Task<IEnumerable<string>> GetAllReservationHoldersAsync()
		{
			var bookings = await GetAllBookingsAsync(); 

			return bookings
				.Select(b => b.FullName!) 
				.Distinct()
				.Where(name => !string.IsNullOrWhiteSpace(name)) 
				.ToList();
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

		public async Task<IEnumerable<BookingViewModel>> SearchBookingsAsync(string searchTerm, string selectedReservationHolder)
		{
			var bookings = await GetAllBookingsAsync(); 

			var filteredBookings = bookings.AsQueryable();

			if (!string.IsNullOrWhiteSpace(searchTerm))
			{
				filteredBookings = filteredBookings.Where(b =>
					(b.AgentFullName != null && b.AgentFullName != null && b.AgentFullName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)) ||
					(b.UserFullName!= null && b.UserFullName != null && b.UserFullName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)) ||
					(b.Offer != null && b.Offer.Title!.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)));
			}

			if (!string.IsNullOrWhiteSpace(selectedReservationHolder))
			{
				filteredBookings = filteredBookings
					.Where(b =>
						(b.AgentFullName != null && b.AgentFullName.Equals(selectedReservationHolder, StringComparison.OrdinalIgnoreCase)) ||
						(b.UserFullName != null && b.UserFullName.Equals(selectedReservationHolder, StringComparison.OrdinalIgnoreCase)));
			}

			return filteredBookings.Select(b => new BookingViewModel
			{
				Id = b.Id,
				ReservedByName = b.ReservedByName ?? "Unknown User", 
				OfferTitle = b.OfferTitle ?? "No Title" 
			}).ToList();
		}

		public async Task<IEnumerable<BookingViewModel>> GetFilteredBookingsAsync(string searchTerm, string selectedReservationHolder)
		{
			var bookings = await GetAllBookingsAsync();

			// Filter by reservation holder if provided
			if (!string.IsNullOrWhiteSpace(selectedReservationHolder))
			{
				bookings = bookings
					.Where(b => b.ReservedByName != null && b.ReservedByName.Equals(selectedReservationHolder, StringComparison.OrdinalIgnoreCase))
					.ToList();
			}

			// Search term filtering
			if (!string.IsNullOrWhiteSpace(searchTerm))
			{
				bookings = bookings
					.Where(b => b.ReservedByName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
								 b.Offer.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
					.ToList();
			}

			return bookings.Select(b => new BookingViewModel
			{
				Id = b.Id,
				ReservedByName = b.ReservedByName ?? "Unknown User",
				Offer = new OfferViewModel { Title = b.Offer?.Title ?? "No Title" },
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

using Moq;
using NUnit.Framework;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TravelAgencyWebApp.Common;
using TravelAgencyWebApp.Data.Models;
using TravelAgencyWebApp.Data.Repository.Interfaces;
using TravelAgencyWebApp.Services.Data;
using TravelAgencyWebApp.ViewModels.Booking;

namespace TravelAgencyWebApp.Service.Tests
{
	[TestFixture]
	public class BookingServiceTests
	{
		private Mock<IRepository<Booking, int>>? _mockBookingRepository;
		private Mock<IRepository<ApplicationUser, Guid>>? _mockUserRepository;
		private Mock<IRepository<Agent, Guid>>? _mockAgentRepository;
		private BookingService? _bookingService;

		[SetUp]
		public void Setup()
		{
			_mockBookingRepository = new Mock<IRepository<Booking, int>>();
			_mockUserRepository = new Mock<IRepository<ApplicationUser, Guid>>();
			_mockAgentRepository = new Mock<IRepository<Agent, Guid>>();

			_bookingService = new BookingService(
				_mockBookingRepository.Object,
				_mockUserRepository.Object,
				_mockAgentRepository.Object
			);
		}

		[Test]
		public async Task GetAllBookingsAsync_ReturnsBookingViewModels()
		{
			// Arrange
			var userId1 = Guid.NewGuid();
			var userId2 = Guid.NewGuid();
			var user1 = new ApplicationUser { Id = userId1, FullName = "User One" };
			var user2 = new ApplicationUser { Id = userId2, FullName = "User Two" };

			var agent1 = new Agent { Id = Guid.NewGuid(), FullName = "Agent One" };
			var agent2 = new Agent { Id = Guid.NewGuid(), FullName = "Agent Two" };

			var offer1 = new Offer { Id = 1, Title = "Монако" };
			var offer2 = new Offer { Id = 2, Title = "Малта" };

			var bookings = new List<Booking>
	{
		new Booking
		{
			Id = 1,
			UserId = userId1,
			OfferId = 1,
			CheckInDate = DateTime.Now,
			CheckOutDate = DateTime.Now.AddDays(2),
			User = user1,
			Offer = offer1,
			Agent = agent1
		},
		new Booking
		{
			Id = 2,
			UserId = userId2,
			OfferId = 2,
			CheckInDate = DateTime.Now.AddDays(1),
			CheckOutDate = DateTime.Now.AddDays(3),
			User = user2,
			Offer = offer2,
			 Agent = agent2
		}
	};
			_mockBookingRepository?.Setup(repo => repo.GetAllIncludingAsync(b => b.Offer!)).ReturnsAsync(bookings);

			// Act
			var result = await _bookingService!.GetAllBookingsAsync();

			// Assert
			Assert.That(result.Count(), Is.EqualTo(2));
			var firstBooking = result.First();
			Assert.That(firstBooking.Id, Is.EqualTo(1));
			Assert.That(firstBooking.UserName, Is.EqualTo(user1.FullName));
			Assert.That(firstBooking.OfferTitle, Is.EqualTo(offer1.Title));
			Assert.That(result.First().ReservedByName, Is.EqualTo(agent1.FullName));
		}

		[Test]
		public async Task GetBookingByIdAsync_NonExistingBooking_ReturnsNull()
		{
			// Arrange
			var bookingId = 999;
			_mockBookingRepository?.Setup(repo => repo.GetIncludingAsync(bookingId, b => b.User!, b => b.Agent!, b => b.Offer!))
								  .ReturnsAsync((Booking)null!);

			// Act
			var result = await _bookingService!.GetBookingByIdAsync(bookingId);

			// Assert
			Assert.That(result, Is.Null);
		}

		[Test]
		public async Task GetBookingByIdAsync_ExistingBooking_ReturnsViewModel()
		{
			// Arrange
			var bookingId = 1;
			var userId = Guid.NewGuid();
			var booking = new Booking
			{
				Id = bookingId,
				UserId = userId,
				OfferId = 1,
				CheckInDate = DateTime.Now,
				CheckOutDate = DateTime.Now.AddDays(2),
				FullName = "Мартин Димитров",
				Email = "martin@gmail.com",
				User = new ApplicationUser { FullName = "Мартин Димитров" },
				Agent = new Agent { FullName = "Симеон Ангелов" },
				Offer = new Offer { Title = "Луксозно сафари" }
			};

			_mockBookingRepository?.Setup(repo => repo.GetIncludingAsync(bookingId, b => b.User!, b => b.Agent!, b => b.Offer!)).ReturnsAsync(booking);

			// Act
			var result = await _bookingService!.GetBookingByIdAsync(bookingId);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result!.Id, Is.EqualTo(bookingId));
			Assert.That(result.UserName, Is.EqualTo(booking.User.FullName));
			Assert.That(result.OfferTitle, Is.EqualTo(booking.Offer.Title));
			Assert.That(result.Email, Is.EqualTo(booking.User.Email));
			Assert.That(result.CheckInDate, Is.EqualTo(booking.CheckInDate));
			Assert.That(result.CheckOutDate, Is.EqualTo(booking.CheckOutDate));
		}

		[Test]
		public async Task GetBookingsByUserIdAsync_ReturnsViewModels()
		{
			// Arrange
			var userId = Guid.NewGuid();
			var offer1 = new Offer { Id = 1, Title = "Рим" };
			var offer2 = new Offer { Id = 2, Title = "Виена" };

			var bookings = new List<Booking>
	{
		new Booking { Id = 1, UserId = userId, OfferId = 1, Offer = offer1, CheckInDate = DateTime.Now, CheckOutDate = DateTime.Now.AddDays(2) },
		new Booking { Id = 2, UserId = userId, OfferId = 2, Offer = offer2, CheckInDate = DateTime.Now.AddDays(1), CheckOutDate = DateTime.Now.AddDays(3) }
	};

			_mockBookingRepository?.Setup(repo => repo.GetByUserIdAsync(userId, b => b.User!, b => b.Offer!)).ReturnsAsync(bookings);

			// Act
			var result = await _bookingService!.GetBookingsByUserIdAsync(userId);

			// Assert
			Assert.That(result, Has.Count.EqualTo(2));
			Assert.That(result.All(b => b.OfferTitle != "Барселона"), Is.True);
		}

		[Test]
		public async Task CreateBookingAsync_ValidModel_CreatesBooking()
		{
			// Arrange
			var model = new CreateBookingViewModel
			{
				UserId = Guid.NewGuid(),
				OfferId = 1,
				CheckInDate = DateTime.Now,
				CheckOutDate = DateTime.Now.AddDays(2),
				UserEmail = "martin@gmail.com",
				AgentId = Guid.NewGuid().ToString()
			};

			var user = new ApplicationUser { Id = model.UserId.Value, FullName = "Мартин Димитров" };
			var agent = new Agent { Id = Guid.Parse(model.AgentId) };

			_mockUserRepository?.Setup(repo => repo.GetByIdAsync(model.UserId.Value)).ReturnsAsync(user);
			_mockAgentRepository?.Setup(repo => repo.GetByUserIdAsync(agent.Id)).ReturnsAsync(new List<Agent> { agent });

			// Act
			var result = await _bookingService!.CreateBookingAsync(model);

			// Assert
			Assert.That(result, Is.True);
			_mockBookingRepository?.Verify(repo => repo.AddAsync(It.IsAny<Booking>()), Times.Once);
		}

		[Test]
		public async Task UpdateBookingAsync_ValidModel_UpdatesBooking()
		{
			// Arrange
			var bookingId = 1;
			var existingBooking = new Booking
			{
				Id = bookingId,
				UserId = Guid.NewGuid(),
				OfferId = 1,
				CheckInDate = DateTime.Now,
				CheckOutDate = DateTime.Now.AddDays(2)
			};

			var model = new EditBookingViewModel
			{
				Id = bookingId,
				CheckInDate = DateTime.Now.AddDays(1),
				CheckOutDate = DateTime.Now.AddDays(3),
				OfferId = 1,
				FullName = "Ново име"
			};

			_mockBookingRepository?.Setup(repo => repo.GetByIdAsync(bookingId)).ReturnsAsync(existingBooking);

			// Act
			var result = await _bookingService!.UpdateBookingAsync(model);

			// Assert
			Assert.That(result, Is.True);
			_mockBookingRepository?.Verify(repo => repo.UpdateAsync(existingBooking), Times.Once);
			Assert.That(existingBooking.CheckInDate, Is.EqualTo(model.CheckInDate));
			Assert.That(existingBooking.CheckOutDate, Is.EqualTo(model.CheckOutDate));
			Assert.That(existingBooking.CheckOutDate, Is.EqualTo(model.CheckOutDate));
			Assert.That(existingBooking.FullName, Is.EqualTo(model.FullName));
		}

		[Test]
		public async Task UpdateBookingAsync_CheckOutBeforeCheckIn_ThrowsArgumentException()
		{
			// Arrange
			var bookingId = 1;
			var existingBooking = new Booking
			{
				Id = bookingId,
				UserId = Guid.NewGuid(),
				OfferId = 1,
				CheckInDate = DateTime.Now,
				CheckOutDate = DateTime.Now.AddDays(2)
			};

			var model = new EditBookingViewModel
			{
				Id = bookingId,
				CheckInDate = DateTime.Now.AddDays(3), // Check-in after Check-out 
				CheckOutDate = DateTime.Now.AddDays(1), // Check-out before Check-in
				OfferId = 1,
				FullName = "Редактирано"
			};

			_mockBookingRepository?.Setup(repo => repo.GetByIdAsync(bookingId)).ReturnsAsync(existingBooking);

			// Act & Assert
			var exception =  Assert.ThrowsAsync<ArgumentException>(() => _bookingService!.UpdateBookingAsync(model));

			Assert.That(exception?.Message, Is.EqualTo(DataConstants.BookingCheckOutDateIsBeforeCheckInDateError));
		}

		[Test]
		public async Task GetBookingByIdIncludingUserAndOfferAsync_ReturnsBooking()
		{
			// Arrange
			var bookingId = 1;
			var booking = new Booking
			{
				Id = bookingId,
				UserId = Guid.NewGuid(),
				OfferId = 1,
				CheckInDate = DateTime.Now,
				CheckOutDate = DateTime.Now.AddDays(2)
			};

			_mockBookingRepository?.Setup(repo => repo.GetIncludingAsync(bookingId, b => b.User!, b => b.Offer!)).ReturnsAsync(booking);

			// Act
			var result = await _bookingService!.GetBookingByIdIncludingUserAndOfferAsync(bookingId);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result!.Id, Is.EqualTo(bookingId));
		}

		[Test]
		public async Task DeleteBookingAsync_BookingExists_ReturnsTrue()
		{
			// Arrange
			var bookingId = 1;
			var booking = new Booking { Id = bookingId };

			_mockBookingRepository?.Setup(repo => repo.GetByIdAsync(bookingId)).ReturnsAsync(booking);
			_mockBookingRepository?.Setup(repo => repo.DeleteAsync(booking)).ReturnsAsync(true);

			// Act
			var result = await _bookingService!.DeleteBookingAsync(bookingId);

			// Assert
			Assert.That(result, Is.True);
			_mockBookingRepository?.Verify(repo => repo.DeleteAsync(booking), Times.Once);
		}

		[Test]
		public async Task DeleteBookingAsync_BookingDoesNotExist_ReturnsFalse()
		{
			// Arrange
			var bookingId = 9999999;
			_mockBookingRepository?.Setup(repo => repo.GetByIdAsync(bookingId)).ReturnsAsync((Booking)null!);

			// Act
			var result = await _bookingService!.DeleteBookingAsync(bookingId);

			// Assert
			Assert.That(result, Is.False);
		}

		[Test]
		public async Task GetBookingsByUserIdAsync_NoBookings_ReturnsEmptyList()
		{
			// Arrange
			var userId = Guid.NewGuid();
			_mockBookingRepository?.Setup(repo => repo.GetByUserIdAsync(userId, It.IsAny<Expression<Func<Booking, object>>[]>()))
				.ReturnsAsync(new List<Booking>());

			// Act
			var result = await _bookingService!.GetBookingsByUserIdAsync(userId);

			// Assert
			Assert.That(result, Is.Empty);
		}

		[Test]
		public async Task CreateBookingAsync_WhenUserIsNull_ThrowsInvalidOperationException()
		{
			// Arrange
			var model = new CreateBookingViewModel
			{
				UserId = null,
				OfferId = 1,
				CheckInDate = DateTime.Now,
				CheckOutDate = DateTime.Now.AddDays(2),
				UserEmail = "martin@gmail.com",
				AgentId = Guid.NewGuid().ToString()
			};

			// Act & Assert
			var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
				await _bookingService!.CreateBookingAsync(model));

			// Assert
			Assert.That(ex!.Message, Is.EqualTo("Nullable object must have a value."));
		}

		[Test]
		public async Task UpdateBookingAsync_BookingDoesNotExist_ReturnsFalse()
		{
			// Arrange
			var model = new EditBookingViewModel
			{
				Id = 999,
				CheckInDate = DateTime.Now,
				CheckOutDate = DateTime.Now.AddDays(3),
				OfferId = 1,
				FullName = "Редактирано име>"
			};

			_mockBookingRepository?.Setup(repo => repo.GetByIdAsync(model.Id)).ReturnsAsync((Booking)null!);

			// Act
			bool result = false;

			try
			{
				result = await _bookingService!.UpdateBookingAsync(model);
			}
			catch (ArgumentNullException)
			{

			}

			// Assert
			Assert.That(result, Is.False);
		}

		[Test]
		public async Task UpdateBookingAsync_NullModel_ThrowsArgumentNullException()
		{
			// Act & Assert
			var ex = Assert.ThrowsAsync<ArgumentNullException>(async () =>
				await _bookingService!.UpdateBookingAsync(null!));

			Assert.That(ex?.ParamName, Is.EqualTo("model"));
		}
	}

}
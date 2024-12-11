using Moq;
using NUnit.Framework;
using System.Linq.Expressions;
using TravelAgencyWebApp.Data.Models;
using TravelAgencyWebApp.Data.Repository.Interfaces;
using TravelAgencyWebApp.Services.Data;

namespace TravelAgencyWebApp.Service.Tests
{
	[TestFixture]
	public class OfferServiceTests
	{
		private Mock<IRepository<Offer, int>>? _mockOfferRepository;
		private Mock<IRepository<TravelingWay, int>>? _mockTravelingWayRepository;
		private OfferService? _offerService;

		[SetUp]
		public void Setup()
		{
			_mockOfferRepository = new Mock<IRepository<Offer, int>>();
			_mockTravelingWayRepository = new Mock<IRepository<TravelingWay, int>>();
			_offerService = new OfferService(_mockOfferRepository.Object, _mockTravelingWayRepository.Object);
		}

		[Test]
		public async Task GetAllOffersAsync_ReturnsFilteredOffers()
		{
			// Arrange
			var offers = new List<Offer>
			{
				new Offer { Id = 1, Title = "Почивка", IsDeleted = false },
				new Offer { Id = 2, Title = "Екскурзия", IsDeleted = true }
			};

			_mockOfferRepository?
			  .Setup(repo => repo.GetAllIncludingAsync(It.IsAny<Expression<Func<Offer, object>>>()))
			  .ReturnsAsync(offers);

			// Act
			var result = await _offerService!.GetAllOffersAsync();

			// Assert
			Assert.That(result.Count(), Is.EqualTo(1));
			Assert.That(result.First().Title, Is.EqualTo("Почивка"));
		}

		[Test]
		public async Task GetFilteredOffersAsync_WithTitleSearch_ReturnsMatchingOffers()
		{
			// Arrange
			var offers = new List<Offer>
	{
		new Offer { Id = 1, Title = "Почивка в Доминикана", Description = "Страхотна оферта", IsDeleted = false },
		new Offer { Id = 2, Title = "Средиземноморски круиз", Description = "Страхотна оферта", IsDeleted = false }
	};
			_mockOfferRepository?
				.Setup(repo => repo.GetAllAsync())
				.ReturnsAsync(offers);

			// Act
			var (result, totalCount) = await _offerService!.GetFilteredOffersAsync("Почивка", null!, 1, 10);

			// Assert
			Assert.That(result.Count(), Is.EqualTo(1));
			Assert.That(result.First().Title, Is.EqualTo("Почивка в Доминикана"));
		}

		[Test]
		public async Task GetFilteredOffersAsync_WithTravelingWay_ReturnsMatchingOffers()
		{
			// Arrange
			var offers = new List<Offer>
	{
		new Offer { Id = 1, Title = "Сейшели", TravelingWay = new TravelingWay { Id = 1, Method = "Самолет" }, IsDeleted = false },
		new Offer { Id = 2, Title = "Букурещ", TravelingWay = new TravelingWay { Id = 2, Method = "Автобус" }, IsDeleted = false },
		new Offer { Id = 3, Title = "Мадагаскар", TravelingWay = new TravelingWay { Id = 1, Method = "Самолет" }, IsDeleted = false }
	};

			_mockOfferRepository?.Setup(repo => repo.GetAllAsync()).ReturnsAsync(offers);

			var selectedTravelingWay = "Самолет";

			// Act
			var (result, totalCount) = await _offerService!.GetFilteredOffersAsync(null!, selectedTravelingWay, 1, 10);

			// Assert
			Assert.That(result, Has.Count.EqualTo(2));
			Assert.That(result.All(o => o.TravelingWay?.Method == selectedTravelingWay), Is.True);
		}

		[Test]
		public async Task GetFilteredOffersAsync_NoFilters_ReturnsAllOffers()
		{
			// Arrange
			var offers = new List<Offer>
	{
		new Offer { Id = 1, Title = "Малдиви", TravelingWay = new TravelingWay { Method = "Самолет" }, IsDeleted = false },
		new Offer { Id = 2, Title = "Сейшели", TravelingWay = new TravelingWay { Method = "Автобус" }, IsDeleted = false }
	};

			// Setup the repository mock
			_mockOfferRepository?.Setup(repo => repo.GetAllAsync()).ReturnsAsync(offers);

			// Act
			var (result, totalCount) = await _offerService!.GetFilteredOffersAsync(null!, null!, 1, 10);

			// Assert
			Assert.That(result, Has.Count.EqualTo(2));
			Assert.That(result.Select(o => o.Title), Is.EquivalentTo(new[] { "Малдиви", "Сейшели" }));
			Assert.That(totalCount, Is.EqualTo(2));
		}

		[Test]
		public async Task GetOfferByIdAsync_ReturnsCorrectOffer()
		{
			// Arrange
			var offer = new Offer { Id = 1, Title = "Дубай" };

			_mockOfferRepository?
				.Setup(repo => repo.GetIncludingAsync(1, It.IsAny<Expression<Func<Offer, object>>>()))
				.ReturnsAsync(offer);

			// Act
			var result = await _offerService!.GetOfferByIdAsync(1);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result!.Id, Is.EqualTo(1));
			Assert.That(result.Title, Is.EqualTo("Дубай"));
		}

		[Test]
		public async Task AddOfferAsync_AddsOffer()
		{
			// Arrange
			var offer = new Offer { Title = "Флоренция", Description = "Сърцето на Тоскана", Price = 100 };

			// Act
			await _offerService!.AddOfferAsync(offer);

			// Assert
			_mockOfferRepository?.Verify(repo => repo.AddAsync(It.Is<Offer>(o => o.Title == "Флоренция")), Times.Once);
		}

		[Test]
		public async Task UpdateOfferAsync_UpdatesOffer()
		{
			// Arrange
			var existingOffer = new Offer { Id = 1, Title = "Мадрид", Description = "Европейски столици" };
			_mockOfferRepository?.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(existingOffer);

			var updatedOffer = new Offer { Id = 1, Title = "Мадрид - Страхотна оферта", Description = "Европейска столица на културата" };

			// Act
			await _offerService!.UpdateOfferAsync(updatedOffer);

			// Assert
			_mockOfferRepository?.Verify(repo => repo.UpdateAsync(It.Is<Offer>(o => o.Title == "Мадрид - Страхотна оферта")), Times.Once);
		}

		[Test]
		public void DeleteOfferAsync_OfferNotFound_ThrowsException()
		{
			// Arrange
			var nonExistentOfferId = 3;
			_mockOfferRepository?.Setup(repo => repo.GetByIdAsync(nonExistentOfferId)).ReturnsAsync((Offer)null!);

			// Act 
			var exception = Assert.ThrowsAsync<KeyNotFoundException>(async () =>
				await _offerService!.DeleteOfferAsync(nonExistentOfferId));

			// Assert
			Assert.That(exception?.Message, Is.EqualTo($"Offer with ID {nonExistentOfferId} was not found."));
		}

		[Test]
		public async Task DeleteOfferAsync_DeletesOffer()
		{
			// Arrange
			var offer = new Offer { Id = 1, Title = "Лондон", IsDeleted = false };
			_mockOfferRepository?.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(offer);

			// Act
			await _offerService!.DeleteOfferAsync(1);

			// Assert
			_mockOfferRepository?.Verify(repo => repo.DeleteAsync(It.Is<Offer>(o => o.Id == 1)), Times.Once);
		}


	}
}


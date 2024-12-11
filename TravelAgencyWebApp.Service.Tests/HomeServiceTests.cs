using Moq;
using NUnit.Framework;
using TravelAgencyWebApp.Data.Models;
using TravelAgencyWebApp.Data.Repository.Interfaces;
using TravelAgencyWebApp.Services.Data;

namespace TravelAgencyWebApp.Service.Tests
{
	[TestFixture]
	public class HomeServiceTests
	{
		private Mock<IRepository<Offer, int>>? _mockOfferRepository;
		private Mock<IRepository<TravelingWay, int>>? _mockTravelingWayRepository;
		private HomeService? _homeService;

		[SetUp]
		public void Setup()
		{
			_mockOfferRepository = new Mock<IRepository<Offer, int>>();
			_mockTravelingWayRepository = new Mock<IRepository<TravelingWay, int>>();
			_homeService = new HomeService(_mockOfferRepository.Object, _mockTravelingWayRepository.Object);
		}

		[Test]
		public async Task GetOffersAsync_ReturnsOffers()
		{
			// Arrange
			var offers = new List<Offer>
			{
				new Offer { Id = 1, Title = "Оман" },
				new Offer { Id = 2, Title = "Тенерифе" }
			};

			_mockOfferRepository?.Setup(repo => repo.GetAllAsync()).ReturnsAsync(offers);

			// Act
			var result = await _homeService!.GetOffersAsync();

			// Assert
			Assert.That(result, Has.Count.EqualTo(2));
			Assert.That(result.First().Title, Is.EqualTo("Оман"));
		}

		[Test]
		public async Task GetOffersGroupedByTravelingWayAsync_ReturnsGroupedOffers()
		{
			// Arrange
			var travelingWay1 = new TravelingWay { Id = 1, Method = "Самолет" };
			var travelingWay2 = new TravelingWay { Id = 2, Method = "Автобус" };

			var offers = new List<Offer>
	{
		new Offer { Id = 1, Title = "Самолетна почивка", TravelingWayId = 1 },
		new Offer { Id = 2, Title = "Автобусна почивка", TravelingWayId = 2 },
		new Offer { Id = 3, Title = "Собствен транспорт", TravelingWayId = 0 }
    };

			_mockOfferRepository?.Setup(repo => repo.GetAllAsync()).ReturnsAsync(offers);
			_mockTravelingWayRepository?.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<TravelingWay> { travelingWay1, travelingWay2 });

			// Act
			var result = await _homeService!.GetOffersGroupedByTravelingWayAsync();

			var travelingWay1Offers = result[travelingWay1].ToList();
			var travelingWay2Offers = result[travelingWay2].ToList();

			// Assert
			Assert.That(result, Has.Count.EqualTo(2)); 

			Assert.That(travelingWay1Offers, Has.Count.EqualTo(1)); 
			Assert.That(travelingWay2Offers, Has.Count.EqualTo(1)); 

			Assert.That(travelingWay1Offers.First().Title, Is.EqualTo("Самолетна почивка"));
			Assert.That(travelingWay2Offers.First().Title, Is.EqualTo("Автобусна почивка"));
		}

		[Test]
		public async Task GetOffersGroupedByTravelingWayAsync_HandlesEmptyOffers()
		{
			// Arrange
			var travelingWays = new List<TravelingWay>
	{
		new TravelingWay { Id = 1, Method = "Air" },
		new TravelingWay { Id = 2, Method = "Land" }
	};

			// Setup mocks
			_mockOfferRepository?.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Offer>()); 
			_mockTravelingWayRepository?.Setup(repo => repo.GetAllAsync()).ReturnsAsync(travelingWays); 

			// Act
			var result = await _homeService!.GetOffersGroupedByTravelingWayAsync();

			// Assert
			Assert.That(result, Has.Count.EqualTo(0)); 
			foreach (var travelingWay in travelingWays)
			{
				Assert.That(result.ContainsKey(travelingWay), Is.False); 
			}
		}
	}
}
	


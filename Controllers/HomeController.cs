using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TravelAgencyWebApp.Services.Data;
using TravelAgencyWebApp.Services.Data.Interfaces;
using TravelAgencyWebApp.ViewModels;
using TravelAgencyWebApp.ViewModels.Offer;

namespace TravelAgencyWebApp.Controllers
{
	public class HomeController(IHomeService homeService,
		ITravelingWayService travelingWayService, IOfferService offerService, ILogger<HomeController> logger)
		: BaseController(logger)
	{
		private readonly IHomeService _homeService = homeService
			?? throw new ArgumentNullException(nameof(homeService));

		private readonly ITravelingWayService _travelingWayService = travelingWayService
			?? throw new ArgumentNullException(nameof(travelingWayService));

		private readonly IOfferService _offerService = offerService
			?? throw new ArgumentNullException(nameof(offerService));

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var offers = await _offerService.GetAllOffersAsync();

			// Use the offer's TravelingWay method to create the group of offers
			var groupedOffers = offers
				.Where(offer => offer.TravelingWay != null) // Make sure there's a related TravelingWay
				.GroupBy(offer => offer.TravelingWay!.Method)  // Group by the method of TravelingWay
				.ToDictionary(g => g.Key, g => g.Select(o => new OfferViewModel
				{
					Id = o.Id,
					Title = o.Title,
					Description = o.Description,
					Price = o.Price,
					ImageUrl = o.ImageUrl,
					TravelingWayMethod = o.TravelingWay?.Method // Assuming you have this property in your model
				}));

			return View(groupedOffers);
		}

		[HttpGet("About")]
		public IActionResult About()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}

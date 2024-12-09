using Microsoft.AspNetCore.Mvc;
using TravelAgencyWebApp.Services.Data.Interfaces;
using TravelAgencyWebApp.ViewModels.Offer;

namespace TravelAgencyWebApp.Controllers
{
	public class HomeController : BaseController
	{
		private readonly IOfferService _offerService;

		public HomeController(IOfferService offerService, ILogger<HomeController> logger)
			: base(logger)
		{
			_offerService = offerService;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var offers = await _offerService.GetAllOffersAsync();

			var groupedOffers = offers
				.Where(offer => offer.TravelingWay != null)
				.GroupBy(offer => offer.TravelingWay!.Method)
				.ToDictionary(g => g.Key, g => g.Select(o => new OfferViewModel
				{
					Id = o.Id,
					Title = o.Title,
					Description = o.Description,
					Price = o.Price,
					ImageUrl = o.ImageUrl,
					TravelingWayMethod = o.TravelingWay?.Method
				}));

			return View(groupedOffers);
		}

		[HttpGet]
		public IActionResult About()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error(int statusCode)
		{
			if (statusCode == 404 || statusCode == 400)
			{
				return this.View("Error404");
			}

			if (statusCode == 500)
			{
				return this.View("Error500");
			}
			return View();
		}
	}
}

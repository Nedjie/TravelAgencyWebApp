using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TravelAgencyWebApp.Services.Data;
using TravelAgencyWebApp.Services.Data.Interfaces;
using TravelAgencyWebApp.ViewModels;

namespace TravelAgencyWebApp.Controllers
{
    public class HomeController(IHomeService homeService,
        ITravelingWayService travelingWayService,IOfferService offerService, ILogger<HomeController> logger)
        : BaseController(logger)
    {
        private readonly IHomeService _homeService = homeService
            ?? throw new ArgumentNullException(nameof(homeService));

        private readonly ITravelingWayService _travelingWayService=travelingWayService
			?? throw new ArgumentNullException(nameof(travelingWayService));

        private readonly IOfferService _offerService=offerService
			?? throw new ArgumentNullException(nameof(offerService));

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			//var travelingWays = await _travelingWayService.GetAllTravelingWaysAsync();
			var offers = await _offerService.GetOffersGroupedByTravelingWayAsync(); 
			return View(offers);
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

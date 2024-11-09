using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TravelAgencyWebApp.Services.Data.Interfaces;
using TravelAgencyWebApp.ViewModels;

namespace TravelAgencyWebApp.Controllers
{
	public class HomeController : BaseController
	{
		private readonly IHomeService _homeService;

		public HomeController(IHomeService homeService, ILogger<BaseController> logger)
			: base(logger)
		{
			_homeService = homeService;
		}

		[HttpGet]
        public async Task<IActionResult> Index()
        {
            var offers = await _homeService.GetOffersAsync();
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

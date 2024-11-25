using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelAgencyWebApp.Services.Data.Interfaces;
using static TravelAgencyWebApp.Common.ApplicationConstants;

namespace TravelAgencyWebApp.Areas.Admin.Controllers
{
	[Area(AdminRoleName)]
	[Authorize(Roles =AdminRoleName)]
	public class HomeController : Controller
	{
        private readonly IOfferService _offerService;

        public HomeController(IOfferService offerService)
        {
            _offerService = offerService;
        }

        public async Task<IActionResult> Index()
		{
            var offers = await _offerService.GetAllOffersAsync(); 
            return View(offers);
        }
	}
}

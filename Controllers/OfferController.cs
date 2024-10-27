using Microsoft.AspNetCore.Mvc;

namespace TravelAgencyWebApp.Controllers
{
    public class OfferController : BaseController
    {
        public OfferController(ILogger<BaseController> logger) : base(logger)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace TravelAgencyWebApp.Controllers
{
    public class ReviewController : BaseController
    {
        public ReviewController(ILogger<BaseController> logger) : base(logger)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}

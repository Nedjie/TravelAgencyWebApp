using Microsoft.AspNetCore.Mvc;

namespace TravelAgencyWebApp.Controllers
{
    public class AccountContoller : BaseController
    {
        public AccountContoller(ILogger<BaseController> logger) : base(logger)
        {
        }

        // GET: /about
        [HttpGet]
        public IActionResult Index()
        {
            return View(); // This will return About/Index.cshtml view
        }
    }
}

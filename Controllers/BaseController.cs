using Microsoft.AspNetCore.Mvc;

namespace TravelAgencyWebApp.Controllers
{
    public class BaseController : Controller
    {
        protected readonly ILogger<BaseController> _logger;

        public BaseController(ILogger<BaseController> logger)
        {
            _logger = logger;
        }

        protected void LogError(string message)
        {
            _logger.LogError(message);
        }

        protected IActionResult HandleException(Exception ex)
        {
            LogError(ex.Message);
            return View("Error");
        }

        protected bool IsUserAuthenticated()
        {
            return User.Identity.IsAuthenticated;
        }

        public IActionResult NotFoundPage()
        {
            return View("NotFound");
        }
    }
}


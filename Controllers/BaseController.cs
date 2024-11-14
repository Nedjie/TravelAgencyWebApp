using Microsoft.AspNetCore.Mvc;

namespace TravelAgencyWebApp.Controllers
{
    public class BaseController(ILogger<BaseController> logger) : Controller
    {
        protected readonly ILogger<BaseController> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        protected void LogError(string message)
        {
            _logger.LogError("An error occurred: {Message}", message);
        }

        protected IActionResult HandleException(Exception ex)
        {
            LogError(ex.Message);
            return View("Error");
        }

        protected bool IsUserAuthenticated()
        {
            return User.Identity!.IsAuthenticated;
        }

        public IActionResult NotFoundPage()
        {
            return View("NotFound");
        }
    }
}


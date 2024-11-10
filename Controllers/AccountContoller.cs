using Microsoft.AspNetCore.Mvc;

namespace TravelAgencyWebApp.Controllers
{
    public class AccountContoller : BaseController
    {
        public AccountContoller(ILogger<BaseController> logger) : base(logger)
        {
        }

		public IActionResult Register()
		{
			return View(); 
		}
	}
}

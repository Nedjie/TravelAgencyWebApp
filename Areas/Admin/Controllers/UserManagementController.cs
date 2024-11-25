using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static TravelAgencyWebApp.Common.ApplicationConstants;


namespace TravelAgencyWebApp.Areas.Admin.Controllers
{
	[Area(AdminRoleName)]
	[Authorize(Roles = AdminRoleName)]
	public class UserManagementController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}

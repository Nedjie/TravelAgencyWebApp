using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelAgencyWebApp.Services.Data.Interfaces;
using TravelAgencyWebApp.ViewModels.Admin.UserManagement;
using static TravelAgencyWebApp.Common.ApplicationConstants;


namespace TravelAgencyWebApp.Areas.Admin.Controllers
{
    [Area(AdminRoleName)]
    [Authorize(Roles = AdminRoleName)]
    public class UserManagementController : Controller
    {
        private readonly IApplicationUserService _userService;

        public UserManagementController(IApplicationUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<AllUsersViewModel> allUsers = await _userService.GetAllUsersAsync();

            return View(allUsers);
        }
    }
}


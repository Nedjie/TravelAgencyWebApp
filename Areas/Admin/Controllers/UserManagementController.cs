using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelAgencyWebApp.Data.Models;
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
			IEnumerable<AllUsersViewModel> allUsers =await _userService.GetAllUsersAsync();

            return View(allUsers);
		}

        // GET: Admin/UserManagement/Details/{id}
        public async Task<IActionResult> Details(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound(); 
            }

            var roles = await _userService.GetRolesByUserIdAsync(id); 

            var model = new UserDetailsViewModel
            {
                Id = user.Id.ToString(),
                UserName = user.UserName!,
                Email = user.Email!,
                FullName = user.FullName,
                Address = user.Address,
                Roles = roles 
            };

            return View(model);
        }

        // POST: Admin/UserManagement/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _userService.DeleteUserAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}


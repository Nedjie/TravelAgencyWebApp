using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelAgencyWebApp.Services.Data.Interfaces;
using static TravelAgencyWebApp.Common.ApplicationConstants;

namespace TravelAgencyWebApp.Areas.Admin.Controllers
{
	[Area(AdminRoleName)]
	[Authorize(Roles = AdminRoleName)]
	public class HomeController : Controller
	{
		private readonly IOfferService _offerService;
		private readonly IApplicationUserService _userService;


		public HomeController(IOfferService offerService,
			IApplicationUserService userService)
		{
			_offerService = offerService;
			_userService = userService;
		}

		public async Task<IActionResult> Index()
		{
			var offers = await _offerService.GetAllOffersAsync();
			var users = await _userService.GetAllUsersAsync();
			ViewBag.Users = users;
			return View(offers);
		}

		[HttpPost]
		public async Task<IActionResult> AssignRole(string userId, string role)
		{
			Guid userGuid = Guid.Empty;
			if (!this.IsGuidValid(userId, ref userGuid))
			{
				return this.RedirectToAction(nameof(Index));
			}

			bool userExists = await _userService
				.UserExistsByIdAsync(userGuid);
			if (!userExists)
			{
				return this.RedirectToAction(nameof(Index));
			}

			bool assignResult = await _userService
				.AssignUserToRoleAsync(userGuid, role);
			if (!assignResult)
			{
				return this.RedirectToAction(nameof(Index));
			}

			return this.RedirectToAction(nameof(Index));
		}

		[HttpPost]
		public async Task<IActionResult> RemoveRole(string userId, string role)
		{
			Guid userGuid = Guid.Empty;
			if (!this.IsGuidValid(userId, ref userGuid))
			{
				return this.RedirectToAction(nameof(Index));
			}

			bool userExists = await _userService
				.UserExistsByIdAsync(userGuid);
			if (!userExists)
			{
				return this.RedirectToAction(nameof(Index));
			}

			bool removeResult = await _userService
				.RemoveUserRoleAsync(userGuid, role);
			if (!removeResult)
			{
				return this.RedirectToAction(nameof(Index));
			}

			return this.RedirectToAction(nameof(Index));
		}

		[HttpPost]
		public async Task<IActionResult> DeleteUser(string userId)
		{
			Guid userGuid = Guid.Empty;
			if (!this.IsGuidValid(userId, ref userGuid))
			{
				return this.RedirectToAction(nameof(Index));
			}

			bool userExists = await _userService
				.UserExistsByIdAsync(userGuid);
			if (!userExists)
			{
				return this.RedirectToAction(nameof(Index));
			}

			bool removeResult = await _userService
				.DeleteUserAsync(userGuid);
			if (!removeResult)
			{
				return this.RedirectToAction(nameof(Index));
			}

			return this.RedirectToAction(nameof(Index));
		}

		private bool IsGuidValid(string? id, ref Guid parsedGuid)
		{
			if (String.IsNullOrWhiteSpace(id))
			{
				return false;
			}

			bool isGuidValid = Guid.TryParse(id, out parsedGuid);
			if (!isGuidValid)
			{
				return false;
			}

			return true;
		}
	}
}


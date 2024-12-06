using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TravelAgencyWebApp.Data.Models;
using TravelAgencyWebApp.Services.Data.Interfaces;
using TravelAgencyWebApp.ViewModels.Admin;
using static TravelAgencyWebApp.Common.ApplicationConstants;

namespace TravelAgencyWebApp.Areas.Admin.Controllers
{
	[Area(AdminRoleName)]
	[Authorize(Roles = AdminRoleName)]
	public class HomeController : Controller
	{
		private readonly IOfferService _offerService;
		private readonly IApplicationUserService _userService;
		private readonly IRoleService _roleService;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IAgentService _agentService;


		public HomeController(IOfferService offerService,
			IApplicationUserService userService, IRoleService roleService,
			UserManager<ApplicationUser> userManager, IAgentService agentService)
		{
			_offerService = offerService;
			_userService = userService;
			_roleService = roleService;
			_userManager = userManager;
			_agentService = agentService;
		}

		public async Task<IActionResult> Index()
		{
			var offers = await _offerService.GetAllOffersAsync();
			var userViewModels = await _userService.GetAllUsersAsync();

			var userList = new List<ApplicationUser>();

			foreach (var vm in userViewModels)
			{
				var user = new ApplicationUser
				{
					Id = Guid.Parse(vm.Id),
					Email = vm.Email,
					FullName = vm.FullName,
					Roles = vm.Roles.ToList()
				};

				userList.Add(user);
			}

			var model = new AdminDashboardViewModel
			{
				Offers = offers,
				Users = userList
			};

			var allRoles = await _roleService.GetAllRoleNamesAsync();
			ViewBag.Roles = allRoles;

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> AssignRole(string userId, string role)
		{
			if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(role))
			{
				return BadRequest("User ID and role are required.");
			}

			var user = await _userManager.FindByIdAsync(userId);
			if (user == null)
			{
				return NotFound();
			}

			var result = await _userManager.AddToRoleAsync(user, role);
			if (result.Succeeded)
			{
				if (role.Equals("Agent", StringComparison.OrdinalIgnoreCase))
				{
					var existingAgent = await _agentService.GetByUserIdAsync(userId);
					if (existingAgent == null)
					{
						var agent = new Agent
						{
							Email = user.Email ?? "unknown",
							FullName = user.FullName,
                            UserId = Guid.Parse(userId)
                        };
						await _agentService.AddAsync(agent);
					}
				}
				return RedirectToAction("Index");
			}
			else
			{
				ModelState.AddModelError(string.Empty, "Failed to assign role.");
				return RedirectToAction("Index");
			}
		}

		[HttpPost]
		public async Task<IActionResult> RemoveRole(string userId, string role)
		{
			if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(role))
			{
				return BadRequest("User ID and role are required.");
			}

			var user = await _userManager.FindByIdAsync(userId);
			if (user == null)
			{
				return NotFound();
			}

			var result = await _userManager.RemoveFromRoleAsync(user, role);
			if (result.Succeeded)
			{
				if (role.Equals("Agent", StringComparison.OrdinalIgnoreCase))
				{
					var existingAgent = await _agentService.GetByUserIdAsync(user.Id.ToString());
					if (existingAgent != null)
					{
						var deletionSuccessful = await _agentService.DeleteAsyncHard(existingAgent);

						if (!deletionSuccessful)
						{
							TempData["ErrorMessage"] = "Failed to delete associated agent.";
						}
					}
				}
				TempData["SuccessMessage"] = $"Role '{role}' removed successfully from user.";
				return RedirectToAction("Index"); 
			}

			foreach (var error in result.Errors)
			{
				ModelState.AddModelError(string.Empty, error.Description);
			}

			TempData["ErrorMessage"] = "Failed to remove the role.";
			return RedirectToAction("Index");
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

		private async Task<bool> CreateAgentAsync(string userId)
		{
			var existingAgent = await _agentService.GetByUserIdAsync(userId);
			if (existingAgent != null)
			{
				return true;
			}

			var newAgent = new Agent
			{
				Id = Guid.Parse(userId),
			};

			await _agentService.AddAsync(newAgent);
			return true;
		}
	}
}


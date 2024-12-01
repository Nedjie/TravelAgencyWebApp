﻿using Microsoft.AspNetCore.Authorization;
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


        public HomeController(IOfferService offerService,
            IApplicationUserService userService, IRoleService roleService,
            UserManager<ApplicationUser> userManager)
        {
            _offerService = offerService;
            _userService = userService;
            _roleService = roleService;
            _userManager = userManager;
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
            Console.WriteLine($"AssignRole called: UserId = {userId}, Role = {role}");

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
                TempData["SuccessMessage"] = "Role assigned successfully.";
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


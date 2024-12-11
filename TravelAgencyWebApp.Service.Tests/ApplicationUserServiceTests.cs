using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;
using TravelAgencyWebApp.Data.Models;
using TravelAgencyWebApp.Services.Data;

namespace TravelAgencyWebApp.Service.Tests
{
	[TestFixture]
	public class ApplicationUserServiceTests
	{
		private Mock<UserManager<ApplicationUser>>? _mockUserManager;
		private Mock<RoleManager<IdentityRole<Guid>>>? _mockRoleManager;
		private ApplicationUserService? _applicationUserService;

		[SetUp]
		public void Setup()
		{
			_mockUserManager = new Mock<UserManager<ApplicationUser>>(
				Mock.Of<IUserStore<ApplicationUser>>(), null!, null!, null!, null!, null!, null!, null!, null!);

			_mockRoleManager = new Mock<RoleManager<IdentityRole<Guid>>>(
				Mock.Of<IRoleStore<IdentityRole<Guid>>>(), null!, null!, null!, null!);

			_applicationUserService = new ApplicationUserService(_mockUserManager.Object, _mockRoleManager.Object);
		}

		[Test]
		public async Task AssignUserToRoleAsync_UserNotFound_ReturnsFalse()
		{
			// Arrange
			var userId = Guid.NewGuid();
			string roleName = "Admin";
			_mockUserManager!.Setup(u => u.FindByIdAsync(userId.ToString())).ReturnsAsync((ApplicationUser)null!);

			// Act
			var result = await _applicationUserService!.AssignUserToRoleAsync(userId, roleName);

			// Assert
			Assert.That(result, Is.False);
		}

		[Test]
		public async Task AssignUserToRoleAsync_RoleNotFound_ReturnsFalse()
		{
			// Arrange
			var userId = Guid.NewGuid();
			string roleName = "Admin";
			var user = new ApplicationUser { Id = userId };

			_mockUserManager?.Setup(u => u.FindByIdAsync(userId.ToString())).ReturnsAsync(user);
			_mockRoleManager?.Setup(r => r.RoleExistsAsync(roleName)).ReturnsAsync(false);

			// Act
			var result = await _applicationUserService!.AssignUserToRoleAsync(userId, roleName);

			// Assert
			Assert.That(result, Is.False);
		}

		[Test]
		public async Task AssignUserToRoleAsync_UserAlreadyInRole_ReturnsTrue()
		{
			// Arrange
			var userId = Guid.NewGuid();
			string roleName = "Admin";
			var user = new ApplicationUser { Id = userId };

			_mockUserManager?.Setup(u => u.FindByIdAsync(userId.ToString())).ReturnsAsync(user);
			_mockRoleManager?.Setup(r => r.RoleExistsAsync(roleName)).ReturnsAsync(true);
			_mockUserManager?.Setup(u => u.IsInRoleAsync(user, roleName)).ReturnsAsync(true);

			// Act
			var result = await _applicationUserService!.AssignUserToRoleAsync(userId, roleName);

			// Assert
			Assert.That(result, Is.True);
		}

		[Test]
		public async Task UserExistsByIdAsync_UserExists_ReturnsTrue()
		{
			// Arrange
			var userId = Guid.NewGuid();
			var user = new ApplicationUser { Id = userId };

			_mockUserManager?.Setup(u => u.FindByIdAsync(userId.ToString())).ReturnsAsync(user);

			// Act
			var result = await _applicationUserService!.UserExistsByIdAsync(userId);

			// Assert
			Assert.That(result, Is.True);
		}

		[Test]
		public async Task UserExistsByIdAsync_UserDoesNotExist_ReturnsFalse()
		{
			// Arrange
			var userId = Guid.NewGuid();
			_mockUserManager?.Setup(u => u.FindByIdAsync(userId.ToString())).ReturnsAsync((ApplicationUser)null!);

			// Act
			var result = await _applicationUserService!.UserExistsByIdAsync(userId);

			// Assert
			Assert.That(result, Is.False);
		}

		[Test]
		public async Task RemoveUserRoleAsync_UserNotFound_ReturnsFalse()
		{
			// Arrange
			var userId = Guid.NewGuid();
			string roleName = "Admin";
			_mockUserManager?.Setup(u => u.FindByIdAsync(userId.ToString())).ReturnsAsync((ApplicationUser)null!);

			// Act
			var result = await _applicationUserService!.RemoveUserRoleAsync(userId, roleName);

			// Assert
			Assert.That(result, Is.False);
		}

		[Test]
		public async Task RemoveUserRoleAsync_RoleNotFound_ReturnsFalse()
		{
			// Arrange
			var userId = Guid.NewGuid();
			string roleName = "Admin";
			var user = new ApplicationUser { Id = userId };

			_mockUserManager?.Setup(u => u.FindByIdAsync(userId.ToString())).ReturnsAsync(user);
			_mockRoleManager?.Setup(r => r.RoleExistsAsync(roleName)).ReturnsAsync(false);

			// Act
			var result = await _applicationUserService!.RemoveUserRoleAsync(userId, roleName);

			// Assert
			Assert.That(result, Is.False);
		}

		[Test]
		public async Task RemoveUserRoleAsync_UserInRole_RemovesRoleAndReturnsTrue()
		{
			// Arrange
			var userId = Guid.NewGuid();
			string roleName = "Admin";
			var user = new ApplicationUser { Id = userId };

			_mockUserManager?.Setup(u => u.FindByIdAsync(userId.ToString())).ReturnsAsync(user);
			_mockRoleManager?.Setup(r => r.RoleExistsAsync(roleName)).ReturnsAsync(true);
			_mockUserManager?.Setup(u => u.IsInRoleAsync(user, roleName)).ReturnsAsync(true);
			_mockUserManager?.Setup(u => u.RemoveFromRoleAsync(user, roleName)).ReturnsAsync(IdentityResult.Success);

			// Act
			var result = await _applicationUserService!.RemoveUserRoleAsync(userId, roleName);

			// Assert
			Assert.That(result, Is.True);
			_mockUserManager?.Verify(u => u.RemoveFromRoleAsync(user, roleName), Times.Once);
		}

		[Test]
		public async Task DeleteUserAsync_UserNotFound_ReturnsFalse()
		{
			// Arrange
			var userId = Guid.NewGuid();
			_mockUserManager?.Setup(u => u.FindByIdAsync(userId.ToString())).ReturnsAsync((ApplicationUser)null!);

			// Act
			var result = await _applicationUserService!.DeleteUserAsync(userId);

			// Assert
			Assert.That(result, Is.False);
		}

		[Test]
		public async Task DeleteUserAsync_UserFound_DeletesUserAndReturnsTrue()
		{
			// Arrange
			var userId = Guid.NewGuid();
			var user = new ApplicationUser { Id = userId };
			_mockUserManager?.Setup(u => u.FindByIdAsync(userId.ToString())).ReturnsAsync(user);
			_mockUserManager?.Setup(u => u.DeleteAsync(user)).ReturnsAsync(IdentityResult.Success);

			// Act
			var result = await _applicationUserService!.DeleteUserAsync(userId);

			// Assert
			Assert.That(result, Is.True);
			_mockUserManager?.Verify(u => u.DeleteAsync(user), Times.Once);
		}
	}
}

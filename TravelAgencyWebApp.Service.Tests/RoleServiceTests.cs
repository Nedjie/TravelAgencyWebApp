using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;
using TravelAgencyWebApp.Services.Data;

namespace TravelAgencyWebApp.Service.Tests
{
	[TestFixture]
	public class RoleServiceTests
	{
		private Mock<RoleManager<IdentityRole<Guid>>>? _mockRoleManager;
		private RoleService? _roleService;

		[SetUp]
		public void Setup()
		{
			var store = new Mock<IRoleStore<IdentityRole<Guid>>>();
			_mockRoleManager = new Mock<RoleManager<IdentityRole<Guid>>>(store.Object, null!, null!, null!, null!);

			_roleService = new RoleService(_mockRoleManager.Object);
		}

		[Test]
		public async Task RoleExistsAsync_ReturnsTrue_WhenRoleExists()
		{
			// Arrange
			var roleName = "Admin";
			_mockRoleManager?.Setup(rm => rm.RoleExistsAsync(roleName)).ReturnsAsync(true);

			// Act
			var result = await _roleService!.RoleExistsAsync(roleName);

			// Assert
			Assert.That(result, Is.True);
		}

		[Test]
		public async Task RoleExistsAsync_ReturnsFalse_WhenRoleDoesNotExist()
		{
			// Arrange
			var roleName = "User";
			_mockRoleManager?.Setup(rm => rm.RoleExistsAsync(roleName)).ReturnsAsync(false);

			// Act
			var result = await _roleService!.RoleExistsAsync(roleName);

			// Assert
			Assert.That(result, Is.False);
		}

	}
}
	


using Microsoft.AspNetCore.Identity;
using TravelAgencyWebApp.Data.Models;

namespace TravelAgencyWebApp.Data.Seeding
{
	public static class SeedDataUsers
	{
		public static async Task DataUsers(UserManager<ApplicationUser> userManager)
		{
			if (userManager.Users.Any())
			{
				return;
			}

			var users = new ApplicationUser[]
			{
		new ApplicationUser
		{
			FullName = "Иван Петров",
			Address = "гр. Пловдив",
			UserName = "ivan@gmail.com",
			Email = "ivan@gmail.com"
		},
		new ApplicationUser
		{
			FullName = "Неджмие Чакър",
			Address = "гр. Кубрат",
			UserName = "nedji@gmail.com",
			Email = "nedji@gmail.com"
		}
			};

			foreach (var user in users)
			{
				await userManager.CreateAsync(user, "123456a");
			}
		}
	}
}

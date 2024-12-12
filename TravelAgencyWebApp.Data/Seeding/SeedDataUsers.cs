using Microsoft.AspNetCore.Identity;
using TravelAgencyWebApp.Data.Models;

namespace TravelAgencyWebApp.Data.Seeding
{
	public static class SeedDataUsers
	{
		public static async Task DataUsers(UserManager<ApplicationUser> userManager)
		{
			if (userManager.Users.Any(u => u.Email == "ivan@gmail.com" || u.Email == "nedji@gmail.com"))
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
				var password = "123456a";
				var result = await userManager.CreateAsync(user, password);
				if (result.Succeeded)
				{
					Console.WriteLine($"User '{user.UserName}' created successfully.");
				}
				else
				{
					foreach (var error in result.Errors)
					{
						Console.WriteLine($"Error creating user '{user.UserName}': {error.Description}");
					}
				}

			}
		}
	}
}

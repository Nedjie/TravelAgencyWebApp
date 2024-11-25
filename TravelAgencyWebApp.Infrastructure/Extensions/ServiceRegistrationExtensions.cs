using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TravelAgencyWebApp.Data;
using TravelAgencyWebApp.Data.Models;
using TravelAgencyWebApp.Data.Repository;
using TravelAgencyWebApp.Data.Repository.Interfaces;
using TravelAgencyWebApp.Services.Data;
using TravelAgencyWebApp.Services.Data.Interfaces;
using static TravelAgencyWebApp.Common.ApplicationConstants;

namespace TravelAgencyWebApp.Infrastructure.Extensions
{
	public static class ServiceRegistrationExtensions
	{
		public static IServiceCollection AddApplicationDatabase(this IServiceCollection services, IConfiguration config)
		{
			// Add services to the container.
			var connectionString = config.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string is wrong");
			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(connectionString));
			services.AddDatabaseDeveloperPageExceptionFilter();

			services.ConfigureApplicationCookie(options =>
			{
				options.LoginPath = "/Identity/Account/Login";
			});

			return services;
		}

		public static IServiceCollection AddApplicationIdentity(this IServiceCollection services, IConfiguration config)
		{
			services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
				.AddEntityFrameworkStores<ApplicationDbContext>()
				.AddRoles<IdentityRole<Guid>>()
				.AddSignInManager<SignInManager<ApplicationUser>>()
				.AddUserManager<UserManager<ApplicationUser>>();
		
			return services;

		}

		public static IApplicationBuilder SeedAdministrator(this IApplicationBuilder app, string email, string username, string password)
		{
			using IServiceScope serviceScope = app.ApplicationServices.CreateAsyncScope();
			IServiceProvider serviceProvider = serviceScope.ServiceProvider;

			RoleManager<IdentityRole<Guid>>? roleManager = serviceProvider
				.GetService<RoleManager<IdentityRole<Guid>>>();
			IUserStore<ApplicationUser>? userStore = serviceProvider
				.GetService<IUserStore<ApplicationUser>>();
			UserManager<ApplicationUser>? userManager = serviceProvider
				.GetService<UserManager<ApplicationUser>>();

			if (roleManager == null)
			{
				throw new ArgumentNullException(nameof(roleManager),
					$"Service for {typeof(RoleManager<IdentityRole<Guid>>)} cannot be obtained!");
			}

			if (userStore == null)
			{
				throw new ArgumentNullException(nameof(userStore),
					$"Service for {typeof(IUserStore<ApplicationUser>)} cannot be obtained!");
			}

			if (userManager == null)
			{
				throw new ArgumentNullException(nameof(userManager),
					$"Service for {typeof(UserManager<ApplicationUser>)} cannot be obtained!");
			}

			Task.Run(async () =>
			{
				bool roleExists = await roleManager.RoleExistsAsync(AdminRoleName);
				IdentityRole<Guid>? adminRole = null;
				if (!roleExists)
				{
					adminRole = new IdentityRole<Guid>(AdminRoleName);

					IdentityResult result = await roleManager.CreateAsync(adminRole);
					if (!result.Succeeded)
					{
						throw new InvalidOperationException($"Error occurred while creating the {AdminRoleName} role!");
					}
				}
				else
				{
					adminRole = await roleManager.FindByNameAsync(AdminRoleName);
				}

				ApplicationUser? adminUser = await userManager.FindByEmailAsync(email);
				if (adminUser == null)
				{
					adminUser = await
						CreateAdminUserAsync(email, username, password, userStore, userManager);
				}

				if (await userManager.IsInRoleAsync(adminUser, AdminRoleName))
				{
					return app;
				}

				IdentityResult userResult = await userManager.AddToRoleAsync(adminUser, AdminRoleName);
				if (!userResult.Succeeded)
				{
					throw new InvalidOperationException($"Error occurred while adding the user {username} to the {AdminRoleName} role!");
				}

				return app;
			})
				.GetAwaiter()
				.GetResult();

			return app;
		}

		public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration config)
		{
			// Register repositories and services
			services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>)); // Example repository
			services.AddScoped<IBookingService, BookingService>(); 
			services.AddScoped<IHomeService, HomeService>();
			services.AddScoped<IOfferService, OfferService>();
			services.AddScoped<IReviewService, ReviewService>();
			services.AddScoped<ITravelingWayService, TravelingWayService>();

			return services;
		}

		private static async Task<ApplicationUser> CreateAdminUserAsync(string email, string username, string password,
		  IUserStore<ApplicationUser> userStore, UserManager<ApplicationUser> userManager)
		{
			ApplicationUser applicationUser = new ApplicationUser
			{
				Email = email
			};

			await userStore.SetUserNameAsync(applicationUser, username, CancellationToken.None);
			IdentityResult result = await userManager.CreateAsync(applicationUser, password);
			if (!result.Succeeded)
			{
				throw new InvalidOperationException($"Error occurred while registering {AdminRoleName} user!");
			}

			return applicationUser;
		}
	}
}



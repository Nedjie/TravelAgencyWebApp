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
			services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
				.AddEntityFrameworkStores<ApplicationDbContext>()
				.AddDefaultTokenProviders();
		
			return services;

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
	}
}



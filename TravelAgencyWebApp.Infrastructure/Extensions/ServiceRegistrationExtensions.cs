using Microsoft.AspNetCore.Identity;
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
		public static void AddCustomServices(this IServiceCollection services)
		{
			// Register repositories and services
			services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>)); // Example repository
			services.AddScoped<IBookingService, BookingService>(); 
			services.AddScoped<IHomeService, HomeService>();
			services.AddScoped<IOfferService, OfferService>();
			services.AddScoped<IReviewService, ReviewService>();
			services.AddScoped<ITravelingWayService, TravelingWayService>();

			services.AddIdentity<ApplicationUser, IdentityRole>()
				.AddEntityFrameworkStores<ApplicationDbContext>()
				.AddDefaultTokenProviders();

			//// Identity setup
			//services.AddIdentity<ApplicationUser, IdentityRole>()
			//	.AddEntityFrameworkStores<ApplicationDbContext>()
			//	.AddDefaultTokenProviders();

		}
	}
}



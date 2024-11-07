using TravelAgencyWebApp.Data.Repository.Interfaces;
using TravelAgencyWebApp.Services.Data.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;
using TravelAgencyWebApp.Data.Models;
using TravelAgencyWebApp.Services.Data;
using NuGet.Protocol.Core.Types;
using TravelAgencyWebApp.Data.Repository;

namespace TravelAgencyWebApp.Infrastructure.Extensions
{
    public static class ServiceRegistrationExtensions
    {
        public static void RegisterUserDefinedServices(this IServiceCollection services)
        {
            RegisterServicesAndRepositories(services, typeof(IBookingService).Assembly);

            // Register Services
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<IOfferService, OfferService>();
            services.AddScoped<IReviewService, ReviewService>();

            // Register Repositories
            services.AddScoped<IRepository<Booking>, Repository<Booking>>();
            services.AddScoped<IRepository<Offer>, Repository<Offer>>();
            services.AddScoped<IRepository<Review>, Repository<Review>>();

        }

        private static void RegisterServicesAndRepositories(IServiceCollection services, Assembly assembly)
        {
            var serviceTypes = assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract)
                .ToList();

            foreach (var serviceType in serviceTypes)
            {
                var serviceInterface = serviceType.GetInterfaces()
                    .FirstOrDefault(i => i.Name == "I" + serviceType.Name);

                if (serviceInterface != null)
                {
                    services.AddScoped(serviceInterface, serviceType);
                }

                var repositoryInterface = serviceType.GetInterfaces()
                    .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRepository<>));

                if (repositoryInterface != null)
                {
                    services.AddScoped(repositoryInterface, serviceType);
                }
            }
        }
    }
}



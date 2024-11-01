using TravelAgencyWebApp.Data.Repository.Interfaces;
using TravelAgencyWebApp.Services.Data.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace TravelAgencyWebApp.Infrastructure.Extensions
{
    public static class ServiceRegistrationExtensions
    {
        public static void RegisterUserDefinedServices(this IServiceCollection services)
        {
            RegisterServicesAndRepositories(services, typeof(IBookingService).Assembly);
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



using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
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
            var connectionString = config.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string is wrong");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
			

			services.AddDatabaseDeveloperPageExceptionFilter();

            services.ConfigureApplicationCookie(options =>
            {
				options.LoginPath = "/Account/Login";
				options.LogoutPath = "/Account/Logout";
				options.AccessDeniedPath = "/Account/AccessDenied";
			});

            return services;
        }

        public static IServiceCollection AddApplicationIdentity(this IServiceCollection services, IConfiguration _)
        {
            services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;

            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddRoles<IdentityRole<Guid>>()
                .AddSignInManager<SignInManager<ApplicationUser>>()
                .AddUserManager<UserManager<ApplicationUser>>()
                .AddDefaultTokenProviders();

            return services;
        }

        public static IApplicationBuilder SeedAdministrator(this IApplicationBuilder app, string email, string username, string password)
        {
            using IServiceScope serviceScope = app.ApplicationServices.CreateAsyncScope();
            IServiceProvider serviceProvider = serviceScope.ServiceProvider;

            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole<Guid>>>();
            var userStore = serviceProvider.GetService<IUserStore<ApplicationUser>>();
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();

            if (roleManager is null)
            {
                throw new InvalidOperationException($"Error occurred while creating the {AdminRoleName} role!");
            }

            if (userStore is null)
            {
                throw new InvalidOperationException($"Error occurred while creating the {AdminRoleName} role!");
            }

            if (userManager is null)
            {
                throw new InvalidOperationException($"Error occurred while creating the {AdminRoleName} role!");
            }

            Task.Run(async () =>
            {
                bool roleExists = await roleManager!.RoleExistsAsync(AdminRoleName);
                var adminRole = new IdentityRole<Guid>(AdminRoleName);
                if (!roleExists)
                {
                    adminRole = new IdentityRole<Guid>(AdminRoleName);

                    var result = await roleManager.CreateAsync(adminRole);
                    if (!result.Succeeded)
                    {
                        throw new InvalidOperationException($"Error occurred while creating the {AdminRoleName} role!");
                    }
                }
                else
                {
                    adminRole = await roleManager.FindByNameAsync(AdminRoleName);
                }

                var adminUser = await userManager!.FindByEmailAsync(email);
                if (adminUser == null)
                {
                    adminUser = await
                        CreateAdminUserAsync(email, username, password, userStore!, userManager, "Admin User", "Admin Address");
                }

                if (await userManager.IsInRoleAsync(adminUser, AdminRoleName))
                {
                    return app;
                }

                var userResult = await userManager.AddToRoleAsync(adminUser, AdminRoleName);
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

        public static IApplicationBuilder SeedRoleAgent(this IApplicationBuilder app)
        {
            using IServiceScope serviceScope = app.ApplicationServices.CreateAsyncScope();
            IServiceProvider serviceProvider = serviceScope.ServiceProvider;

            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole<Guid>>>();
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();

            if (roleManager is null)
            {
                throw new InvalidOperationException($"Error occurred while accessing the {AgentRoleName} role!");
            }

            if (userManager is null)
            {
                throw new InvalidOperationException("UserManager service not found!");
            }

            Task.Run(async () =>
            {
                if (!await roleManager.RoleExistsAsync(AgentRoleName))
                {
                    var result = await roleManager.CreateAsync(new IdentityRole<Guid>(AgentRoleName));
                    if (!result.Succeeded)
                    {
                        throw new InvalidOperationException($"Error occurred while creating the Agent role!");
                    }
                }
            }).GetAwaiter().GetResult();

            return app; 

        }

        public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration _)
        {
            // Register repositories and services
            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
            services.AddScoped<IApplicationUserService, ApplicationUserService>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<IRepository<Booking, int>, Repository<Booking, int>>();
            services.AddScoped<IHomeService, HomeService>();
            services.AddScoped<IOfferService, OfferService>();
            services.AddScoped<ITravelingWayService, TravelingWayService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IAgentService, AgentService>();

            return services;
        }

        private static async Task<ApplicationUser> CreateAdminUserAsync(string email, string username, string password,
          IUserStore<ApplicationUser> userStore, UserManager<ApplicationUser> userManager, string fullName, string address)
        {
            var applicationUser = new ApplicationUser
            {
                Email = email,
                UserName = username,
                FullName = fullName,
                Address = address
            };

            IdentityResult result = await userManager.CreateAsync(applicationUser, password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"Error occurred while registering {username} user: {errors}");
            }

            return applicationUser;
        }
    }
}



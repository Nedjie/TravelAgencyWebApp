using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TravelAgencyWebApp.Data;
using TravelAgencyWebApp.Data.Models;
using TravelAgencyWebApp.Data.Seeding;
using static TravelAgencyWebApp.Infrastructure.Extensions.ServiceRegistrationExtensions;

namespace TravelAgencyWebApp
{
	public class Program
    {
        public static  void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            string adminEmail = builder.Configuration.GetValue<string>("Administrator:Email")!;
            string adminUsername = builder.Configuration.GetValue<string>("Administrator:Username")!;
            string adminPassword = builder.Configuration.GetValue<string>("Administrator:Password")!;

			builder.Services.AddApplicationDatabase(builder.Configuration);
            builder.Services.AddApplicationIdentity(builder.Configuration);
            builder.Services.AddCustomServices(builder.Configuration);
           
		
            builder.Services.AddControllersWithViews(cfg=>
            {
                cfg.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });
	
            builder.Services.AddRazorPages();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                app.SeedRoleAgent();
				var userManager = services.GetRequiredService<UserManager<ApplicationUser>>(); 
                var context = services.GetRequiredService<ApplicationDbContext>();
				if (!context.Users.Any(u => u.Email == "ivan@gmail.com" || u.Email == "nedji@gmail.com"))
				{
					SeedDataUsers.DataUsers(userManager).Wait();
				}		

			}

            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStatusCodePagesWithReExecute("/Home/Error/{0}");

			app.SeedAdministrator(adminEmail, adminUsername, adminPassword);
          
			app.MapControllerRoute(
                name: "Areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "Errors",
                pattern: "{controller=Home}/{action=Index}/{statusCode?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
           
            app.MapRazorPages();

             app.Run();
        }
    }
}

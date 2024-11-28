using TravelAgencyWebApp.Data.Seeding;
using TravelAgencyWebApp.Infrastructure.Extensions;
using TravelAgencyWebApp.Services.Mapping;
using TravelAgencyWebApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TravelAgencyWebApp.Data;

namespace TravelAgencyWebApp
{
	public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection")
            //    ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");

            //builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

            //builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();

            string adminEmail = builder.Configuration.GetValue<string>("Administrator:Email")!;
            string adminUsername = builder.Configuration.GetValue<string>("Administrator:Username")!;
            string adminPassword = builder.Configuration.GetValue<string>("Administrator:Password")!;
            string adminAddress = builder.Configuration.GetValue<string>("Administrator:Address")!;

			builder.Services.AddApplicationDatabase(builder.Configuration);
            builder.Services.AddApplicationIdentity(builder.Configuration);
            builder.Services.AddCustomServices(builder.Configuration);
           
		
            builder.Services.AddControllersWithViews();
	
            builder.Services.AddRazorPages();

            var app = builder.Build();

			AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).Assembly);

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

using TravelAgencyWebApp.Data.Seeding;
using TravelAgencyWebApp.Infrastructure.Extensions;
using TravelAgencyWebApp.Services.Mapping;
using TravelAgencyWebApp.ViewModels;

namespace TravelAgencyWebApp
{
	public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

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


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

			app.SeedAdministrator(adminEmail, adminUsername, adminPassword);

			app.MapControllerRoute(
                name: "Areas",
                pattern: "{area:exists}/{cotroller=Home}/{action=Index}/{id}");
			
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

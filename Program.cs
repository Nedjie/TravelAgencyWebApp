using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TravelAgencyWebApp.Data;
using TravelAgencyWebApp.Data.Models;
using TravelAgencyWebApp.Data.Repository;
using TravelAgencyWebApp.Data.Repository.Interfaces;
using TravelAgencyWebApp.Services.Mapping;
using TravelAgencyWebApp.ViewModels;

namespace TravelAgencyWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddScoped<IRepository<Booking>,Repository<Booking>>();
            builder.Services.AddScoped<IRepository<Offer>,Repository<Offer>>();
            builder.Services.AddScoped<IRepository<Review>,Repository<Review>>();
            builder.Services.AddScoped<IRepository<TravelingWay>,Repository<TravelingWay>>();
            builder.Services.AddScoped<IRepository<User>, Repository<User>>();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddControllersWithViews();

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}

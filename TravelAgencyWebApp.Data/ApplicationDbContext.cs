using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TravelAgencyWebApp.Data.Models;
using TravelAgencyWebApp.Data.Seeding;

namespace TravelAgencyWebApp.Data
{

    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : IdentityDbContext<ApplicationUser,IdentityRole<Guid>,Guid>(options)
    {
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<TravelingWay> TravelingWays { get; set; }
        //public DbSet<ApplicationUser> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString ="Server=.\\SQLEXPRESS;Database=TravelAgencyDb;Integrated Security=True;TrustServerCertificate=True";
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Booking>()
                 .HasOne(b => b.User)
                 .WithMany(u => u.Bookings)
                 .HasForeignKey(b => b.UserId)
                 .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Offer)
                .WithMany(o => o.Reviews)
                .HasForeignKey(r => r.OfferId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Offer>()
                  .HasOne(o => o.TravelingWay)
                  .WithMany(tw => tw.Offers)
                  .HasForeignKey(o => o.TravelingWayId)
                  .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Offer>()
                  .Property(o => o.Price)
                  .HasColumnType("decimal(18,2)"); 

            modelBuilder.Entity<TravelingWay>()
                   .Property(tw => tw.Cost) 
                   .HasColumnType("decimal(18,2)");

			SeedDataTravelingWays.DataTravelingWays(modelBuilder);
		}
    }

}

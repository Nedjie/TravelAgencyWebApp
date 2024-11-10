using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TravelAgencyWebApp.Data.Models;

namespace TravelAgencyWebApp.Data
{

	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
		   : base(options)
		{
		}

		public DbSet<Booking> Bookings { get; set; }
		public DbSet<Offer> Offers { get; set; }
		public DbSet<Review> Reviews { get; set; }
		public DbSet<TravelingWay> TravelingWays { get; set; }
		//public DbSet<ApplicationUser> Users { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// Configure foreign key for Booking
			modelBuilder.Entity<Booking>()
				.HasOne(b => b.User)
				.WithMany(u => u.Bookings)
				.HasForeignKey(b => b.UserId)
				.OnDelete(DeleteBehavior.Cascade);

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

			modelBuilder.Entity<TravelingWay>()
				.HasOne(t => t.Offer)
				.WithMany()
				.HasForeignKey(t => t.OfferId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}

}

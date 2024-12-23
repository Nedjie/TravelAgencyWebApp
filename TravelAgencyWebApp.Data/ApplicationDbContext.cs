﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TravelAgencyWebApp.Data.Models;
using TravelAgencyWebApp.Data.Seeding;

namespace TravelAgencyWebApp.Data
{

	public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
		: IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>(options)
	{
		public DbSet<Booking> Bookings { get; set; }
		public DbSet<Offer> Offers { get; set; }
		public DbSet<TravelingWay> TravelingWays { get; set; }
		public DbSet<Agent> Agents { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				var connectionString = "Server=.\\SQLEXPRESS;Database=TravelAgencyDb;Integrated Security=True;TrustServerCertificate=True";
				optionsBuilder.UseSqlServer(connectionString,
					b => b.MigrationsAssembly("TravelAgencyWebApp.Data"));
			}
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Booking>()
				 .HasOne(b => b.User)
				 .WithMany(u => u.Bookings)
				 .HasForeignKey(b => b.UserId)
				 .OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<Booking>()
				.HasOne(b => b.Agent)
				.WithMany(a => a.Bookings)
				.HasForeignKey(b => b.AgentId)
				 .OnDelete(DeleteBehavior.SetNull);

			modelBuilder.Entity<Offer>()
				  .HasOne(o => o.TravelingWay)
				  .WithMany(tw => tw.Offers)
				  .HasForeignKey(o => o.TravelingWayId)
				  .OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<Offer>()
				  .Property(o => o.Price)
				  .HasColumnType("decimal(18,2)");

			modelBuilder.Entity<TravelingWay>()
				   .Property(tw => tw.Cost)
				   .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Agent>()
				  .HasOne(a => a.User) 
			      .WithMany()  
				  .HasForeignKey(a => a.UserId) 
				  .OnDelete(DeleteBehavior.NoAction);


            SeedDataTravelingWays.DataTravelingWays(modelBuilder);
			SeedDataOffers.DataOffers(modelBuilder);
		}
	}

}

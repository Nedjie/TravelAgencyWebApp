using Microsoft.EntityFrameworkCore;
using TravelAgencyWebApp.Data.Models;

namespace TravelAgencyWebApp.Data.Seeding
{
	public static class SeedDataTravelingWays
	{
		public static void DataTravelingWays(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<TravelingWay>().HasData(
				new TravelingWay
				{
					Id = 1,
					Method = "Air",
					Description = "Travel by airplane.",
				},
				new TravelingWay
				{
					Id = 2,
					Method = "Train",
					Description = "Travel by train.",
				},
				new TravelingWay
				{
					Id = 3,
					Method = "Bus",
					Description = "Travel by bus.",
				},
				new TravelingWay
				{
					Id = 4,
					Method = "Car",
					Description = "Travel by car.",
				}
			);
		}
	}
}

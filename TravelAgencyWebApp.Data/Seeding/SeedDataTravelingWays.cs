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
					Method = "Самолет",
					Description = "Пътуване със самолет",
				},
				new TravelingWay
				{
					Id = 2,
					Method = "Круиз",
					Description = "Пътуване със круизен кораб",
				},
				new TravelingWay
				{
					Id = 3,
					Method ="Автобус",
					Description = "Пътуване с автобус",
				},
				new TravelingWay
				{
					Id = 4,
					Method = "Кола",
					Description = "Пътуване с кола",
				}
			);
		}
	}
}

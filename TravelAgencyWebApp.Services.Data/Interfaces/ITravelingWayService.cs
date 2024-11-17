using TravelAgencyWebApp.Data.Models;

namespace TravelAgencyWebApp.Services.Data.Interfaces
{
    public interface ITravelingWayService
    {
        Task<IEnumerable<TravelingWay>> GetAllTravelingWaysAsync();
        Task<TravelingWay?> GetTravelingWayByIdAsync(int id);
		Task<TravelingWay?> GetByMethodAsync(string method);
		Task AddTravelingWayAsync(TravelingWay travelingWay);
        Task UpdateTravelingWayAsync(TravelingWay travelingWay);
        Task DeleteTravelingWayAsync(int id);
    }
}

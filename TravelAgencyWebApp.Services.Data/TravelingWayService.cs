using TravelAgencyWebApp.Common.ErrorMessages;
using TravelAgencyWebApp.Data.Models;
using TravelAgencyWebApp.Data.Repository.Interfaces;
using TravelAgencyWebApp.Services.Data.Interfaces;

namespace TravelAgencyWebApp.Services.Data
{
	public class TravelingWayService(IRepository<TravelingWay, int> travelingWayRepository) : ITravelingWayService
    {
        private readonly IRepository<TravelingWay, int> _travelingWayRepository= travelingWayRepository 
            ?? throw new ArgumentNullException(nameof(travelingWayRepository));

        public async Task<IEnumerable<TravelingWay>> GetAllTravelingWaysAsync()
        {
            return await _travelingWayRepository.GetAllAsync();
        }

        public async Task<TravelingWay?> GetTravelingWayByIdAsync(int id)
        {
           var travelingWay= await _travelingWayRepository.GetByIdAsync(id)
                       ?? throw new EntityNotFoundException($"TravelingWay with ID {id} not found.");

            return travelingWay;
        }

		public async Task<TravelingWay?> GetByMethodAsync(string method)
		{
			var travelingWays = await _travelingWayRepository.GetAllAsync(); 
			return travelingWays.FirstOrDefault(tw => tw.Method.Equals(method, StringComparison.OrdinalIgnoreCase));
		}

		public async Task AddTravelingWayAsync(TravelingWay travelingWay)
        {
            ArgumentNullException.ThrowIfNull(travelingWay, nameof(travelingWay));

            await _travelingWayRepository.AddAsync(travelingWay);
        }

        public async Task UpdateTravelingWayAsync(TravelingWay travelingWay)
        {
            ArgumentNullException.ThrowIfNull(travelingWay, nameof(travelingWay));

            var existingTravelingWay = await _travelingWayRepository.GetByIdAsync(travelingWay.Id)
                ?? throw new EntityNotFoundException($"TravelingWay with ID {travelingWay.Id} not found.");

            await _travelingWayRepository.UpdateAsync(existingTravelingWay);
        }

        public async Task DeleteTravelingWayAsync(int id)
        {
            var travelingWay = await _travelingWayRepository.GetByIdAsync(id)
                      ?? throw new EntityNotFoundException($"TravelingWay with ID {id} not found."); 

            await _travelingWayRepository.DeleteAsync(travelingWay);
        }
    }
}

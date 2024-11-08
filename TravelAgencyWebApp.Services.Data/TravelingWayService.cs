using TravelAgencyWebApp.Common.ErrorMessages;
using TravelAgencyWebApp.Data.Models;
using TravelAgencyWebApp.Data.Repository.Interfaces;
using TravelAgencyWebApp.Services.Data.Interfaces;

namespace TravelAgencyWebApp.Services.Data
{
    public class TravelingWayService : ITravelingWayService
    {
        private readonly IRepository<TravelingWay, int> _travelingWayRepository;

        public TravelingWayService(IRepository<TravelingWay, int> travelingWayRepository)
        {
            _travelingWayRepository = travelingWayRepository;
        }

        public async Task<IEnumerable<TravelingWay>> GetAllTravelingWaysAsync()
        {
            return await _travelingWayRepository.GetAllAsync();
        }

        public async Task<TravelingWay?> GetTravelingWayByIdAsync(int id)
        {
            return await _travelingWayRepository.GetByIdAsync(id);
        }

        public async Task AddTravelingWayAsync(TravelingWay travelingWay)
        {
            if (travelingWay == null)
            {
                throw new ArgumentNullException(nameof(travelingWay), "TravelingWay cannot be null.");
            }

            await _travelingWayRepository.AddAsync(travelingWay);
        }

        public async Task UpdateTravelingWayAsync(TravelingWay travelingWay)
        {
            if (travelingWay == null)
            {
                throw new ArgumentNullException(nameof(travelingWay), "TravelingWay cannot be null.");
            }

            var existingTravelingWay = await _travelingWayRepository.GetByIdAsync(travelingWay.Id);
            if (existingTravelingWay == null)
            {
                throw new EntityNotFoundException($"TravelingWay with ID {travelingWay.Id} not found.");
            }

            await _travelingWayRepository.UpdateAsync(travelingWay);
        }

        public async Task DeleteTravelingWayAsync(int id)
        {
            var travelingWay = await _travelingWayRepository.GetByIdAsync(id);
            if (travelingWay == null)
            {
                throw new EntityNotFoundException($"TravelingWay with ID {id} not found.");
            }

            await _travelingWayRepository.DeleteAsync(travelingWay);
        }
    }
}

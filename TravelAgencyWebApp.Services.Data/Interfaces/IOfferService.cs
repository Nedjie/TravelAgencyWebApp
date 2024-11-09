using TravelAgencyWebApp.Data.Models;
using TravelAgencyWebApp.ViewModels.Offer;

namespace TravelAgencyWebApp.Services.Data.Interfaces
{
    public interface IOfferService
    {
        Task<IEnumerable<Offer>> GetAllOffersAsync();
        Task<Offer?> GetOfferByIdAsync(int id);
        Task AddOfferAsync(OfferViewModel model);
        Task UpdateOfferAsync(Offer offer);
        Task DeleteOfferAsync(int id);
    }
}

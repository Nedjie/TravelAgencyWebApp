using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TravelAgencyWebApp.ViewModels.Booking
{
    public class ConfirmDeleteViewModel
    {

        [Key]
        [Comment("Booking identifier")]
        public int Id { get; set; }

        [Comment("User name")]
        public string? UserName { get; set; }

        [Comment("Offer title")]
        public string? OfferTitle { get; set; }
    }
}

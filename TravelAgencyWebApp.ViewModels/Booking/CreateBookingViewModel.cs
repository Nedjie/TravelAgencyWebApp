using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TravelAgencyWebApp.Common;

namespace TravelAgencyWebApp.ViewModels.Booking
{
    public class CreateBookingViewModel
    {
        [Required]
        [Comment("User identifier")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = DataConstants.BookingCheckInDateRequiredError)]
        [Comment("Check in date of booking")]
        public DateTime CheckInDate { get; set; }

        [Required(ErrorMessage = DataConstants.BookingCheckOutDateRequiredError)]
        [Comment("Check out date of booking")]
        public DateTime CheckOutDate { get; set; }

        [Comment("Offer identifier")]
        public int OfferId { get; set; } 
    }
}

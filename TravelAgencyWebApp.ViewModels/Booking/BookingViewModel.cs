using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TravelAgencyWebApp.Common;
using TravelAgencyWebApp.Common.Attributes;


namespace TravelAgencyWebApp.ViewModels.Booking
{
    public class BookingViewModel
    {
        [Key]
        [Comment("Booking identifier")]
        public int Id { get; set; }

        [Required(ErrorMessage = DataConstants.BookingUserIdRequiredError)]
        [Comment("User identifier")]
        public int UserId { get; set; }

        [Comment("User name")]
        public string? UserName { get; set; }

        [Comment("Offer identifier")]
        public int OfferId { get; set; }

        [Comment("Offer title")]
        public string? OfferTitle { get; set; }

        [Required(ErrorMessage = DataConstants.BookingCheckInDateRequiredError)]
        [Comment("Check in date of booking")]
        public DateTime CheckInDate { get; set; } 

        [Required(ErrorMessage = DataConstants.BookingCheckOutDateRequiredError)]
        [IsBefore("CheckInDate", ErrorMessage = DataConstants.BookingCheckOutDateIsBeforeCheckInDateError)]
        [Comment("Check out date of booking")]
        public DateTime CheckOutDate { get; set; } 
    }
}

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static TravelAgencyWebApp.Common.DataConstants;

namespace TravelAgencyWebApp.ViewModels.Booking
{
    public class EditBookingViewModel
    {
        [Key]
        [Comment("Booking identifier")]
        public int Id { get; set; }

 
        [Comment("User identifier")]
        public Guid? AgentId { get; set; }

        [Comment("User name")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = BookingCheckInDateRequiredError)]
        [Comment("Check in date of booking")]
        public DateTime CheckInDate { get; set; }

        [Required(ErrorMessage = BookingCheckOutDateRequiredError)]
        [Comment("Check out date of booking")]
        public DateTime CheckOutDate { get; set; }

        [Comment("Offer identifier")]
        public int OfferId { get; set; }

        public string? FullName { get; set; }

        public string? Email { get; set; }
    }
}

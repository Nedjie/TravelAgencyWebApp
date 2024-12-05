using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TravelAgencyWebApp.Common.Attributes;
using static TravelAgencyWebApp.Common.DataConstants;

namespace TravelAgencyWebApp.Data.Models
{
    public class Booking
    {
        [Key]
        [Comment("Booking identifier")]
        public int Id { get; set; }

        [Required(ErrorMessage = BookingUserIdRequiredError)]
        [Comment("User identifier")]
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public ApplicationUser? User { get; set; }

        [Comment("Agent identifier")]
        public Guid? AgentId { get; set; }

        [ForeignKey(nameof(AgentId))]
        public Agent? Agent { get; set; }

        [Required(ErrorMessage = BookingOfferIdRequiredError)]
        [Comment("Offer identifier")]
        public int OfferId { get; set; }

        [ForeignKey(nameof(OfferId))]
        public Offer? Offer { get; set; }

        [Required(ErrorMessage = BookingCheckInDateRequiredError)]
        [Comment("Check in date of booking")]
        public DateTime CheckInDate { get; set; }

        [Required(ErrorMessage = BookingCheckOutDateRequiredError)]
        [IsBefore("CheckInDate", ErrorMessage = BookingCheckOutDateIsBeforeCheckInDateError)]
        [Comment("Check out date of booking")]
        public DateTime CheckOutDate { get; set; }

		public bool IsDeleted { get; set; }
	}
}

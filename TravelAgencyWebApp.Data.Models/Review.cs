using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static TravelAgencyWebApp.Common.DataConstants;

namespace TravelAgencyWebApp.Data.Models
{
    public class Review
    {
        [Key]
        [Comment("Review identifier")]
        public int Id { get; set; }

        [Required(ErrorMessage = ReviewUserIdRequiredError)]
        [Comment("User identifier of review")]
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public ApplicationUser? User { get; set; }

        [Required(ErrorMessage = ReviewOfferIdRequiredError)]
        [Comment("Offer identifier")]
        public int OfferId { get; set; }

        [ForeignKey(nameof(OfferId))]
        public Offer? Offer { get; set; }

        [Required(ErrorMessage = ReviewTextRequiredError)]
        [StringLength(ReviewTextMaxLength,
            ErrorMessage = ReviewTextMaxLengthError)]
        [Comment("Review text")]
        public string ReviewText { get; set; } = null!;

        [Range(ReviewRangeMin, ReviewRangeMax,
            ErrorMessage = ReviewRatingRangeError)]
        [Comment("Review rating")]
        public int Rating { get; set; }

        [Comment("Review date")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static TravelAgencyWebApp.Common.DataConstants;

namespace TravelAgencyWebApp.Data.Models
{
    public class Offer
    {
        [Key]
        [Comment("Offer identifier")]
        public int Id { get; set; }

        [Required(ErrorMessage = OfferTitleRequiredError)]
        [StringLength(OfferTitleMaxLength,
            ErrorMessage = OfferTitleMaxLengthError)]
        [Comment("Offer title")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = OfferPriceRequiredError)]
        [Range(OfferPriceRangeMin, OfferPriceRangeMax,
            ErrorMessage = OfferPriceRangeError)]
        [Comment("Offer price")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = OfferDescriptionRequiredError)]
        [Comment("Offer description")]
        public string Description { get; set; } = null!;

        [Url(ErrorMessage = OfferImageUrlInvalidError)]
        [Comment("Offer image")]
        public string ImageUrl { get; set; } = null!;

        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}

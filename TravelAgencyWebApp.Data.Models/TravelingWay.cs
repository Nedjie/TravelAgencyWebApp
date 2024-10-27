using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static TravelAgencyWebApp.Common.DataConstants;

namespace TravelAgencyWebApp.Data.Models
{
    public class TravelingWay
    {
        [Key]
        [Comment("Traveling way identifier")]
        public int Id { get; set; }

        [Required(ErrorMessage = TravelingMethodRequiredError)]
        [StringLength(TravelingMethodMaxLength,
            ErrorMessage = TravelingMethodMaxLengthError)]
        [Comment("Traveling way method")]
        public string Method { get; set; } = null!;


        [StringLength(TravelingDescriptionMaxLength,
            ErrorMessage = TravelingDescriptionMaxLengthError)]
        [Comment("Traveling way description")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = TravelingCostRequiredError)]
        [Range(TravelingCostMin, TravelingConstMax,
           ErrorMessage = TravelingCostRangeError)]
        [Comment("Traveling way cost")]
        public decimal Cost { get; set; }

        [Comment("Offer identifier")]
        public int OfferId { get; set; }

        [ForeignKey(nameof(OfferId))]
        public Offer? Offer { get; set; }
    }
}

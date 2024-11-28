using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static TravelAgencyWebApp.Common.DataConstants;

namespace TravelAgencyWebApp.ViewModels.Offer
{
    public class ConfirmDeleteOfferViewModel
    {
        [Key]
        [Comment("Offer identifier")]
        public int Id { get; set; }

        [Required(ErrorMessage = OfferTitleRequiredError)]
        [StringLength(OfferTitleMaxLength,
               ErrorMessage = OfferTitleMaxLengthError)]
        [Comment("Offer title")]
        public string? Title { get; set; }

        [Required(ErrorMessage =OfferDescriptionRequiredError)]
        [Comment("Offer description")]
        public string? Description { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static TravelAgencyWebApp.Common.DataConstants;

namespace TravelAgencyWebApp.Data.Models
{
    public class Agent
    {
        [Key]
        [Comment("Agent identifier")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = FullNameRequiredError)]
        [StringLength(FullNameMaxLength,
            ErrorMessage = FullNameMaxLengthError)]
        [Comment("Agent Full Name")]
        public string FullName { get; set; } = null!; 

        [Required]
        [Comment("Agent email address")]
        public string Email { get; set; } = null!; 

        public virtual ICollection<Booking> Bookings { get; set; } = new HashSet<Booking>(); 
        public virtual ICollection<Offer> Offers { get; set; } = new HashSet<Offer>(); 

    }

}

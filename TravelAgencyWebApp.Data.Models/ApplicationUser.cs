using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using static TravelAgencyWebApp.Common.DataConstants;

namespace TravelAgencyWebApp.Data.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        [Required(ErrorMessage = FullNameRequiredError)]
        [StringLength(FullNameMaxLength,
            ErrorMessage = FullNameMaxLengthError)]
        public string FullName { get; set; } = null!;

        [StringLength(AddressMaxLength,
            ErrorMessage = AddressMaxLengthError)]
        public string? Address { get; set; } 

        public virtual ICollection<Booking> Bookings { get; set; } = new HashSet<Booking>();
        public virtual ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
    }
}

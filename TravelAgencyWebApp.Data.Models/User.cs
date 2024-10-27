using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static TravelAgencyWebApp.Common.DataConstants;

namespace TravelAgencyWebApp.Data.Models
{
    public class User
    {
        [Key]
        [Comment("User identifier")]
        public int Id { get; set; }

        [Required(ErrorMessage = UserNameRequiredError)]
        [MaxLength(UserNameMaxLength,
            ErrorMessage = UserNameMaxLengthError)]
        [Comment("User name")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = UserEmailRequiredError)]
        [EmailAddress(ErrorMessage = UserEmailInvalidError)]
        [Comment("User email")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = UserPasswordRequiredError)]
        [DataType(DataType.Password)]
        [StringLength(UserPasswordMaxLength,
            MinimumLength = UserPasswordMinLength,
            ErrorMessage = UserPasswordMinLengthError)]
        [Comment("User password")]
        public string Password { get; set; } = null!;

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}

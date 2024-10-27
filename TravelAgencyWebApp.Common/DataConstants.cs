namespace TravelAgencyWebApp.Common
{
    public static class DataConstants
    {
        // User Constants
        public const string UserNameRequiredError = "Name is required.";
        public const int UserNameMaxLength = 100;
        public const string UserNameMaxLengthError = "Name cannot be longer than {0} characters.";
        public const string UserEmailRequiredError = "Email is required.";
        public const string UserEmailInvalidError = "Invalid email format.";
        public const string UserPasswordRequiredError = "Password is required.";
        public const int UserPasswordMinLength = 6;
        public const int UserPasswordMaxLength = 100;
        public const string UserPasswordMinLengthError = "Password must be at least {0} characters long.";

        // Offer Constants
        public const int OfferTitleMaxLength = 200;
        public const string OfferTitleRequiredError = "Title is required.";
        public const string OfferTitleMaxLengthError = "Title cannot be longer than {0} characters.";
        public const string OfferPriceRequiredError = "Price is required.";
        public const double OfferPriceRangeMin = 0.01;
        public const double OfferPriceRangeMax = double.MaxValue;
        public const string OfferPriceRangeError = "Price must be greater than zero.";
        public const string OfferDescriptionRequiredError = "Description is required.";
        public const string OfferImageUrlInvalidError = "Invalid image URL format.";

        // Booking Constants
        public const string BookingUserIdRequiredError = "User ID is required.";
        public const string BookingOfferIdRequiredError = "Offer ID is required.";
        public const string BookingCheckInDateRequiredError = "Check-in date is required.";
        public const string BookingCheckOutDateRequiredError = "Check-out date is required.";
        public const string BookingCheckOutDateIsBeforeCheckInDateError = "Check-out date must be later than check-in date.";

        // Review Constants
        public const int ReviewTextMaxLength = 1000;
        public const string ReviewTextRequiredError = "Review text is required.";
        public const string ReviewTextMaxLengthError = "Review text cannot exceed 1000 characters.";
        public const string ReviewUserIdRequiredError = "User ID is required.";
        public const string ReviewOfferIdRequiredError = "Offer ID is required.";
        public const int ReviewRangeMin = 1;
        public const int ReviewRangeMax = 5;
        public const string ReviewRatingRangeError = "Rating must be between 1 and 5.";

        // Traveling Way Constants
        public const string TravelingMethodRequiredError = "Traveling method is required.";
        public const int TravelingMethodMaxLength = 100;
        public const string TravelingMethodMaxLengthError = "Traveling method cannot exceed 100 characters.";
        public const int TravelingDescriptionMaxLength = 500;
        public const string TravelingDescriptionMaxLengthError = "Description cannot exceed 500 characters.";
        public const string TravelingCostRequiredError = "Cost is required.";
        public const double TravelingCostMin = 0.01;
        public const double TravelingConstMax = double.MaxValue;
        public const string TravelingCostRangeError = "Cost must be greater than zero.";
    }
}

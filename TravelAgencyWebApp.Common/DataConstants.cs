namespace TravelAgencyWebApp.Common
{
    public static class DataConstants
    {
        // ApplicationUser and Agent Constants
        public const string FullNameRequiredError = "Name is required.";
        public const int FullNameMaxLength = 200;
        public const string FullNameMaxLengthError = "Full name cannot be longer than {0} characters";
        public const int AddressMaxLength = 250;
        public const string AddressMaxLengthError = "Address cannot be longer than {0} characters";

        // Offer Constants
        public const int OfferTitleMaxLength = 200;
        public const string OfferTitleRequiredError = "Title is required.";
        public const string OfferTitleMaxLengthError = "Title cannot be longer than {0} characters.";
        public const string OfferPriceRequiredError = "Price is required.";
        public const double OfferPriceRangeMin = 0.01;
        public const double OfferPriceRangeMax = double.MaxValue;
        public const string OfferPriceRangeError = "Price must be greater than zero.";
        public const string OfferDescriptionRequiredError = "Description is required.";
        public const string OfferCheckInDateRequiredError = "Check-in date is required.";
        public const string OfferCheckOutDateRequiredError = "Check-out date is required.";
        public const string OfferImageUrlInvalidError = "Invalid image URL format.";

        // Booking Constants
        public const string BookingUserIdRequiredError = "User ID is required.";
        public const string BookingOfferIdRequiredError = "Offer ID is required.";
        public const string BookingCheckInDateRequiredError = "Check-in date is required.";
        public const string BookingCheckOutDateRequiredError = "Check-out date is required.";
        public const string BookingCheckOutDateIsBeforeCheckInDateError = "Check-out date must be later than check-in date.";

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

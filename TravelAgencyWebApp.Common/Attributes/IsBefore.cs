using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace TravelAgencyWebApp.Common.Attributes
{
	public class IsBefore : ValidationAttribute
	{
		private readonly string _comparisonProperty;

		public IsBefore(string comparisonProperty)
		{
			_comparisonProperty = comparisonProperty;
		}

		protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            var comparisonValue = validationContext.ObjectType.GetProperty(_comparisonProperty)?.GetValue(validationContext.ObjectInstance, null);

            if (value is DateTime currentDate && comparisonValue is DateTime comparisonDate)
            {
                if (currentDate >= comparisonDate)
                {
                    return ValidationResult.Success!; 
                }
                return new ValidationResult(ErrorMessage ?? "The check-out date must be before the check-in date.");
            }

            return new ValidationResult("Both values must be valid dates.");
        }
    }
}


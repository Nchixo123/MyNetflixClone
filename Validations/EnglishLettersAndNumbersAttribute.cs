using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Validations;

public class EnglishLettersAndNumbersAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var regex = new Regex("^[a-zA-Z0-9]*$");
        ArgumentNullException.ThrowIfNull(value, "value cant be null");
        if (!regex.IsMatch(value.ToString()))
        {
            return new ValidationResult($"The {validationContext.DisplayName} field can only contain English letters and numbers.");
        }

        return ValidationResult.Success;
    }
}

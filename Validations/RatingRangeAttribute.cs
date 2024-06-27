using System.ComponentModel.DataAnnotations;

namespace Validations;

public class RatingRangeAttribute : ValidationAttribute
{
    private readonly int _min;
    private readonly int _max;

    public RatingRangeAttribute(int min, int max)
    {
        _min = min;
        _max = max;
    }

    public override bool IsValid(object value)
    {
        if (value == null)
        {
            return false;
        }

        if (int.TryParse(value.ToString(), out int rating))
        {
            if (rating >= _min && rating <= _max)
            {
                return true;
            }
        }

        return false;
    }

    public override string FormatErrorMessage(string name)
    {
        return $"The field {name} must be between {_min} and {_max}.";
    }
}

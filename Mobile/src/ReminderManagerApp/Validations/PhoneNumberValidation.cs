using ReminderManagerApp.Helpers;
using System.ComponentModel.DataAnnotations;

namespace ReminderManagerApp.Validations
{
    public class PhoneNumberValidation : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult(SystemMessage.FIELD_PHONE_REQUIRED);
            }

            var input = value.ToString();
            if (string.IsNullOrWhiteSpace(input))
            {
                return new ValidationResult(SystemMessage.FIELD_PHONE_REQUIRED);
            }

            if (PhoneNumberHelper.IsPhoneNumber(input))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(SystemMessage.FIELDS_PHONE_VALID);
            }
        }
    }
}

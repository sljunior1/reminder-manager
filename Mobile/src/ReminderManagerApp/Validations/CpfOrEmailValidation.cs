using ReminderManagerApp.Helpers;
using System.ComponentModel.DataAnnotations;

namespace ReminderManagerApp.Validations
{
    public class CpfOrEmailValidation : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult(SystemMessage.CPF_EMAIL_REQUIRED);
            }

            var input = value.ToString();
            if (string.IsNullOrWhiteSpace(input))
            {
                return new ValidationResult(SystemMessage.CPF_EMAIL_REQUIRED);
            }

            if (CpfHelper.IsCpf(input))
            {
                return ValidationResult.Success;
            }
            else if (EmailHelper.IsEmail(input))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(SystemMessage.FIELDS_CPF_EMAIL_VALID);
            }
        }
    }
}

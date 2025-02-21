using ReminderManagerApp.Helpers;
using System.ComponentModel.DataAnnotations;

namespace ReminderManagerApp.Validations
{
    public class CpfValidation: ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult(SystemMessage.FIELD_CPF_REQUIRED);
            }

            var input = value.ToString();
            if (string.IsNullOrWhiteSpace(input))
            {
                return new ValidationResult(SystemMessage.FIELD_CPF_REQUIRED);
            }

            if (CpfHelper.IsCpf(input))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(SystemMessage.FIELDS_CPF_VALID);
            }
        }
    }
}

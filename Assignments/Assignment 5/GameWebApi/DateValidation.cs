using System;
using System.ComponentModel.DataAnnotations;

namespace GameWebApi
{
    public class DateValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if ((DateTime)value > DateTime.Now)
                return new ValidationResult("Date is not from the past!");
            return ValidationResult.Success;
        }
    }
}
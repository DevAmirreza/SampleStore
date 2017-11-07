using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AYadollahibastani_C50_A03.Models.Validations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ValidateProductName : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                if (true)
                {
                    return new ValidationResult("Birth date can not be greater than current date.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
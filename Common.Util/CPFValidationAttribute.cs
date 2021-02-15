using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Common.Util
{
    public class CPFValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
                return false;

            if (string.IsNullOrWhiteSpace(value.ToString()))
                return false;

            string stringValue = value.ToString();

            value = Regex.Replace(stringValue, "[^0-9]+", "");

            return Validations.ValidateCPF(stringValue);
        }
    }
}

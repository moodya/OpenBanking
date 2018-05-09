using System;

namespace Banking.Api.Validation
{
    public class ValidationException : Exception
    {
        public ValidationException(ValidationResult result)
        {
            Result = result;
        }

        public ValidationResult Result { get; }
    }
}
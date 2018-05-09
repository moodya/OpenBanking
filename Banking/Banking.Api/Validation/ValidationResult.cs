using System.Collections.Generic;

namespace Banking.Api.Validation
{
    public class ValidationResult
    {
        public ValidationResult()
        {
            IsValid = true;
        }
        public ValidationResult(IEnumerable<string> error)
        {
            IsValid = false;
            Error = error;
        }

        public bool IsValid { get; }
        public IEnumerable<string> Error { get; }
    }
}
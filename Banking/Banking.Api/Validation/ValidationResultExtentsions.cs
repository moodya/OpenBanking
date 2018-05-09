namespace Banking.Api.Validation
{
    public static class ValidationResultExtentsions
    {
        public static ValidationResult ThrowIfNotValid(this ValidationResult result)
        {
            if (!result.IsValid)
            {
                throw new ValidationException(result);
            }

            return result;
        }
    }
}
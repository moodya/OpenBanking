namespace Banking.Api.Validation
{
    public interface IValidator<in T>
    {
        ValidationResult Validate(T value);
    }
}
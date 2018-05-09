using System.Collections.Generic;
using System.Linq;
using Banking.Api.Requests;

namespace Banking.Api.Validation
{
    public class AccountTransactionsRequestValidator : IAccountTransactionsRequestValidator
    {
        public ValidationResult Validate(AccountTransactionsRequest request)
        {
            var error = new List<string>();

            if (request == null)
            {
                error.Add("Provide a valid request object");
                return new ValidationResult(error);
            }

            if (string.IsNullOrWhiteSpace(request.BankName) || request.BankName.Length > 200)
            {
                error.Add("Provide a valid bank name. 1 - 200 digits");
            }

            if (string.IsNullOrWhiteSpace(request.AccountNumber) || request.AccountNumber.Length != 8)
            {
                error.Add("Provide a valid account number. 8 digits");
            }

            return error.Any() ? new ValidationResult(error) : new ValidationResult();
        }
    }
}
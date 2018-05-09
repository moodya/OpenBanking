using System.Collections.Generic;
using System.Linq;
using Banking.Api.Requests;

namespace Banking.Api.Validation
{
    public class AddAccountRequestValidator : IAddAccountRequestValidator
    {
        public ValidationResult Validate(AddAccountRequest request)
        {
            var error = new List<string>();

            if (request == null)
            {
                error.Add("Provide a valid request object");
                return new ValidationResult(error);
            }

            var account = request.Account;

            if (account == null)
            {
                error.Add("Provide a valid account object");
                return new ValidationResult(error);
            }

            if (account.Number.Length != 8)
            {
                error.Add("Provide a valid account number. 8 digits");
            }

            if (account.Bank == null)
            {
                error.Add("Provide a valid bank object");
            }

            if (account.Bank != null && (string.IsNullOrWhiteSpace(account.Bank.Name) || account.Bank.Name.Length > 200))
            {
                error.Add("Provide a valid bank name. 1 - 200 digits");
            }

            if (account.User == null)
            {
                error.Add("Provide a valid User object");
            }

            if (account.User != null && (string.IsNullOrWhiteSpace(account.User.Name) || account.User.Name.Length > 200))
            {
                error.Add("Provide a valid user name. 1 - 200 digits");
            }

            if (account.User != null && (string.IsNullOrWhiteSpace(account.User.Username) || account.User.Username.Length > 200))
            {
                error.Add("Provide a valid user username. 1 - 200 digits");
            }

            if (account.User != null && (string.IsNullOrWhiteSpace(account.User.Password) || account.User.Password.Length != 8))
            {
                error.Add("Provide a valid user password. 8 digits");
            }

            //TODO finsh validation, then refactor into seperate validator classes


            return error.Any() ? new ValidationResult(error) : new ValidationResult();
        }
    }
}
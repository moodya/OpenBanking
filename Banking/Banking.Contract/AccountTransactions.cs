using System.Collections.Generic;

namespace Banking.Contract
{
    public class AccountTransactions
    {
        public AccountBalance AccountBalance { get; set; }
        public IEnumerable<Transaction> Transactions { get; set; }
    }
}
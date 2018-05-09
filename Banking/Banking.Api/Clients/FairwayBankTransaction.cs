using System;

namespace Banking.Api.Clients
{
    public class FairWayBankTransaction
    {
        public double amount { get; set; }
        public string transactionInformation { get; set; }
        public string type { get; set; }
        public DateTime bookedDate { get; set; }
    }
}
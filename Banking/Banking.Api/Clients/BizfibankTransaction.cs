using System;

namespace Banking.Api.Clients
{
    public class BizfiBankTransaction
    {
        public double amount { get; set; }
        public string merchant { get; set; }
        public DateTime cleared_date { get; set; }
    }
}
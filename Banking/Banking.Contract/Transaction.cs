using System;

namespace Banking.Contract
{
    public class Transaction
    {
        public DateTime Date { get; set; }
        public string TypeDescription { get; set; }
        public int Type { get; set; }
        public double Amount { get; set; }
    }
}
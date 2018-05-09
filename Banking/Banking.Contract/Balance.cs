namespace Banking.Contract
{
    public class Balance
    {
        public double Value { get; set; }
        public double AvailableBalance { get; set; }
        public double Overdraft { get; set; }
    }
}
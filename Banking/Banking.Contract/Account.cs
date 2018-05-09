namespace Banking.Contract
{
    public class Account
    {
        public string Number { get; set; }
        public User User { get; set; }
        public Bank Bank { get; set; }
    }
}
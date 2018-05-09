namespace Banking.Model.Entities
{
    public class Account
    {
        public int User_Id { get; set; }
        public int Bank_Id { get; set; }
        public string Number { get; set; }
        public virtual Bank Bank { get; set; }
        public virtual User User { get; set; }
    }
}
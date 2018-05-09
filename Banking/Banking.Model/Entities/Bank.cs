using System.Collections.Generic;

namespace Banking.Model.Entities
{
    public class Bank
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ClientName { get; set; }
        public string ClientBaseAddress { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
    }
}
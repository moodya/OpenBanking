using System.Collections.Generic;

namespace Banking.Model.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
    }
}
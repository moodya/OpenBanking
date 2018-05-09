using System.Data.Entity;
using Banking.Model;
using Banking.Model.Entities;

namespace Banking.DatabaseDeployment
{
    public class SeedAndDropCreateDatabaseAlwaysLive : DropCreateDatabaseAlways<BankingContext>
    {
        protected override void Seed(BankingContext context)
        {
            var bizfiBank = new Bank
            {
                Name = "BizfiBank",
                ClientBaseAddress = "http://bizfibank-bizfitech.azurewebsites.net/api/v1/",
                ClientName = "BizfiBankAccountTransactionsHttpClient"
            };

            var fairWayBank = new Bank
            {
                Name = "FairWayBank",
                ClientBaseAddress = "http://fairwaybank-bizfitech.azurewebsites.net/api/v1/",
                ClientName = "FairWayBankAccountTransactionsHttpClient"
            };

            var testUser1 = new User {Name = "Brian Testerton", Username = "Bricheese", Password = "Monkeys1" };

            var testUser1AccountBizfiBank = new Account {Number = "66666666", Bank = bizfiBank, User = testUser1};
            var testUser1AccountFairwayBank = new Account {Number = "66666666", Bank = fairWayBank, User = testUser1};

            context.Accounts.AddRange(new[]
                {testUser1AccountBizfiBank, testUser1AccountFairwayBank});

            context.SaveChanges();

            base.Seed(context);
        }
    }
}
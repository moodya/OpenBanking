using System;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Banking.Model.Entities;
using Seterlund.CodeGuard;

namespace Banking.Model.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly Func<BankingContext> _bankingDataContextFactory;

        public AccountRepository(Func<BankingContext> bankingDataContextFactory)
        {
            Guard.That(() => bankingDataContextFactory).IsNotNull();
            
            _bankingDataContextFactory = bankingDataContextFactory;
        }

        public async Task<Account> GetSingleAsync(Expression<Func<Account, bool>> whereCondition)
        {
            using (var dataContext = _bankingDataContextFactory())
            {
                return await dataContext.Accounts
                    .Include(nameof(Account.Bank))
                    .Include(nameof(Account.User))
                    .FirstOrDefaultAsync(whereCondition);
            }
        }

        public async Task<Account> AddAsync(Account entity)
        {
            using (var dataContext = _bankingDataContextFactory())
            {
                entity.Bank = await dataContext.Banks.FirstOrDefaultAsync(x => x.Name == entity.Bank.Name);
                var newAccount = dataContext.Accounts.Add(entity);
                await dataContext.SaveChangesAsync();
                return newAccount;
            }
        }
    }
}
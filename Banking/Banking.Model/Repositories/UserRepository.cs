using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Banking.Model.Entities;
using Seterlund.CodeGuard;

namespace Banking.Model.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly Func<BankingContext> _bankingDataContextFactory;

        public UserRepository(Func<BankingContext> bankingDataContextFactory)
        {
            Guard.That(() => bankingDataContextFactory).IsNotNull();

            _bankingDataContextFactory = bankingDataContextFactory;
        }

        public async Task<User> GetSingleAsync(Expression<Func<User, bool>> whereCondition)
        {
            using (var dataContext = _bankingDataContextFactory())
            {
                return await dataContext.Users.FirstAsync(whereCondition);
            }
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            using (var dataContext = _bankingDataContextFactory())
            {
                return await dataContext.Users.ToListAsync();
            }
        }
    }
}
using System;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Banking.Model.Entities;
using Seterlund.CodeGuard;

namespace Banking.Model.Repositories
{
    public class BankRepository : IBankRepository
    {
        private readonly Func<BankingContext> _bankingDataContextFactory;

        public BankRepository(Func<BankingContext> bankingDataContextFactory)
        {
            Guard.That(() => bankingDataContextFactory).IsNotNull();

            _bankingDataContextFactory = bankingDataContextFactory;
        }
    
        public async Task<Bank> GetSingleAsync(Expression<Func<Bank, bool>> whereCondition)
        {
            using (var dataContext = _bankingDataContextFactory())
            {
                return await dataContext.Banks.FirstOrDefaultAsync(whereCondition);
            }
        }
    }
}
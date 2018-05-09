using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Banking.Model.Entities;

namespace Banking.Model.Repositories
{
    public interface IAccountRepository 
    {
        Task<Account> AddAsync(Account entity);
        Task<Account> GetSingleAsync(Expression<Func<Account, bool>> whereCondition);
    }
}
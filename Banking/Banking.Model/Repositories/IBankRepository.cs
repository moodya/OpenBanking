using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Banking.Model.Entities;

namespace Banking.Model.Repositories
{
    public interface IBankRepository 
    {
        Task<Bank> GetSingleAsync(Expression<Func<Bank, bool>> whereCondition);
    }
}
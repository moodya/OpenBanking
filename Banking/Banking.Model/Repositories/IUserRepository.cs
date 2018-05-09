using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Banking.Model.Entities;

namespace Banking.Model.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetSingleAsync(Expression<Func<User, bool>> whereCondition);
    }
}
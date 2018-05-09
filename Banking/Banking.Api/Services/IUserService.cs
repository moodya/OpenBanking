using System.Collections.Generic;
using System.Threading.Tasks;
using Banking.Contract;

namespace Banking.Api.Services
{
    public interface IUserService
    {
        Task<Account> AddAsync(Account account);
        Task<IEnumerable<User>> GetAsync();
    }
}
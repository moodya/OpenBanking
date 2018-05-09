using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Banking.Contract;
using Banking.Model.Repositories;
using Seterlund.CodeGuard;
using Account = Banking.Model.Entities.Account;

namespace Banking.Api.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public UserService(
            IUserRepository userRepository,
            IAccountRepository accountRepository,
            IMapper mapper)
        {
            Guard.That(() => userRepository).IsNotNull();
            Guard.That(() => accountRepository).IsNotNull();
            Guard.That(() => mapper).IsNotNull();

            _userRepository = userRepository;
            _accountRepository = accountRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<User>> GetAsync()
        {
            var modelUsers = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<User>>(modelUsers);
        }

        public async Task<Contract.Account> AddAsync(Contract.Account account)
        {
            var modelAccount = _mapper.Map<Account>(account);
            var newModelUser = await _accountRepository.AddAsync(modelAccount);
            return _mapper.Map<Contract.Account>(newModelUser);
        }
    }
}
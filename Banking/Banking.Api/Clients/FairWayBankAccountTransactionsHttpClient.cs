using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoMapper;
using Banking.Contract;
using Newtonsoft.Json;
using Seterlund.CodeGuard;

namespace Banking.Api.Clients
{
    public class FairWayBankAccountTransactionsHttpClient : IAccountTransactionsHttpClient
    {
        private readonly IMapper _mapper;
        private readonly HttpClient _client;
        
        public FairWayBankAccountTransactionsHttpClient(IMapper mapper, string baseAddress)
        {
            Guard.That(() => mapper).IsNotNull();
            Guard.That(() => baseAddress).IsNotNullOrWhiteSpace();
            
            _mapper = mapper;
            _client = new HttpClient { BaseAddress = new Uri(baseAddress) };
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<AccountTransactions> GetAsync(string accountNumber)
        {
            string requestUri = $"accounts/{accountNumber}/transactions";
            var result = _client.GetAsync(requestUri).Result;
            result.EnsureSuccessStatusCode();
            var content = await result.Content.ReadAsStringAsync();
            var transactions = JsonConvert.DeserializeObject<IEnumerable<FairWayBankTransaction>>(content);
            return new AccountTransactions
            {
                AccountBalance = new AccountBalance
                {
                    Account = null,//_mapper.Map<Banking.Contract.Account>(account),
                    Balance = null
                },
                Transactions = _mapper.Map<IEnumerable<Transaction>>(transactions)
            };
        }
    }
}
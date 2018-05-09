using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Banking.Contract;
using Newtonsoft.Json;

namespace Banking.Client.Test
{
    public class Program
    {
        static void Main(string[] args)
        {
            TestBankingApi();

            Console.ReadLine();

        }

        private static void TestBankingApi()
        {
            AddUsers();

            GetAllUsers();

            GetAccountTransactionsForUser();
        }
        
        private static HttpClient GetClient()
        {
            var client = new HttpClient { BaseAddress = new Uri("http://localhost:63641/api/v1/") };
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

        private static void AddUsers()
        {
            var number = new Random().Next(10000000, 99999999).ToString();
            var accountOne = new Account
            {
                Number = number,
                User = new User { Name = $"Test{number}", Username = $"Test{number}", Password = "12345678" },
                Bank = new Bank { Name = "BizfiBank" }
            };
            AddUser(accountOne);
        }

        private static void AddUser(Account account)
        {
            try
            {
                HttpClient client = GetClient();
                Console.WriteLine("Attempting to add a new user");
                var stringContent = new StringContent(JsonConvert.SerializeObject(account), Encoding.UTF8,
                    "application/json");
                var result = client.PostAsync("banking/user", stringContent).Result;
                result.EnsureSuccessStatusCode();
                Console.WriteLine(result.Content.ReadAsStringAsync().Result);
                Console.WriteLine(Environment.NewLine);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to add user {e}");
                Console.WriteLine(Environment.NewLine);
            }
        }

        private static void GetAllUsers()
        {
            try
            {
                HttpClient client = GetClient();
                Console.WriteLine("Attempting to get all users");
                var result = client.GetAsync("banking/users").Result;
                result.EnsureSuccessStatusCode();
                Console.WriteLine(result.Content.ReadAsStringAsync().Result);
                Console.WriteLine(Environment.NewLine);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to to get all users {e}");
                Console.WriteLine(Environment.NewLine);
            }
        }

        private static void GetAccountTransactionsForUser()
        {
            HttpClient client = GetClient();
            var byteArray = Encoding.ASCII.GetBytes("Bricheese|Monkeys1");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            var accountNumber = "66666666";
            GetAccountTransactions(client, "BizfiBank", accountNumber);
            GetAccountTransactions(client, "FairWayBank", accountNumber);
        }

        private static void GetAccountTransactions(HttpClient client, string bankName, string accountNumber)
        {
            try
            {
                Console.WriteLine("Attempting to get all account transactions");
                var result = client.GetAsync($"banking/bank/{bankName}/user/{accountNumber}/transactions").Result;
                result.EnsureSuccessStatusCode();
                Console.WriteLine(result.Content.ReadAsStringAsync().Result);
                Console.WriteLine(Environment.NewLine);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to to get all account transactions {e}");
                Console.WriteLine(Environment.NewLine);
            }
        }
    }
}

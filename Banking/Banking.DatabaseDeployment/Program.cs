using System;
using System.Data.Entity;
using Banking.Model;

namespace Banking.DatabaseDeployment
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (var db = new BankingContext())
                {
                    Database.SetInitializer<BankingContext>(new SeedAndDropCreateDatabaseAlwaysLive());

                    db.Database.Initialize(true);

                    Console.WriteLine("DB deployed successfully");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}

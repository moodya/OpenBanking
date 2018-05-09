using System;
using System.IO;
using Banking.Api.Authentication;
using Banking.Api.Clients;
using Banking.Api.Logging;
using Banking.Api.Services;
using Banking.Api.Validation;
using Banking.Model;
using Banking.Model.Repositories;
using log4net;
using log4net.Config;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace Banking.Api.Configuration
{
    public class ServicesPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            var configFileInfo = new FileInfo(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
            XmlConfigurator.ConfigureAndWatch(configFileInfo);
            container.RegisterSingleton<ILogger>(() => new Log4NetLogger(LogManager.GetLogger(typeof(Log4NetLogger))));
            container.RegisterSingleton<Func<BankingContext>>(() => new BankingContext());
            container.Register<IUserRepository, UserRepository>();
            container.Register<IAccountRepository, AccountRepository>();
            container.Register<IBankRepository, BankRepository>();
            container.Register<ITransactionService, TransactionService>();
            container.Register<IUserService, UserService>();
            container.Register<IAuthorisationParameterParser, AuthorisationParameterParser>();
            container.Register<IAuthenticationService, AuthenticationService>();
            container.Register<IAddAccountRequestValidator, AddAccountRequestValidator>();
            container.Register<IAccountTransactionsRequestValidator, AccountTransactionsRequestValidator>();
            container.Register<IAccountTransactionsHttpClientFactory, AccountTransactionsHttpClientFactory>();

        }
    }
}
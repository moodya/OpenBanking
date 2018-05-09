using System.Reflection;
using Banking.Api.Handlers;
using Banking.Api.Requests;
using Banking.Contract;
using MediatR;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace Banking.Api.Configuration
{
    public class MediatrPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<IMediator, Mediator>();
            container.Register<SingleInstanceFactory>(() => type => container.GetInstance(type));
            container.Register<MultiInstanceFactory>(() => type => container.GetAllInstances(type));

            Assembly[] assemblies = MediatrPackage.GetAssembliesContainingRequestOrNotificationHandlers();

            container.Register(typeof(IRequestHandler<,>), assemblies);
            container.Register(typeof(IAsyncRequestHandler<,>), assemblies);

            container.RegisterCollection(typeof(INotificationHandler<>), assemblies);
            container.RegisterCollection(typeof(IAsyncNotificationHandler<>), assemblies);

            container.RegisterDecorator(typeof(IAsyncRequestHandler<AccountTransactionsRequest, AccountTransactions>),
                typeof(SortedAccountTransactionsRequestHandler));

        }

        private static Assembly[] GetAssembliesContainingRequestOrNotificationHandlers()
        {
            return new[] {typeof(MediatrPackage).Assembly};
        }
    }
}
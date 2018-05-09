using System.Web.Http.Dependencies;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;

namespace Banking.Api.Configuration
{
    public class DependencyConfiguration
    {
        public DependencyConfiguration()
        {
            _container = new Container();

            _container.RegisterPackages(new[] { GetType().Assembly });

#if DEBUG
            Verify();
#endif
        }

        private readonly Container _container;

        public IDependencyResolver GetResolver()
        {
            return new SimpleInjectorWebApiDependencyResolver(_container);
        }

        public void Verify()
        {
            _container.Verify();
        }
    }
}
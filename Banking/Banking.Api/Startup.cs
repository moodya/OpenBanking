using System.Web.Http;
using System.Web.Http.Dependencies;
using Banking.Api;
using Banking.Api.Authentication;
using Banking.Api.Configuration;
using Banking.Api.Controllers;
using Banking.Api.Services;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace Banking.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var dependencyConfig = new DependencyConfiguration();
            IDependencyResolver dependencyResolver = dependencyConfig.GetResolver();
            var config = new HttpConfiguration { DependencyResolver = dependencyResolver, Initializer = c => c.EnsureInitialized() };

            config.UseGlobalRoutePrefix();
            config.Filters.Add(new BasicAuthenticationFilter(dependencyResolver.GetService<IAuthenticationService>(), dependencyResolver.GetService<IAuthorisationParameterParser>()));
            
            app.UseWebApi(config);
        }
    }
}

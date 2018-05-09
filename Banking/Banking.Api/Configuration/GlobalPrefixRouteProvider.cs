using System.Web.Http.Controllers;
using System.Web.Http.Routing;

namespace Banking.Api.Configuration
{
    public class GlobalPrefixRouteProvider : DefaultDirectRouteProvider
    {
        private readonly string _globalprefix;

        public GlobalPrefixRouteProvider(string globalprefix = "api/v{version:int}")
        {
            _globalprefix = globalprefix;
        }

        protected override string GetRoutePrefix(HttpControllerDescriptor controllerDescriptor)
        {
            string routePrefix = base.GetRoutePrefix(controllerDescriptor);
            return routePrefix == null ? _globalprefix : $"{(object)_globalprefix}/{(object)routePrefix}";
        }
    }
}
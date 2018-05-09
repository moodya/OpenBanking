using System.Web.Http;
using System.Web.Http.Routing;

namespace Banking.Api.Configuration
{
    public static class HttpConfigurationExtensions
    {
        public static HttpConfiguration UseGlobalRoutePrefix(this HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes((IDirectRouteProvider)new GlobalPrefixRouteProvider("api/v{version:int}"));
            return config;
        }
    }
}
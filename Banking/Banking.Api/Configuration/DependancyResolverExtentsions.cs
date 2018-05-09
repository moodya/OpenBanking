using System.Web.Http.Dependencies;

namespace Banking.Api.Configuration
{
    public static class DependancyResolverExtentsions
    {
        public static T GetService<T>(this IDependencyResolver dependencyResolver)
        {
            return (T) dependencyResolver.GetService(typeof(T));
        }
    }
}
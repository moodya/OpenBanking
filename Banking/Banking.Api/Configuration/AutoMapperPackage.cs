using AutoMapper;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace Banking.Api.Configuration
{
    public class AutoMapperPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperConfiguration>();
            });

            container.RegisterSingleton<IConfigurationProvider>(mapperConfig);

            container.Register(() => mapperConfig.CreateMapper(container.GetInstance));
        }
    }
}
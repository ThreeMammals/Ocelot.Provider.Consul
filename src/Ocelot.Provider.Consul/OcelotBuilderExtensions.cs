namespace Ocelot.Provider.Consul
{
    using Configuration.Repository;
    using DependencyInjection;
    using Logging;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    public static class OcelotBuilderExtensions
    {
        public static IOcelotBuilder AddConsul(this IOcelotBuilder builder)
        {
            ServiceDiscoveryFinderDelegate app = (provider, config, name) =>
            {
                var factory = provider.GetService<IOcelotLoggerFactory>();

                var consulFactory = provider.GetService<IConsulClientFactory>();

                var consulRegistryConfiguration = new ConsulRegistryConfiguration(config.Host, config.Port, name, config.Token);

                var consulServiceDiscoveryProvider = new ConsulServiceDiscoveryProvider(consulRegistryConfiguration, factory, consulFactory);

                if (config.Type?.ToLower() == "pollconsul")
                {
                    return new PollingConsulServiceDiscoveryProvider(config.PollingInterval, consulRegistryConfiguration.KeyOfServiceInConsul, factory, consulServiceDiscoveryProvider);
                }

                return consulServiceDiscoveryProvider;
            };

            builder.Services.AddSingleton<ServiceDiscoveryFinderDelegate>(app);
            builder.Services.TryAddSingleton<IConsulClientFactory, ConsulClientFactory>();
            return builder;
        }

        public static IOcelotBuilder AddConsulConfigurationPoller(this IOcelotBuilder builder)
        {
            builder.Services.AddHostedService<FileConfigurationPoller>();
            builder.Services.AddSingleton<IFileConfigurationRepository, ConsulFileConfigurationRepository>();
            return builder;
        }
    }
}

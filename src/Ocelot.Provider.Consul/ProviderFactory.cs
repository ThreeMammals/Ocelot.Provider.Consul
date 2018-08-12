namespace Ocelot.Provider.Consul
{
    using Logging;
    using Microsoft.Extensions.DependencyInjection;
    using ServiceDiscovery;

    public static class ProviderFactory
    {
        public static ServiceDiscoveryFinderDelegate Get = (provider, config, name) =>
        {
            var factory = provider.GetService<IOcelotLoggerFactory>();

            var consulFactory = provider.GetService<IConsulClientFactory>();

            var consulRegistryConfiguration = new ConsulRegistryConfiguration(config.Host, config.Port, name, config.Token);

            var consulServiceDiscoveryProvider = new ConsulServiceDiscoveryProvider(consulRegistryConfiguration, factory, consulFactory);

            if (config.Type?.ToLower() == "pollconsul")
            {
                return new PollingConsulServiceDiscoveryProvider(config.PollingInterval, factory, consulServiceDiscoveryProvider);
            }

            return consulServiceDiscoveryProvider;
        };
    }
}

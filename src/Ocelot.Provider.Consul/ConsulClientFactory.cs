namespace Ocelot.Provider.Consul
{
    using System;
    using global::Consul;
    using ServiceDiscovery.Configuration;

    public class ConsulClientFactory : IConsulClientFactory
    {
        public IConsulClient Get(ConsulRegistryConfiguration config)
        {
            return new ConsulClient(c =>
            {
                c.Address = new Uri($"http://{config.Host}:{config.Port}");

                if (!string.IsNullOrEmpty(config?.Token))
                {
                    c.Token = config.Token;
                }
            });
        }
    }
}

namespace Ocelot.Provider.Consul
{
    using global::Consul;
    using ServiceDiscovery.Configuration;

    public interface IConsulClientFactory
    {
        IConsulClient Get(ConsulRegistryConfiguration config);
    }
}

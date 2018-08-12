namespace Ocelot.Provider.Consul
{
    using System;
    using Configuration;
    using ServiceDiscovery.Providers;

    public delegate IServiceDiscoveryProvider ServiceDiscoveryFinderDelegate(IServiceProvider provider, ServiceProviderConfiguration config, string key);
}

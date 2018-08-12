namespace Ocelot.Provider.Consul
{
    using Configuration.Repository;
    using DependencyInjection;
    using Microsoft.Extensions.DependencyInjection;
    using ServiceDiscovery;

    public static class OcelotBuilderExtensions
    {
        public static IOcelotBuilder AddConsul(this IOcelotBuilder builder)
        {
            builder.Services.AddSingleton<ServiceDiscoveryFinderDelegate>(ProviderFactory.Get);
            builder.Services.AddSingleton<IConsulClientFactory, ConsulClientFactory>();
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

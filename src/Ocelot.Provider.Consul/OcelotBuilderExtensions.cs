namespace Ocelot.Provider.Consul
{
    using DependencyInjection;

    public static class OcelotBuilderExtensions
    {
        public static IOcelotBuilder AddCacheManager(this IOcelotBuilder builder)
        {
            return builder;
        }
    }
}

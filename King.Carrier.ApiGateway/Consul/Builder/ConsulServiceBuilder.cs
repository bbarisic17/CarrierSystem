using Consul;
using Ocelot.Logging;
using Ocelot.Provider.Consul.Interfaces;
using Ocelot.Provider.Consul;

namespace King.Carrier.ApiGateway.Consul.Builder;

public class ConsulServiceBuilder : DefaultConsulServiceBuilder
{
    public ConsulServiceBuilder(Func<ConsulRegistryConfiguration> configurationFactory, IConsulClientFactory clientFactory, IOcelotLoggerFactory loggerFactory)
        : base(configurationFactory, clientFactory, loggerFactory) { }
    protected override string GetDownstreamHost(ServiceEntry entry, Node node) => entry.Service.Address;
}

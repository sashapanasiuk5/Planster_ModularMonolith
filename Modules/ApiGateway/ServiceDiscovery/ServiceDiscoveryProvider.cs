using ApiGateway.Enums;
using Ocelot.Configuration;
using Ocelot.ServiceDiscovery.Providers;
using Ocelot.Values;

namespace ApiGateway.ServiceDiscovery;

public class ServiceDiscoveryProvider(IServiceProvider serviceProvider, DownstreamRoute downstreamRoute) : IServiceDiscoveryProvider
{
    private readonly IConfiguration _configuration = serviceProvider.GetService<IConfiguration>() ?? throw new InvalidOperationException();
    private readonly DownstreamRoute _downstreamRoute = downstreamRoute;
    
    public Task<List<Service>> GetAsync()
    {
        var serviceName = _downstreamRoute.ServiceName;
        var service = new Service(serviceName,
                                GetHostAndPort(serviceName),
                                serviceName.ToLower(),
                                "1.0",
                                []);
        
        return Task.FromResult(new List<Service>() { service });
    }

    private ServiceHostAndPort GetHostAndPort(string serviceName)
    {
        var serviceAddress = _configuration.GetValue<string>("Services:"+serviceName + "Address");
        var addressParts = serviceAddress.Split(':');
        return new ServiceHostAndPort(addressParts[0], int.Parse(addressParts[1]));
    }
}
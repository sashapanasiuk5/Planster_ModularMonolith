using ApiGateway.ServiceDiscovery;
using Infrastructure.Configuration;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.ServiceDiscovery;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddGoogleSecretsManager(builder.Environment)
    .AddOcelot();

ServiceDiscoveryFinderDelegate serviceDiscoveryFinder = (provider, config, route)
    => new ServiceDiscoveryProvider(provider, route);

builder.Services
    .AddSingleton(serviceDiscoveryFinder)
    .AddOcelot(builder.Configuration);

if (builder.Environment.IsDevelopment())
{
    builder.Logging.AddConsole();
}

var app = builder.Build();
await app.UseOcelot();
await app.RunAsync();
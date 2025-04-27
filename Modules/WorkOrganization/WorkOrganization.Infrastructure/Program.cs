using Bootstraper;
using Infrastructure;
using Microsoft.AspNetCore;
using WorkOrganization.Infrastructure;

public class Program
{
    private static readonly Func<WebHostBuilderContext, Startup> StartupFactory =
        (ctx) => new Startup(ctx.Configuration, (services, config) => services.AddWorkModule(config));
    public static async Task Main(string[] args) {  
        var host = BuildWebHost(args);
        await host.SeedDataAsync();
        host.Run();
    }  
    public static IWebHost BuildWebHost(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
               .UseStartup(StartupFactory)
               .Build();  
}  
using Infrastructure;
using Infrastructure.Utils;
using Microsoft.AspNetCore;

public class Program {  
    private static readonly Func<WebHostBuilderContext, Startup> StartupFactory =
        (ctx) => new Startup(ctx.Configuration, (services, config) => services.AddIdentityModule(config));
    public static async Task Main(string[] args) {  
        var host = BuildWebHost(args);
        host.Run();
    }  
    public static IWebHost BuildWebHost(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .UseStartup(StartupFactory)
            .Build(); 
}  
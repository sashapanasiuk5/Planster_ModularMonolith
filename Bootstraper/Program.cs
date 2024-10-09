using Bootstraper;
using Infrastructure.Utils;
using Microsoft.AspNetCore;

public class Program {  
    public static async Task Main(string[] args) {  
        var host = BuildWebHost(args);
        await host.SeedDataAsync();
        host.Run();
    }  
    public static IWebHost BuildWebHost(string[] args) => WebHost.CreateDefaultBuilder(args).UseStartup <Startup> ().Build();  
}  
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Infrastructure;

public class Startup
{
    private readonly Action<IServiceCollection, IConfiguration> _configureAction; 
    public Startup(IConfiguration configuration, Action<IServiceCollection, IConfiguration> configureAction)
    {
        Configuration = configuration;
        _configureAction = configureAction;
    }

    public IConfiguration Configuration { get; }
    
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSharedInfrastructure(Configuration);
        _configureAction.Invoke(services, Configuration);
        services.AddEndpointsApiExplorer();
        services.AddLogging();
        services.AddCors(options =>
        {
            options.AddPolicy(name: "FrontendOrigin",
                policy  =>
                {
                    policy.WithOrigins("http://localhost:5173")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                    
                    policy.WithOrigins("https://localhost:5173")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
        });
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
        });
    }
    
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseCors("FrontendOrigin");

        app.UseHttpsRedirection();
        
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
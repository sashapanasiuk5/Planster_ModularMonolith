using Infrastructure;
using Shared.Infrastructure;
using Microsoft.OpenApi.Models;
using Teams.Infrastructure;
using Users.Infrastructure;

namespace Bootstraper;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }
    
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSharedInfrastructure(Configuration);
        services.AddIdentityModule(Configuration);
        services.AddUsersModule(Configuration);
        services.AddTeamsModule(Configuration);
        services.AddEndpointsApiExplorer();
        services.AddLogging();
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
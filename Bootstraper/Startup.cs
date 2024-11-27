using Infrastructure;
using Shared.Infrastructure;
using Microsoft.OpenApi.Models;
using Teams.Infrastructure;
using Users.Infrastructure;
using WorkOrganization.Infrastructure;

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
        services.AddWorkModule(Configuration);
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
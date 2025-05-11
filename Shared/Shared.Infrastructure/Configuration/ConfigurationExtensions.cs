using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Configuration;

public static class ConfigurationExtensions
{
    public static IConfigurationBuilder AddGoogleSecretsManager(this IConfigurationBuilder configurationBuilder, IWebHostEnvironment environment)
    {
        if (!environment.IsDevelopment())
        {
            configurationBuilder.Add(new SecretManagerSource());   
        }
        return configurationBuilder;
    }
}
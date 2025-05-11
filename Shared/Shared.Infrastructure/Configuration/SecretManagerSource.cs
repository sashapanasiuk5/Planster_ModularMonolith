using Microsoft.Extensions.Configuration;

namespace Infrastructure.Configuration;

public class SecretManagerSource: IConfigurationSource
{
    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        return new SecretManagerConfigurationProvider();
    }
}
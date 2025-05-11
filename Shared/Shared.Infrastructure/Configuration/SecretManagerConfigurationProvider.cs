using Google.Api.Gax.ResourceNames;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.SecretManager.V1;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Configuration;

public class SecretManagerConfigurationProvider: ConfigurationProvider
{
    private SecretManagerServiceClient _client;
    public override void Load()
    {
        var projectId = Environment.GetEnvironmentVariable("PROJECT_ID");
        var projectName = new ProjectName(projectId);
        _client = SecretManagerServiceClient.Create();
        var secrets = _client.ListSecrets(projectName);
        foreach (var secret in secrets)
        {
            AddValue(secret);
        }
    }

    private void AddValue(Secret secret)
    {
        var secretVersionName = new SecretVersionName(secret.SecretName.ProjectId, secret.SecretName.SecretId, "latest");
        var secretVersion = _client.AccessSecretVersion(secretVersionName);
        
        var key = secret.SecretName.SecretId;
        key = key.Replace('_', ':');
        Set(key, secretVersion.Payload.Data.ToStringUtf8());
    }
}
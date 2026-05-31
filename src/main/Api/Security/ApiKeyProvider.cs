using AspNetCore.Authentication.ApiKey;

namespace JacksonVeroneze.NET.GRPCServer.Api.Security;

public class ApiKeyProvider(ILogger<ApiKeyProvider> logger) : IApiKeyProvider
{
    public async Task<IApiKey?> ProvideAsync(string key)
    {
        try
        {
            // write your validation implementation here and return an instance of a valid ApiKey or retun null for an invalid key.
            // return await _apiKeyRepository.GetApiKeyAsync(key);
            return new ApiKey(key, "test");
        }
        catch (System.Exception exception)
        {
            logger.LogError(exception, exception.Message);
            throw;
        }
    }
}

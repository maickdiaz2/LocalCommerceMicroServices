using Microsoft.Extensions.Configuration;

namespace LocalCommerce.Shared.Setup.Services;

public static class SecretManager
{
    public static void AddSecretManager(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        //TODO: create an awaiter project instead of .result everywhere in the config
        //string discoveredUrl = GetVaultUrl(serviceCollection.BuildServiceProvider()).Result;
        string? discoveredUrl = configuration.GetConnectionString("vaultUrl");
        serviceCollection.AddVaultService(configuration, discoveredUrl); 
    }

    // private static async Task<string> GetVaultUrl(IServiceProvider serviceProvider)
    // {
    //     var serviceDiscovery = serviceProvider.GetService<IServiceDiscovery>();
    //     return await serviceDiscovery?.GetFullAddress(DiscoveryServices.Secrets)!;
    // }
}
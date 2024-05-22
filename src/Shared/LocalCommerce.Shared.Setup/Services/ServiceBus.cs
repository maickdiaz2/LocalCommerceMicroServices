using LocalCommerce.Shared.Communication.RabbitMQ;
using Microsoft.Extensions.Configuration;

namespace LocalCommerce.Shared.Setup.Services;

public static class ServiceBus
{
  public static IServiceCollection AddServiceBusIntegrationPublisher(this IServiceCollection serviceCollection,
      IConfiguration configuration)
  {
    serviceCollection.AddRabbitMQ(GetRabbitMqSecretCredentials, GetRabbitMQHostName, configuration, "IntegrationPublisher");
    serviceCollection.AddRabbitMQPublisher<IntegrationMessage>();
    return serviceCollection;
  }

  public static IServiceCollection AddServiceBusIntegrationConsumer(this IServiceCollection serviceCollection,
      IConfiguration configuration)
  {
    serviceCollection.AddRabbitMQ(GetRabbitMqSecretCredentials, GetRabbitMQHostName, configuration, "IntegrationConsumer");
    serviceCollection.AddRabbitMqConsumer<IntegrationMessage>();
    return serviceCollection;
  }

  public static IServiceCollection AddServiceBusDomainPublisher(this IServiceCollection serviceCollection,
      IConfiguration configuration)
  {
    serviceCollection.AddRabbitMQ(GetRabbitMqSecretCredentials, GetRabbitMQHostName, configuration, "DomainPublisher");
    serviceCollection.AddRabbitMQPublisher<DomainMessage>();
    return serviceCollection;
  }

  public static IServiceCollection AddServiceBusDomainConsumer(this IServiceCollection serviceCollection,
      IConfiguration configuration)
  {
    serviceCollection.AddRabbitMQ(GetRabbitMqSecretCredentials, GetRabbitMQHostName, configuration, "DomainConsumer");
    serviceCollection.AddRabbitMqConsumer<DomainMessage>();
    return serviceCollection;
  }

  /// <summary>
  /// default option (KeyValue) to get credentials using Vault 
  /// </summary>
  /// <param name="serviceProvider"></param>
  /// <returns></returns>
  private static async Task<RabbitMQCredentials> GetRabbitMqSecretCredentials(IServiceProvider serviceProvider)
  {
    var secretManager = serviceProvider.GetService<ISecretManager>();
    return await secretManager!.Get<RabbitMQCredentials>("rabbitmq");
  }

  public static IServiceCollection AddHandlersInAssembly<T>(this IServiceCollection serviceCollection)
  {
    serviceCollection.Scan(scan => scan.FromAssemblyOf<T>()
        .AddClasses(classes => classes.AssignableTo<IMessageHandler>())
        .AsImplementedInterfaces()
        .WithTransientLifetime());

    ServiceProvider sp = serviceCollection.BuildServiceProvider();
    var listHandlers = sp.GetServices<IMessageHandler>();
    serviceCollection.AddConsumerHandlers(listHandlers);
    return serviceCollection;
  }

  private static async Task<string> GetRabbitMQHostName(IConfiguration configuration)
  {
    string RabittMQUrl = configuration.GetConnectionString("RabittMQUrl") ?? "";

    return await Task.FromResult(RabittMQUrl);
  }
}
using LocalCommerce.Shared.Communication.Consumer.Host;
using LocalCommerce.Shared.Communication.Consumer.Manager;
using LocalCommerce.Shared.Communication.Messages;
using LocalCommerce.Shared.Communication.Publisher.Domain;
using LocalCommerce.Shared.Communication.Publisher.Integration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LocalCommerce.Shared.Communication;

public static class CommunicationDependencyInjection
{
  public static void AddConsumer<TMessage>(this IServiceCollection serviceCollection)
  {
    serviceCollection.AddSingleton<IConsumerManager<TMessage>, ConsumerManager<TMessage>>();
    serviceCollection.AddSingleton<IHostedService, ConsumerHostedService<TMessage>>();
  }

  public static void AddPublisher<TMessage>(this IServiceCollection serviceCollection)
  {
    if (typeof(TMessage) == typeof(IntegrationMessage))
    {
      serviceCollection.AddIntegrationBusPublisher();
    }
    else if (typeof(TMessage) == typeof(DomainMessage))
    {
      serviceCollection.AddDomainBusPublisher();
    }
  }

  private static void AddIntegrationBusPublisher(this IServiceCollection serviceCollection)
  {
    serviceCollection.AddTransient<IIntegrationMessagePublisher, DefaultIntegrationMessagePublisher>();
  }


  private static void AddDomainBusPublisher(this IServiceCollection serviceCollection)
  {
    serviceCollection.AddTransient<IDomainMessagePublisher, DefaultDomainMessagePublisher>();
  }
}
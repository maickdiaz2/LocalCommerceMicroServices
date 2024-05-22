using LocalCommerce.Shared.Communication.Consumer;
using LocalCommerce.Shared.Communication.Consumer.Handler;
using LocalCommerce.Shared.Communication.Messages;
using LocalCommerce.Shared.Communication.Publisher;
using LocalCommerce.Shared.Communication.RabbitMQ.Consumer;
using LocalCommerce.Shared.Communication.RabbitMQ.Publisher;
using Microsoft.AspNetCore.JsonPatch.Adapters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace LocalCommerce.Shared.Communication.RabbitMQ;

public static class RabbitMQDependencyInjection
{
    public static void AddRabbitMQ(this IServiceCollection serviceCollection,
        Func<IServiceProvider, Task<RabbitMQCredentials>> rabbitMqCredentialsFactory,
        Func<IConfiguration, Task<string>> rabbitMqHostName,
        IConfiguration configuration, string name)
    {
        serviceCollection.AddRabbitMQ(configuration);
        serviceCollection.PostConfigure<RabbitMQSettings>(x =>
        {
            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            x.SetCredentials(rabbitMqCredentialsFactory.Invoke(serviceProvider).Result);
            x.SetHostName(rabbitMqHostName.Invoke(configuration).Result);
        });
    }

    /// <summary>
    /// this method is used when the credentials are inside the configuration. not recommended.
    /// </summary>
    public static void AddRabbitMQ(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.Configure<RabbitMQSettings>(configuration.GetSection("Bus:RabbitMQ"));
    }

    public static void AddConsumerHandlers(this IServiceCollection serviceCollection,
        IEnumerable<IMessageHandler> handlers)
    {
        serviceCollection.AddSingleton<IMessageHandlerRegistry>(new MessageHandlerRegistry(handlers));
        serviceCollection.AddSingleton<IHandleMessage, HandleMessage>();
    }

    public static void AddRabbitMqConsumer<TMessage>(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddConsumer<TMessage>();
        serviceCollection.AddSingleton<IMessageConsumer<TMessage>, RabbitMQMessageConsumer<TMessage>>();
    }

    public static void AddRabbitMQPublisher<TMessage>(this IServiceCollection serviceCollection)
        where TMessage : IMessage
    {
        serviceCollection.AddPublisher<TMessage>();
        serviceCollection.AddSingleton<IExternalMessagePublisher<TMessage>, RabbitMQMessagePublisher<TMessage>>();
    }
}
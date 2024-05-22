﻿using LocalCommerce.Shared.Communication.Messages;

namespace LocalCommerce.Shared.Communication.Publisher;

public interface IExternalMessagePublisher<in TMessage>
    where TMessage : IMessage
{
  Task Publish(TMessage message, string? routingKey = null, CancellationToken cancellationToken = default);
  Task PublishMany(IEnumerable<TMessage> messages, string? routingKey = null, CancellationToken cancellationToken = default);
}
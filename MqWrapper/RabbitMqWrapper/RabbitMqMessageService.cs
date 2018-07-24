using System;
using MqWrapper;
using MqWrapper.Attributes;
using MqWrapper.Messages;
using RabbitMqWrapper;
using RabbitMQ.Client;

namespace RabbitMqService
{
    public class RabbitMqMessageService : IMessageService
    {
        private ConnectionFactory factory;
        private IConnection _connection;
        private IModel _channel;
        private BroadcastMessageProcessor broadcastMessageProcessor;
        private DirectMessageProcessor directMessageProcessor;

        public RabbitMqMessageService(string connection, int port)
        {
            factory = new ConnectionFactory() { HostName = connection, Port = port };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            broadcastMessageProcessor = new BroadcastMessageProcessor();
            directMessageProcessor = new DirectMessageProcessor();
        }

        public void Publish(IMessage message)
        {
            Publish(message, "");
        }

        public void Publish(IMessage message, string route)
        {
            MessageAttribute messageAttribute = GetMessageAttribute(message.GetType());
            ValidateAttribute(messageAttribute);


            if (messageAttribute.IsBroadcast)
            {
                broadcastMessageProcessor.Publish(_channel, message.GetType().FullName, messageAttribute.Durable, message, route);
            }
            else
            {
                directMessageProcessor.Publish(_channel, message.GetType().FullName, messageAttribute.Durable, message);
            }
        }

        public void ListenMessage<T>(Action<T> callback) where T : IMessage
        {
            ListenMessage(callback, new string[] { });
        }

        public void ListenMessage<T>(Action<T> callback, string[] routes) where T : IMessage
        {
            MessageAttribute messageAttribute = GetMessageAttribute(typeof(T));
            ValidateAttribute(messageAttribute);

            if (messageAttribute.IsBroadcast)
            {
                var broadcastAttribute = (BroadcastMessageAttribute)messageAttribute;
                broadcastMessageProcessor.ListenRabbitMessage(_channel, typeof(T).FullName, messageAttribute.Durable, callback, routes, broadcastAttribute.Target);
            }
            else
            {
                directMessageProcessor.ListenRabbitMessage(_channel, typeof(T).FullName, messageAttribute.Durable, callback);
            }
        }

        private void ValidateAttribute(MessageAttribute messageAttribute)
        {
            if (messageAttribute == null)
            {
                throw new Exception("Can't listen a message with missing MessageAttribute!");
            }
        }

        private MessageAttribute GetMessageAttribute(Type messageType)
        {
            foreach (Attribute attribute in messageType.GetCustomAttributes(false))
            {
                if (attribute is MessageAttribute)
                {
                    return (MessageAttribute)attribute;
                }
            }
            return null;
        }

        public void Dispose()
        {
            if (_channel != null)
            {
                _channel.Dispose();
                _channel = null;
            }
            if (_connection != null)
            {
                _connection.Dispose();
                _connection = null;
            }
        }

    }
}

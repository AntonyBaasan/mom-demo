using System;
using System.Text;
using MqWrapper;
using MqWrapper.Messages;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMqService
{
    public class RabbitMqMessageService : IMessageService
    {
        private ConnectionFactory factory;
        private IConnection _connection;
        private IModel _channel;

        public RabbitMqMessageService(string connection, int port)
        {
            factory = new ConnectionFactory() { HostName = connection, Port = port };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public void Publish(IMessage message)
        {
            MessageAttribute messageAttribute = GetMessageAttribute(message.GetType());
            if (messageAttribute == null)
            {
                throw new Exception("Can't publish a message with missing MessageAttribute!");
            }

            if (messageAttribute.IsBroadcast)
            {
                BroadcastToRabbit(messageAttribute.ChannelName, messageAttribute.Durable, message.GetPayload());
            }
            else
            {
                QueueToRabbit(messageAttribute.ChannelName, messageAttribute.Durable, message.GetPayload());
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

        private void BroadcastToRabbit(string queueName, bool durable, Payload payload)
        {
        }

        private void QueueToRabbit(string queueName, bool durable, Payload payload)
        {
            _channel.QueueDeclare(queue: queueName,
                durable: durable,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            string contentAsJsonString = JsonConvert.SerializeObject(payload);
            var jsonPayload = Encoding.UTF8.GetBytes(contentAsJsonString);

            _channel.BasicPublish(exchange: "",
                routingKey: queueName,
                basicProperties: null,
                body: jsonPayload);
        }

        public void ListenMessage<T>(Action<Payload> callback) where T : IMessage
        {
            MessageAttribute messageAttribute = GetMessageAttribute(typeof(T));
            if (messageAttribute == null)
            {
                throw new Exception("Can't listen a message with missing MessageAttribute!");
            }

            if (messageAttribute.IsBroadcast)
            {
                //TODO: should it has to be different?
            }
            else
            {
                ListenRabbitMessage<T>(messageAttribute.ChannelName, messageAttribute.Durable, callback);
            }
        }

        public void ListenRabbitMessage<T>(string channelName, bool durable, Action<Payload> callback) where T : IMessage
        {
            _channel.QueueDeclare(queue: channelName,
                durable: durable,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var messagePayload  = Encoding.UTF8.GetString(ea.Body);
                var payload = JsonConvert.DeserializeObject<Payload>(messagePayload);
                callback(payload);
            };

            String consumerTag = _channel.BasicConsume(queue: channelName,
                autoAck: true,
                consumer: consumer);
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

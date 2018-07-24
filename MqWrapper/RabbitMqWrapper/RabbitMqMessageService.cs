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
                BroadcastToRabbit(messageAttribute.ChannelName, messageAttribute.Durable, message);
            }
            else
            {
                QueueToRabbit(messageAttribute.ChannelName, messageAttribute.Durable, message);
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

        private void BroadcastToRabbit(string queueName, bool durable, IMessage message)
        {
        }

        private void QueueToRabbit(string queueName, bool durable, IMessage message)
        {
            _channel.QueueDeclare(queue: queueName,
                durable: durable,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            string json = JsonConvert.SerializeObject(message);
            var jsonAsString = Encoding.UTF8.GetBytes(json);

            _channel.BasicPublish(exchange: "",
                routingKey: queueName,
                basicProperties: null,
                body: jsonAsString);
        }

        public void ListenMessage<T>(Action<T> callback) where T : IMessage
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

        public void ListenRabbitMessage<T>(string channelName, bool durable, Action<T> callback) where T : IMessage
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
                var msg = JsonConvert.DeserializeObject<T>(messagePayload);
                callback(msg);
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

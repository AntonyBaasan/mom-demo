using System;
using System.Text;
using MqService;
using MqService.Messages;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMqService
{
    public class RabbitMqMessageService : IMessageService
    {
        private ConnectionFactory factory;
        //private readonly string connection = "http://192.168.99.100:8080";

        public virtual void Start(string connection)
        {
            factory = new ConnectionFactory() { HostName = connection };
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

        private void BroadcastToRabbit(string queueName, bool durable, object content)
        {
        }

        private void QueueToRabbit(string queueName, bool durable, object content)
        {
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queueName,
                    durable: durable,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                string contentAsJsonString = JsonConvert.SerializeObject(content);
                var jsonPayload = Encoding.UTF8.GetBytes(contentAsJsonString);

                channel.BasicPublish(exchange: "",
                    routingKey: queueName,
                    basicProperties: null,
                    body: jsonPayload);
            }
        }

        public void ListenMessage<T>(Action callback) where T : IMessage
        {

            MessageAttribute messageAttribute = GetMessageAttribute(typeof(T));
            if (messageAttribute == null)
            {
                throw new Exception("Can't listen a message with missing MessageAttribute!");
            }

            if (messageAttribute.IsBroadcast)
            {
            }
            else
            {
                ListenRabbitMessage<T>(messageAttribute.ChannelName, messageAttribute.Durable, callback);
            }
        }

        public void ListenRabbitMessage<T>(string channelName, bool durable, Action callback) where T : IMessage
        {
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: channelName,
                    durable: durable,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(" [x] Received {0}", message);
                };

                channel.BasicConsume(queue: channelName,
                    autoAck: true,
                    consumer: consumer);
            }
        }
    }
}

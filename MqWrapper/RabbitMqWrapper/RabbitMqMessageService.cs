using System;
using System.Text;
using MqService;
using MqService.Messages;
using Newtonsoft.Json;
using RabbitMQ.Client;

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
            string channel = "";
            bool durable = false;
            bool isBroadcast = false;

            foreach (Attribute attribute in message.GetType().GetCustomAttributes(false))
            {
                MessageAttribute messageAttribute = (MessageAttribute)attribute;
                channel = messageAttribute.ChannelName;
                durable = messageAttribute.Durable;
                isBroadcast = messageAttribute.IsBroadcast;
            }

            if (isBroadcast)
            {
                BroadcastToRabbit(channel, durable, message.GetPayload());
            }
            else
            {
                QueueToRabbit(channel, durable, message.GetPayload());
            }
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
            throw new NotImplementedException();
        }
    }
}

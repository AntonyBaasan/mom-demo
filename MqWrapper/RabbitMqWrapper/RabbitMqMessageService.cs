using System;
using System.Text;
using MqService;
using MqService.Messages;
using RabbitMQ.Client;

namespace RabbitMqService
{
    public class RabbitMqMessageService : IMessageService
    {
        private ConnectionFactory factory;
        private readonly string connection = "http://192.168.99.100:8080";

        public virtual void Start()
        {
            factory = new ConnectionFactory() { HostName = connection };
        }

        public void Publish(IMessage message)
        {


        }

        private void BroadcastToRabbit(string queueName, bool durable, object content)
        {

        }
        private void SendToRabbit(string queueName, bool durable, object content)
        {
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queueName,
                    durable: durable,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                channel.BasicPublish(exchange: "",
                    routingKey: queueName,
                    basicProperties: null,
                    body: content);
            }
        }

        public void ListenMessage<T>(Action callback) where T : IMessage
        {
            throw new NotImplementedException();
        }
    }
}

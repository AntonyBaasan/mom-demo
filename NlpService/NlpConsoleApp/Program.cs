using System;
using MqService;
using NlpLibrary;
using RabbitMqService;

namespace NlpConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = "192.168.99.100"; 
            var port = 5672;
            var text = "Message From NLPService!";

            IMessageService messageService = new RabbitMqMessageService(connectionString, port);
            NlpService nlpService = new NlpService(messageService);

            nlpService.SendText(text);

            Console.ReadKey();
        }
    }
}

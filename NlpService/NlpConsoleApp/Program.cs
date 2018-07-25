using System;
using MqWrapper;
using MqWrapper.Messages;
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

            Console.WriteLine("NLP service");
            IMessageService messageService = new RabbitMqMessageService(connectionString, port);
            NlpService nlpService = new NlpService(messageService);

            while (true)
            {
                var inputString = Console.ReadLine();
                //nlpService.SendText(inputString);
                //Console.WriteLine("Sent a message: " + inputString);

                messageService.Publish(new SystemControllerMessage(), inputString);
            }
        }
    }
}

using System;
using ExecutionEngineLibrary;
using MqWrapper;
using MqWrapper.Messages;
using RabbitMqService;

namespace ExecutionEngineConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = "192.168.99.100";
            var port = 5672;

            Console.WriteLine("Execution Engine");
            IMessageService messageService = new RabbitMqMessageService(connectionString, port);
            ExecutionEngine engine = new ExecutionEngine(messageService);

            Console.WriteLine("Which user to listen:");
            var userId = Console.ReadLine();
            messageService.ListenMessage<UserNotificationMessage>((msg) =>
            {
                Console.WriteLine($"Got a message from {msg.UserId}, text: {msg.Text}");
            }, new string[] { userId });
            Console.ReadKey();

        }
    }
}

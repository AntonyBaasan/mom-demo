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
            var input = Console.ReadLine();
            messageService.ListenMessage<SystemControllerMessage>((msg) =>
            {
                Console.WriteLine("Got SystemControllerMessage!");
            }, new string[] { input });
            Console.ReadKey();

        }
    }
}

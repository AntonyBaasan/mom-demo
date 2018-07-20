using System;
using ExecutionEngineLibrary;
using MqService;
using RabbitMqService;

namespace ExecutionEngineConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = "192.168.99.100"; 
            var port = 5672;

            IMessageService messageService = new RabbitMqMessageService(connectionString, port);
            ExecutionEngine engine = new ExecutionEngine(messageService);

            Console.ReadKey();

        }
    }
}

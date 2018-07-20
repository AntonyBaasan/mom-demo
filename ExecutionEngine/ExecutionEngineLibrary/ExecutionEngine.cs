using System;
using System.Collections.Generic;
using System.Text;
using MqService;
using MqService.Messages;
using MqWrapper.Messages;

namespace ExecutionEngineLibrary
{
    public class ExecutionEngine
    {
        private readonly IMessageService _messageService;

        public ExecutionEngine(IMessageService messageService)
        {
            _messageService = messageService;

            _messageService.ListenMessage<ChatMessage>(OnChatReceived);
        }

        public void OnChatReceived()
        {
            Console.WriteLine("Got a chat message!");
            var message = new ExecutionMessage();
            message.SetContent("ExecutedObject1");

            _messageService.Publish(message);
        }
    }
}

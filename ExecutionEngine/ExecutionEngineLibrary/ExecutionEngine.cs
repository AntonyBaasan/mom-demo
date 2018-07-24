using System;
using System.Collections.Generic;
using MqWrapper;
using MqWrapper.Domain;
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

        public void OnChatReceived(ChatMessage msg)
        {
            List<Intent> list = msg.Intents;

            Console.WriteLine($"Got a chat message with {list.Count} intenst(s)!");
            var message = new ExecutionMessage();
            message.ResultText = "ExecutedObject1";
            _messageService.Publish(message);
        }
    }
}

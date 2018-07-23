using System;
using System.Collections.Generic;
using System.Text;
using MqWrapper;
using MqWrapper.Messages;
using Newtonsoft.Json;
using NlpLibrary;

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

        public void OnChatReceived(Payload payload)
        {
            var intent = JsonConvert.DeserializeObject<Intent>(payload.ContentAsJson);

            Console.WriteLine("Got a chat message!");
            var message = new ExecutionMessage();
            message.SetContent("ExecutedObject1");
            _messageService.Publish(message);
        }
    }
}

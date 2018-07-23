using System;
using MqWrapper;
using MqWrapper.Messages;

namespace NlpLibrary
{
    public class NlpService
    {
        private readonly IMessageService _messageService;

        public NlpService(IMessageService messageService)
        {
            _messageService = messageService;

            InitListeners();
        }

        private void InitListeners()
        {
            // TODO: listen execution response
            _messageService.ListenMessage<ExecutionMessage>(OnExecutionResponse);
        }

        private void OnExecutionResponse(Payload payload)
        {
            Console.WriteLine("Got a exec result message!");
        }

        public void SendText(string text)
        {
            //TODO: use SimpleParser or Chatbot to get FFO
            Intent intent = SendRequestToSimpleParserOrChatbot(text);

            //IExecutionObject execObj = ParseIntentToExecutionObject(intent);

            var message = new ChatMessage();
            message.SetContent(intent);
            _messageService.Publish(message);
        }

        //private IExecutionObject ParseIntentToExecutionObject(Intent intent)
        //{
        //    throw new NotImplementedException();
        //}

        private Intent SendRequestToSimpleParserOrChatbot(string text)
        {
            return new Intent { Name = "OpenFile" };
        }

        public void SendAudio(string text)
        {

        }
    }
}

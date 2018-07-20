using System;
using ExecutionEngineLibrary;
using MqService;
using MqService.Messages;

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
            _messageService.ListenMessage<BasicMessage>(CallBack);
        }

        private void CallBack() { }

        public void SendText(string text)
        {
            //TODO: use SimpleParser or Chatbot to get FFO
            Intent intent = SendRequestToSimpleParserOrChatbot(text);

            IExecutionObject execObj = ParseIntentToExecutionObject(intent);

            _messageService.Publish(new BasicMessage { Payload = execObj });

        }

        private IExecutionObject ParseIntentToExecutionObject(Intent intent)
        {
            throw new NotImplementedException();
        }

        private Intent SendRequestToSimpleParserOrChatbot(string text)
        {
            return new Intent { Name = "OpenFile" };
        }

        public void SendAudio(string text)
        {

        }
    }
}

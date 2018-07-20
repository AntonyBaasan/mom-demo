using System;
using System.Collections.Generic;
using System.Text;
using MqService;
using MqService.Messages;

namespace ExecutionEngineLibrary
{
    public class ExecutionEngine
    {
        private readonly IMessageService _messageService;

        public ExecutionEngine(IMessageService messageService)
        {
            _messageService = messageService;

            _messageService.ListenMessage<BasicMessage>(OnMessageReceive);
        }

        public void OnMessageReceive()
        {

        }
    }
}

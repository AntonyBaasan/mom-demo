using System;
using System.Collections.Generic;
using System.Text;
using MqService.Messages;

namespace MqService
{
    public interface IMessageService
    {
        void Start();

        void Publish(IMessage message);

        void ListenMessage<T>(Action callback) where T : IMessage;
    }
}

using System;
using MqService.Messages;

namespace MqService
{
    public interface IMessageService
    {
        void Start(string connection);

        void Publish(IMessage message);

        void ListenMessage<T>(Action callback) where T : IMessage;
    }
}

using System;
using MqService.Messages;

namespace MqService
{
    public interface IMessageService: IDisposable
    {
        void Publish(IMessage message);

        void ListenMessage<T>(Action callback) where T : IMessage;
    }
}

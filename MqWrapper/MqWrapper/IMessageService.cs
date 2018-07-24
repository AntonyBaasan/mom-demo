using System;
using MqWrapper.Messages;

namespace MqWrapper
{
    public interface IMessageService: IDisposable
    {
        void Publish(IMessage message);

        void ListenMessage<T>(Action<T> callback) where T : IMessage;
    }
}

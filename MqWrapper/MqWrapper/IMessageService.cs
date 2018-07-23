using System;
using MqWrapper.Messages;

namespace MqWrapper
{
    public interface IMessageService: IDisposable
    {
        void Publish(IMessage message);

        void ListenMessage<T>(Action<Payload> callback) where T : IMessage;
    }
}

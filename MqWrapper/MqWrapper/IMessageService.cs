using System;
using MqWrapper.Messages;

namespace MqWrapper
{
    public interface IMessageService: IDisposable
    {
        void Publish(IMessage message);

        void Publish(IMessage message, string route);

        void ListenMessage<T>(Action<T> callback) where T : IMessage;

        void ListenMessage<T>(Action<T> callback, string[] routes) where T : IMessage;
    }
}

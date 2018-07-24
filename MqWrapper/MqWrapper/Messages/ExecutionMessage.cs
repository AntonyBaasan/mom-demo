using MqWrapper.Attributes;

namespace MqWrapper.Messages
{
    [Message(IsBroadcast = false)]
    public class ExecutionMessage: AbstractMessage
    {
        public string ResultText;
    }
}

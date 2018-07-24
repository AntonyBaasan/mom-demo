using MqWrapper.Attributes;

namespace MqWrapper.Messages
{
    [DirectMessage]
    public class ExecutionMessage: AbstractMessage
    {
        public string ResultText;
    }
}

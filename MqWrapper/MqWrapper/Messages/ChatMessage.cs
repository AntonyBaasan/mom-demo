using MqWrapper.Messages;

namespace MqWrapper.Messages
{
    [MessageAttribute(ChannelName = "ChatMessage", IsBroadcast = false)]
    public class ChatMessage : AbstractMessage
    {
    }
}

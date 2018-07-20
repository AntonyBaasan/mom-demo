using MqWrapper.Messages;

namespace MqService.Messages
{
    [MessageAttribute(ChannelName = "ChatMessage", IsBroadcast = false)]
    public class ChatMessage : AbstractMessage
    {
    }
}

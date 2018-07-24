using System.Collections.Generic;
using MqWrapper.Domain;

namespace MqWrapper.Messages
{
    [MessageAttribute(ChannelName = "ChatMessage", IsBroadcast = false)]
    public class ChatMessage : AbstractMessage
    {
        public List<Intent> Intents;
    }
}

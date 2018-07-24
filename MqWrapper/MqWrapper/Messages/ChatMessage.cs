using System.Collections.Generic;
using MqWrapper.Attributes;
using MqWrapper.Domain;

namespace MqWrapper.Messages
{
    [Message(IsBroadcast = false)]
    public class ChatMessage : AbstractMessage
    {
        public List<Intent> Intents;
    }
}

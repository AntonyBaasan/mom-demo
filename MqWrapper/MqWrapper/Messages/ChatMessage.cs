using System.Collections.Generic;
using MqWrapper.Attributes;
using MqWrapper.Domain;

namespace MqWrapper.Messages
{
    [DirectMessage]
    public class ChatMessage : AbstractMessage
    {
        public List<Intent> Intents;
    }
}

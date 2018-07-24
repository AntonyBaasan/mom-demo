using System;

namespace MqWrapper.Attributes
{
    [Message(IsBroadcast = false, Durable = false)]
    public class DirectMessageAttribute : Attribute
    {
    }
}

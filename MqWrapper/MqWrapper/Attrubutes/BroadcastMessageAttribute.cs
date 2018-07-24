using System;

namespace MqWrapper.Attributes
{
    [Message(IsBroadcast = true, Durable = false)]
    public class BroadcastMessageAttribute : Attribute
    {
        public bool RouteRequired { get; set; }
    }
}

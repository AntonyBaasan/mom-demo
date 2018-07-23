using System;

namespace MqWrapper.Messages
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class MessageAttribute : Attribute
    {
        public string ChannelName { get; set; }
        public bool IsBroadcast { get; set; }
        public bool Durable { get; set; }
    }
}

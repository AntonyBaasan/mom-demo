using System;

namespace MqWrapper.Messages
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class MessageAttribute : Attribute
    {
        public string ChannelName { get; set; }
        public bool IsBroadcast { get; set; }
        /// <summary>
        /// Durable means the message will be saved on the disk, which 
        /// means the message will restored when MQ server is restarted.
        /// BUT, this will add more overhead (slower than non durable).
        /// </summary>
        public bool Durable { get; set; }
    }
}

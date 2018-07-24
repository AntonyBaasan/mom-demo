using System;

namespace MqWrapper.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public abstract class MessageAttribute : Attribute
    {
        public virtual bool IsBroadcast { get; set; }
        /// <summary>
        /// Durable means the message will be saved on the disk, which 
        /// means the message will restored when MQ server is restarted.
        /// BUT, this will add more overhead (slower than non durable).
        /// </summary>
        public virtual bool Durable { get; set; }

        public virtual bool RouteRequired { get; set; }

    }
}

using System;

namespace MqWrapper.Attributes
{
    public class DirectMessageAttribute : MessageAttribute
    {
        public override bool IsBroadcast { get => false; }
    }
}

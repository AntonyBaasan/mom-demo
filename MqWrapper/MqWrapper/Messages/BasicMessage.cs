using System;
using System.Collections.Generic;
using System.Text;

namespace MqService.Messages
{
    [MessageAttribute(ChannelName = "BasicMessage", IsBroadcast = false)]
    public class BasicMessage: IMessage
    {
        public object Payload {get; set;}
    }
}

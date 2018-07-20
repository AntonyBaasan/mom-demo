using System;
using System.Collections.Generic;
using System.Text;
using MqService.Messages;

namespace MqWrapper.Messages
{
    [Message(ChannelName = "ExecutionResult", IsBroadcast = false)]
    public class ExecutionMessage: AbstractMessage
    {
    }
}

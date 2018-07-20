using System;
using System.Collections.Generic;
using System.Text;
using MqWrapper.Messages;

namespace MqService.Messages
{
    public interface IMessage
    {
        Payload GetPayload();
    }
}

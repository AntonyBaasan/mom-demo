using System;
using System.Collections.Generic;
using System.Text;
using MqWrapper.Messages;

namespace MqWrapper.Messages
{
    public interface IMessage
    {
        Payload GetPayload();
    }
}

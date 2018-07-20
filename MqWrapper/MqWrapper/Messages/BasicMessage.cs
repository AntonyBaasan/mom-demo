using System;
using System.Collections.Generic;
using System.Text;
using MqWrapper.Messages;
using Newtonsoft.Json;

namespace MqService.Messages
{
    [MessageAttribute(ChannelName = "BasicMessage", IsBroadcast = false)]
    public class BasicMessage : IMessage
    {
        private Payload Payload { get; set; }

        public Payload GetPayload()
        {
            return Payload;
        }

        public void SetContent<T>(T content)
        {
            Payload = new Payload
            {
                TypeName = typeof(T).Name, 
                ContentAsJson = JsonConvert.SerializeObject(content)
            };
        }
    }
}

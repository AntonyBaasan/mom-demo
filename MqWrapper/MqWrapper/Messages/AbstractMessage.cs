﻿using MqService.Messages;
using Newtonsoft.Json;

namespace MqWrapper.Messages
{
    public class AbstractMessage: IMessage
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
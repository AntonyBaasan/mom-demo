using MqWrapper.Attributes;

namespace MqWrapper.Messages
{
    [BroadcastMessage(Target = BroadcastTarget.Instance)]
    public class SystemControllerMessage: IMessage
    {
    }
}

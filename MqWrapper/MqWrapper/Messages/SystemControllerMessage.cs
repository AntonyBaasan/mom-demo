using MqWrapper.Attributes;

namespace MqWrapper.Messages
{
    [BroadcastMessage(Target = BroadcastTarget.All)]
    public class SystemControllerMessage: IMessage
    {
    }
}

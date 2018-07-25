using MqWrapper.Attributes;

namespace MqWrapper.Messages
{
    /// <summary>
    /// Route is required because notification module should listen 
    /// individual user channed. User ID will be set as route name
    /// </summary>
    [DirectMessage(RouteRequired = true)]
    public class UserNotificationMessage: IMessage
    {
    }
}

namespace MqWrapper.Messages
{
    [Message(ChannelName = "ExecutionResult", IsBroadcast = false)]
    public class ExecutionMessage: AbstractMessage
    {
    }
}

namespace MqWrapper.Attributes
{
    /// <summary>
    /// This enum is used for horizontal scaling.
    /// Application - messages will be delivered one by one to duplicate applications round-robing way.
    /// Instance - messages will be delivered to all intances of same application.
    /// </summary>
    public enum BroadcastTarget
    {
        Application, // same/duplicate applications will have same queue name - messages will be delivered one by one
        Instance // same/duplication applicationss will have different queue name - messages will be delivered to all
    }
    public class BroadcastMessageAttribute : MessageAttribute
    {
        public override bool IsBroadcast { get => true; }

        public BroadcastTarget Target { get; set; } = BroadcastTarget.Application;
    }
}

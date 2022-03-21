using System;

namespace RRQMSkin.MVVM
{
    public class EventAction<TSender, TE> : IEventAction
    {
        public EventAction(string eventName, Action<TSender, TE> action)
        {
            this.eventName = eventName;
            this.action = action;
        }
        Action<TSender, TE> action;
        private string eventName;

        public string EventName => this.eventName;

        private void Event(TSender sender, TE e)
        {
            this.action?.Invoke(sender, e);
        }
    }
}

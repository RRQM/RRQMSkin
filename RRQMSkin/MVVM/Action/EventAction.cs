using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace RRQMSkin.MVVM
{
    public class EventAction<TSender,TE>: IEventAction
    {
        public EventAction(string eventName, Action<TSender, TE> action)
        {
            this.eventName = eventName;
            this.action = action;
        }
        Action<TSender, TE> action;
        private string eventName;

        public string EventName => eventName;

        private void Event(TSender sender, TE e)
        {
            this.action?.Invoke(sender, e);
        }
    }
}

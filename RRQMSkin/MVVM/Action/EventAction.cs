using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace RRQMSkin.MVVM
{
    public class EventAction
    {
        public EventAction(string eventName, Action<object, object> action)
        {
            this.eventName = eventName;
            this.action = action;
        }
        Action<object, object> action;
        private string eventName;

        public string EventName => eventName;

        private void Event(object sender, object e)
        {
            this.action?.Invoke(sender,e);
        }
    }
}

using Cyjb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace RRQMSkin.MVVM
{
    public static class EventCommands
    {
        public static IEnumerable<EventAction> GetEvents(DependencyObject obj)
        {
            return (IEnumerable<EventAction>)obj.GetValue(EventsProperty);
        }

        public static void SetEvents(DependencyObject obj, IEnumerable<EventAction> value)
        {
            obj.SetValue(EventsProperty, value);
        }

        // Using a DependencyProperty as the backing store for Events.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EventsProperty =
            DependencyProperty.RegisterAttached("Events", typeof(IEnumerable<EventAction>), typeof(EventCommands), new PropertyMetadata(null, OnCommandChanged));



        private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is IEnumerable<EventAction> eventActionInfos)
            {
                foreach (var eventActionInfo in eventActionInfos)
                {
                    if (!string.IsNullOrEmpty(eventActionInfo.EventName))
                    {
                        EventInfo eventInfo = d.GetType().GetEvent(eventActionInfo.EventName);
                        if (eventInfo == null)
                        {
                            throw new Exception($"没有找到名称为{eventActionInfo.EventName}的事件");
                        }
                        Delegate @delegate = DelegateBuilder.CreateDelegate(eventActionInfo, "Event", eventInfo.EventHandlerType, BindingFlags.NonPublic);
                        eventInfo.AddEventHandler(d, @delegate);
                    }
                    else
                    {
                        throw new Exception($"事件名不能为空");
                    }
                }
                
            }
        }

    }
}

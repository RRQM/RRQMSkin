using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;

namespace RRQMSkin.MVVM
{
    public static class EventCommands
    {
        public static IEnumerable<IEventAction> GetEvents(DependencyObject obj)
        {
            return (IEnumerable<IEventAction>)obj.GetValue(EventsProperty);
        }

        public static void SetEvents(DependencyObject obj, IEnumerable<IEventAction> value)
        {
            obj.SetValue(EventsProperty, value);
        }

        // Using a DependencyProperty as the backing store for Events.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EventsProperty =
            DependencyProperty.RegisterAttached("Events", typeof(IEnumerable<IEventAction>), typeof(EventCommands), new PropertyMetadata(null, OnCommandChanged));



        private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is IEnumerable eventActions)
            {
                foreach (IEventAction eventAction in eventActions)
                {
                    if (!string.IsNullOrEmpty(eventAction.EventName))
                    {
                        EventInfo eventInfo = d.GetType().GetEvent(eventAction.EventName);
                        if (eventInfo == null)
                        {
                            throw new Exception($"没有找到名称为{eventAction.EventName}的事件");
                        }
                        Delegate @delegate = Delegate.CreateDelegate(eventInfo.EventHandlerType, eventAction, "Event");

                        //Delegate @delegate2 = eventAction.Begin(eventInfo.EventHandlerType, typeof(object), typeof(MouseButtonEventArgs));
                        //Delegate @delegate = DelegateBuilder.CreateDelegate(eventAction, "Event", eventInfo.EventHandlerType, BindingFlags.NonPublic);
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

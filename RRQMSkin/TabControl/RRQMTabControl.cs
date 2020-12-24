using System;
using System.Collections.Specialized;
using System.Windows.Controls;

namespace RRQMSkin
{
    public class RRQMTabControl : TabControl
    {
        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsChanged(e);
            OnItemCountChanged?.Invoke(this);
        }

        public event Action<RRQMTabControl> OnItemCountChanged;
    }
}
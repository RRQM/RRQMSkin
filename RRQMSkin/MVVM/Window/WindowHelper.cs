using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RRQMSkin.MVVM
{
    public static class WindowHelper
    {
        // Using a DependencyProperty as the backing store for RRQMID.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RRQMIDProperty =
            DependencyProperty.Register("RRQMID", typeof(string), typeof(WindowHelper));

        public static string GetRRQMID(this Window window)
        {
            return window.GetValue(WindowHelper.RRQMIDProperty) as string;
        }
    }
}

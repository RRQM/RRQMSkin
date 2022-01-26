using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RRQMSkin.MVVM
{
    public class UIEngine<UI> where UI : UIElement
    {
        public UIEngine()
        {
            this.uis = new ConcurrentDictionary<object, UI>();
        }
        private readonly ConcurrentDictionary<object, UI> uis;
        public UI RegisterUI<TUI>(object[] args) where TUI : UI
        {
            TUI obj = (TUI)Activator.CreateInstance(typeof(TUI), args);
            this.uis.add
        }

        public UI GetUI<TUI>() where TUI : UI
        {

        }
    }
}

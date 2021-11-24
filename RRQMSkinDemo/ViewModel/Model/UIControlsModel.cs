using RRQMSkin.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RRQMSkinDemo.ViewModel.Model
{
    public class UIControlsModel : ViewModelBase
    {
        private string title;
        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        private string toolTipText;
        public string ToolTipText
        {
            get => toolTipText;
            set => SetProperty(ref toolTipText, value);
        }

        private UIElement child;
        public UIElement Child
        {
            get => child;
            set => SetProperty(ref child, value);
        }

        public ICommand ChangedClick =>
            new ExecuteCommand<object>((p) =>
            {
                var temp = p as Border;
                if (temp != null)
                {
                    UIInvoke(new Action(()=> 
                    {
                        temp.Child = Child;
                    }));
                }
            });
    }
}

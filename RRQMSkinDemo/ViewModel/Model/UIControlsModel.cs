using RRQMSkin.MVVM;
using System;
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
            get => this.title;
            set => this.SetProperty(ref this.title, value);
        }

        private string toolTipText;
        public string ToolTipText
        {
            get => this.toolTipText;
            set => this.SetProperty(ref this.toolTipText, value);
        }

        private UIElement child;
        public UIElement Child
        {
            get => this.child;
            set => this.SetProperty(ref this.child, value);
        }

        public ICommand ChangedClick =>
            new ExecuteCommand<object>((p) =>
            {
                var temp = p as Border;
                if (temp != null)
                {
                    this.UIInvoke(new Action(()=> 
                    {
                        temp.Child = this.Child;
                    }));
                }
            });
    }
}

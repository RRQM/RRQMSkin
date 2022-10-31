using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RRQMSkin.Primitives
{
    public class WindowTitle : UIElement
    {
        private Window window;

        public WindowTitle()
        {
            if (DesignerProperties.GetIsInDesignMode(this))
            {
                return;
            }
            this.window =Window.GetWindow(this);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.LeftButton== MouseButtonState.Pressed)
            {
                if (window != null)
                {
                    window.DragMove();
                }
            }
           
            base.OnMouseMove(e);
        }
    }
}

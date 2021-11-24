using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RRQMSkinDemo.View
{
    /// <summary>
    /// UserChat.xaml 的交互逻辑
    /// </summary>
    public partial class UserChat : Border
    {
        public UserChat()
        {
            InitializeComponent();
        }

        private void ListBox_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (!e.Handled)
            {
                e.Handled = true;
                var eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta);
                eventArg.RoutedEvent = Border.MouseWheelEvent;
                eventArg.Source = sender;
                var parent = ((System.Windows.Controls.Control)sender).Parent as UIElement;
                parent.RaiseEvent(eventArg);
            }
        }

        private void EmojiTabControlUC_Close(object sender, EventArgs e)
        {
            var emoji = EmojiTabControlUC.GetEmojiItem();
            if (emoji != null)
            {
                var container = new InlineUIContainer(new Image { Source = emoji.Image, Height = 25, Width = 25 }, rtb.CaretPosition);
                rtb.CaretPosition = container.ElementEnd;

                rtb.Focus();

                pop.IsOpen = false;
            }
        }
    }
}

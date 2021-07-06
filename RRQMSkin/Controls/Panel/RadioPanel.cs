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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RRQMSkin.Controls
{
    /// <summary>
    /// 切换面板
    /// </summary>
    public class RadioPanel : RadioButton
    {
        static RadioPanel()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RadioPanel), new FrameworkPropertyMetadata(typeof(RadioPanel)));
        }

        protected override void OnClick()
        {
            this.IsChecked = !(bool)this.IsChecked;
        }

        public object RadioContent
        {
            get { return (object)GetValue(RadioContentProperty); }
            set { SetValue(RadioContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RadioContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RadioContentProperty =
            DependencyProperty.Register("RadioContent", typeof(object), typeof(RadioPanel), new PropertyMetadata(null));


    }
}

using System.Windows;
using System.Windows.Controls;

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
            get => (object)this.GetValue(RadioContentProperty);
            set => this.SetValue(RadioContentProperty, value);
        }

        // Using a DependencyProperty as the backing store for RadioContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RadioContentProperty =
            DependencyProperty.Register("RadioContent", typeof(object), typeof(RadioPanel), new PropertyMetadata(null));


    }
}

using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace RRQMSkin.Windows
{
    /// <summary>
    /// DialogWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DialogWindow : Window
    {
        public DialogWindow(string mes)
        {
            this.InitializeComponent();
            this.Tb_Content.Text = mes;
            this.Loaded += this.DialogWindow_Loaded;
        }

        private void DialogWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DoubleAnimation animation_Top = new DoubleAnimation();
            animation_Top.To = this.Owner.Top;
            animation_Top.BeginTime = TimeSpan.FromSeconds(3);
            animation_Top.Duration = TimeSpan.FromSeconds(1);
            animation_Top.Completed += this.Animation_Top_Completed;
            this.BeginAnimation(Window.TopProperty, animation_Top);

            DoubleAnimation animation_Op = new DoubleAnimation();
            animation_Op.To = 0;
            animation_Op.BeginTime = TimeSpan.FromSeconds(3);
            animation_Op.Duration = TimeSpan.FromSeconds(1);
            this.BeginAnimation(Window.OpacityProperty, animation_Op);
        }

        private void Animation_Top_Completed(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

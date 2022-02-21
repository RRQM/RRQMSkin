using System;
using System.Windows;
using System.Windows.Controls;

namespace RRQM.Emoji
{
    /// <summary>
    /// EmojiALL.xaml 的交互逻辑
    /// </summary>
    public partial class EmojiALL : Border
    {
        public event EventHandler Close;
        public EmojiALL()
        {
            this.InitializeComponent();
        }

        private void Border_Loaded(object sender, RoutedEventArgs e)
        {
            var temp = this.DataContext as EmojiALLViewModel;
            temp.Init();
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            this.EmojiList.ScrollIntoView(0);
        }

        public Emoji GetEmojiItem() 
        {
            return this.EmojiItem;
        }

        private Emoji EmojiItem;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var temp = sender as Button;
            this.EmojiItem = temp.DataContext as Emoji;
            Close?.Invoke(this, null);
        }
    }
}

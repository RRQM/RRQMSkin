using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            InitializeComponent();
        }

        private void Border_Loaded(object sender, RoutedEventArgs e)
        {
            var temp = this.DataContext as EmojiALLViewModel;
            temp.Init();
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            EmojiList.ScrollIntoView(0);
        }

        public Emoji GetEmojiItem() 
        {
            return EmojiItem;
        }

        private Emoji EmojiItem;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var temp = sender as Button;
            EmojiItem = temp.DataContext as Emoji;
            Close?.Invoke(this, null);
        }
    }
}

//------------------------------------------------------------------------------
//  此代码版权归作者本人若汝棋茗所有
//  源代码使用协议遵循本仓库的开源协议及附加协议，若本仓库没有设置，则按MIT开源协议授权
//  CSDN博客：https://blog.csdn.net/qq_40374647
//  哔哩哔哩视频：https://space.bilibili.com/94253567
//  源代码仓库：https://gitee.com/RRQM_Home
//  交流QQ群：234762506
//  感谢您的下载和使用
//------------------------------------------------------------------------------
using RRQMMVVM;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;

namespace RRQMSkin.Demo.Display
{
    /// <summary>
    /// ChartsPanel.xaml 的交互逻辑
    /// </summary>
    public partial class ChartsPanel : WrapPanel
    {
        public ChartsPanel()
        {
            InitializeComponent();

            //List<Pie> vs = new List<Pie>();
            //vs.Add(new Pie() { Value = 1, PieBrush = Brushes.Red, Text = "张三" });
            //vs.Add(new Pie() { Value = 2, PieBrush = Brushes.Blue, Text = "李四" });
            //vs.Add(new Pie() { Value = 3, PieBrush = Brushes.Green, Text = "王五" });
            //vs.Add(new Pie() { Value = 4, PieBrush = Brushes.Yellow, Text = "赵六" });
            //this.pieChart.ItemsSource = vs;
        }
    }

    public class Pie : ObservableObject
    {
        private double value;

        public double Value
        {
            get { return value; }
            set { this.value = value; OnPropertyChanged(); }
        }

        private Brush pieBrush;

        public Brush PieBrush
        {
            get { return pieBrush; }
            set { pieBrush = value; OnPropertyChanged(); }
        }

        private double offset;

        public double Offset
        {
            get { return offset; }
            set { offset = value; OnPropertyChanged(); }
        }

        private string text;

        public string Text
        {
            get { return text; }
            set { text = value; OnPropertyChanged(); }
        }
    }
}
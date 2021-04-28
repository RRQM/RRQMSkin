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

            List<Pie> vs = new List<Pie>();
            vs.Add(new Pie() { Value = 1, PieBrush = Brushes.Red, Text = "张三" });
            vs.Add(new Pie() { Value = 2, PieBrush = Brushes.Blue, Text = "李四" });
            vs.Add(new Pie() { Value = 3, PieBrush = Brushes.Green, Text = "王五" });
            vs.Add(new Pie() { Value = 4, PieBrush = Brushes.Yellow, Text = "赵六" });
            this.pieChart.ItemsSource = vs;
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
//------------------------------------------------------------------------------
//  此代码版权归作者本人若汝棋茗所有
//  源代码使用协议遵循本仓库的开源协议及附加协议，若本仓库没有设置，则按MIT开源协议授权
//  CSDN博客：https://blog.csdn.net/qq_40374647
//  哔哩哔哩视频：https://space.bilibili.com/94253567
//  源代码仓库：https://gitee.com/RRQM_Home
//  交流QQ群：234762506
//  感谢您的下载和使用
//------------------------------------------------------------------------------
//------------------------------------------------------------------------------
using System.Windows;
using System.Windows.Controls;
using RRQMSkin.Charts.Primitives;
using RRQMSkin.DragDrop;

namespace RRQMSkin.Charts
{
    public class PieChartItem : Control
    {
        static PieChartItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PieChartItem), new FrameworkPropertyMetadata(typeof(PieChartItem)));
        }

        public PieChartItem()
        {
            this.Loaded += this.PieChartItem_Loaded;
        }

        private void PieChartItem_Loaded(object sender, RoutedEventArgs e)
        {
            UpdataValue();
        }

        internal Sector sector;
        internal DialText dialText;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.sector = (Sector)this.Template.FindName("sector", this);
            this.dialText = (DialText)this.Template.FindName("dialText", this);
        }

        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(PieChartItem), new PropertyMetadata(1.0, OnValueChanged));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(PieChartItem), new PropertyMetadata(null));

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PieChartItem pie)
            {
                pie.UpdataValue();
            }
        }

        private void UpdataValue()
        {
            PieChart pieChart = Utils.FindVisualParent<PieChart>(this);
            if (pieChart != null)
            {
                pieChart.UpdataChart();
            }
        }
    }
}
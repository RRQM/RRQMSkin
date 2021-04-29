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
using System;
using System.Windows;
using System.Windows.Media.Animation;
using RRQMSkin.Primitives;

namespace RRQMSkin.Charts
{
    [StyleTypedProperty(Property = "ItemContainerStyle", StyleTargetType = typeof(PieChartItem))]
    public class PieChart : RRQMChart
    {
        // Using a DependencyProperty as the backing store for PieBrushPath.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PieBrushPathProperty =
            DependencyProperty.Register("PieBrushPath", typeof(string), typeof(PieChart), new PropertyMetadata(null, OnPieBrushPathChanged));

        // Using a DependencyProperty as the backing store for ValuePath.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValuePathProperty =
            DependencyProperty.Register("ValuePath", typeof(string), typeof(PieChart), new PropertyMetadata(null, OnValuePathChanged));

        public PieChart()
        {
            Console.WriteLine(this.ItemContainerStyle);
        }

        static PieChart()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PieChart), new FrameworkPropertyMetadata(typeof(PieChart)));
        }

        public string PieBrushPath
        {
            get { return (string)GetValue(PieBrushPathProperty); }
            set { SetValue(PieBrushPathProperty, value); }
        }

        public string ValuePath
        {
            get { return (string)GetValue(ValuePathProperty); }
            set { SetValue(ValuePathProperty, value); }
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            var item = new PieChartItem();
            return item;
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is PieChartItem;
        }

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            base.PrepareContainerForItemOverride(element, item);

            if (element is PieChartItem pie)
            {
                PreparePieItem(pie, item);
            }
        }

        private static void OnPieBrushPathChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                if (d is PieChart pieChart)
                {
                    for (int i = 0; i < pieChart.Items.Count; i++)
                    {
                        PieChartItem item = (PieChartItem)pieChart.ItemContainerGenerator.ContainerFromIndex(i);
                        if (item == null)
                        {
                            return;
                        }
                        item.SetBinding(ForegroundProperty, e.NewValue.ToString());
                    }
                }
            }
        }

        private static void OnValuePathChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                if (d is PieChart pieChart)
                {
                    for (int i = 0; i < pieChart.Items.Count; i++)
                    {
                        PieChartItem item = (PieChartItem)pieChart.ItemContainerGenerator.ContainerFromIndex(i);
                        if (item == null)
                        {
                            return;
                        }
                        item.SetBinding(PieChartItem.ValueProperty, e.NewValue.ToString());
                    }
                }
            }
        }

        private void PreparePieItem(PieChartItem pie, object item)
        {
            if (pie == item)
                return;
            if (this.ValuePath != null)
            {
                pie.SetBinding(PieChartItem.ValueProperty, this.ValuePath);
            }

            if (this.PieBrushPath != null)
            {
                pie.SetBinding(PieChartItem.ForegroundProperty, this.PieBrushPath);
            }

            if (this.PieBrushPath != null)
            {
                pie.SetBinding(PieChartItem.TextProperty, this.DisplayMemberPath);
            }
        }

        public override void UpdataChart()
        {
            double sum = 0;

            for (int i = 0; i < this.Items.Count; i++)
            {
                PieChartItem item = (PieChartItem)this.ItemContainerGenerator.ContainerFromIndex(i);
                if (item == null)
                {
                    return;
                }
                sum += item.Value;
            }

            double start = this.StartAngle;

            for (int i = 0; i < this.Items.Count; i++)
            {
                PieChartItem item = (PieChartItem)this.ItemContainerGenerator.ContainerFromIndex(i);
                if (item.sector == null)
                {
                    return;
                }
                if (sum > 0)
                {
                    double end = start + item.Value / sum * 360;
                    if (this.IsAnimation)
                    {
                        AnimationDouble(item.sector, Sector.StartAngleProperty, start);
                        AnimationDouble(item.sector, Sector.EndAngleProperty, end);

                        AnimationDouble(item.dialText, DialText.StartAngleProperty, (end - start) / 2 + start);
                    }
                    else
                    {
                        item.sector.StartAngle = start;
                        item.sector.EndAngle = end;
                        item.dialText.StartAngle = (end - start) / 2 + start;
                    }
                    start = end;
                }
                else
                {
                    if (this.IsAnimation)
                    {
                        AnimationDouble(item.sector, Sector.EndAngleProperty, 0);
                        AnimationDouble(item.sector, Sector.EndAngleProperty, 360);
                    }
                    else
                    {
                        item.sector.StartAngle = 0;
                        item.sector.EndAngle = 360;
                    }
                }
            }
        }

        public double StartAngle
        {
            get { return (double)GetValue(StartAngleProperty); }
            set { SetValue(StartAngleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StartAngle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StartAngleProperty =
            DependencyProperty.Register("StartAngle", typeof(double), typeof(PieChart), new PropertyMetadata(0.0, OnStartAngleChanged));

        public bool IsAnimation
        {
            get { return (bool)GetValue(IsAnimationProperty); }
            set { SetValue(IsAnimationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAnimation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAnimationProperty =
            DependencyProperty.Register("IsAnimation", typeof(bool), typeof(PieChart), new PropertyMetadata(true));

        private static void OnStartAngleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PieChart chart)
            {
                chart.UpdataChart();
            }
        }

        protected override void OnDisplayMemberPathChanged(string oldDisplayMemberPath, string newDisplayMemberPath)
        {
            base.OnDisplayMemberPathChanged(oldDisplayMemberPath, newDisplayMemberPath);

            if (newDisplayMemberPath != null)
            {
                for (int i = 0; i < this.Items.Count; i++)
                {
                    PieChartItem item = (PieChartItem)this.ItemContainerGenerator.ContainerFromIndex(i);
                    if (item == null)
                    {
                        return;
                    }
                    item.SetBinding(PieChartItem.TextProperty, newDisplayMemberPath);
                }
            }
        }

        private void AnimationDouble(UIElement element, DependencyProperty property, double to)
        {
            DoubleAnimation animation = new DoubleAnimation();
            animation.To = to;
            animation.Duration = TimeSpan.FromSeconds(0.5);
            element.BeginAnimation(property, animation);
        }
    }
}
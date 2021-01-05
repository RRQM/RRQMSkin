using System;
using System.Windows;
using System.Windows.Controls;
using RRQMSkin.Charts.Primitives;

namespace RRQMSkin.Controls
{
    /// <summary>
    /// 速度表盘
    /// </summary>
    public class SpeedDial : Control
    {
        static SpeedDial()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SpeedDial), new FrameworkPropertyMetadata(typeof(SpeedDial)));
        }

        /// <summary>
        ///
        /// </summary>
        public SpeedDial()
        {
            this.SizeChanged += SpeedDial_SizeChanged;
        }

        private void SpeedDial_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double radius = Math.Min(this.ActualHeight, this.ActualWidth) / 2;
            pointer.PointerWidth = radius / 300 * 300;
        }

        private Pointer pointer;

        /// <summary>
        ///
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            pointer = (Pointer)this.Template.FindName("pointer", this);
        }

        /// <summary>
        /// 指针角度
        /// </summary>
        public double PointerAngle
        {
            get { return (double)GetValue(PointerAngleProperty); }
            private set { SetValue(PointerAngleProperty, value); }
        }

        /// <summary>
        /// 指针角度属性
        /// </summary>
        public static readonly DependencyProperty PointerAngleProperty =
            DependencyProperty.Register("PointerAngle", typeof(double), typeof(SpeedDial), new PropertyMetadata(150.0));

        /// <summary>
        /// 最大值
        /// </summary>
        public double MaxValue
        {
            get { return (double)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }

        /// <summary>
        /// 最大值属性
        /// </summary>
        public static readonly DependencyProperty MaxValueProperty =
            DependencyProperty.Register("MaxValue", typeof(double), typeof(SpeedDial), new PropertyMetadata(160.0));

        /// <summary>
        /// 最小值
        /// </summary>
        public double MinValue
        {
            get { return (double)GetValue(MinValueProperty); }
            set { SetValue(MinValueProperty, value); }
        }

        /// <summary>
        /// 最小值属性
        /// </summary>
        public static readonly DependencyProperty MinValueProperty =
            DependencyProperty.Register("MinValue", typeof(double), typeof(SpeedDial), new PropertyMetadata(0.0));

        /// <summary>
        /// 结束角度
        /// </summary>
        public double EndAngle
        {
            get { return (double)GetValue(EndAngleProperty); }
            set { SetValue(EndAngleProperty, value); }
        }

        /// <summary>
        /// 结束角属性
        /// </summary>
        public static readonly DependencyProperty EndAngleProperty =
            DependencyProperty.Register("EndAngle", typeof(double), typeof(SpeedDial), new PropertyMetadata(390.0));

        /// <summary>
        /// 开始角度
        /// </summary>
        public double StartAngle
        {
            get { return (double)GetValue(StartAngleProperty); }
            set { SetValue(StartAngleProperty, value); }
        }

        /// <summary>
        /// 开始角度
        /// </summary>
        public static readonly DependencyProperty StartAngleProperty =
            DependencyProperty.Register("StartAngle", typeof(double), typeof(SpeedDial), new PropertyMetadata(150.0));

        /// <summary>
        /// 速度
        /// </summary>
        public double Speed
        {
            get { return (double)GetValue(SpeedProperty); }
            set { SetValue(SpeedProperty, value); }
        }

        /// <summary>
        /// 速度属性
        /// </summary>
        public static readonly DependencyProperty SpeedProperty =
            DependencyProperty.Register("Speed", typeof(double), typeof(SpeedDial), new PropertyMetadata(0.0, new PropertyChangedCallback(OnChanged)));

        private static void OnChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // ((SpeedDial)d).InvalidateVisual();
            SpeedDial speedDial = (SpeedDial)d;
            if (speedDial.Speed >= speedDial.MinValue)
            {
                double angle = speedDial.EndAngle - speedDial.StartAngle;
                double diff = speedDial.MaxValue - speedDial.MinValue;
                double unit = angle / diff;

                speedDial.PointerAngle = speedDial.StartAngle + unit * speedDial.Speed;
            }
            else
            {
                speedDial.PointerAngle = speedDial.StartAngle;
            }
        }
    }
}
//------------------------------------------------------------------------------
//  此代码版权归作者本人若汝棋茗所有
//  源代码使用协议遵循本仓库的开源协议，若本仓库没有设置，则按MIT开源协议授权
//  CSDN博客：https://blog.csdn.net/qq_40374647
//  哔哩哔哩视频：https://space.bilibili.com/94253567
//  源代码仓库：https://gitee.com/RRQM_Home
//  交流QQ群：234762506
//  感谢您的下载和使用
//------------------------------------------------------------------------------
//------------------------------------------------------------------------------
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using RRQMSkin.Charts.Primitives;

namespace RRQMSkin.Controls
{
    /// <summary>
    /// 时钟
    /// </summary>
    public class Clock : Control
    {
        static Clock()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Clock), new FrameworkPropertyMetadata(typeof(Clock)));
        }

        /// <summary>
        ///
        /// </summary>
        public Clock()
        {
            this.SizeChanged += Clock_SizeChanged;
        }

        private void Clock_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ClockUpdata();
        }

        private void ClockUpdata()
        {
            double max = Math.Min(this.ActualHeight, this.ActualWidth);
            secondPointer.PointerWidth = 160 / 300.0 * max;
            minutePointer.PointerWidth = 130 / 300.0 * max;
            hourPointer.PointerWidth = 80 / 300.0 * max;

            hourDial.InvalidateVisual();
            minuteDial.InvalidateVisual();
            dialText.InvalidateVisual();
        }

        private Pointer secondPointer;
        private Pointer minutePointer;
        private Pointer hourPointer;
        private Dial hourDial;
        private Dial minuteDial;
        private DialText dialText;

        /// <summary>
        ///
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            secondPointer = (Pointer)this.Template.FindName("secondPointer", this);
            minutePointer = (Pointer)this.Template.FindName("minutePointer", this);
            hourPointer = (Pointer)this.Template.FindName("hourPointer", this);
            hourDial = (Dial)this.Template.FindName("hourDial", this);
            minuteDial = (Dial)this.Template.FindName("minuteDial", this);
            dialText = (DialText)this.Template.FindName("dialText", this);
        }

        /// <summary>
        /// 显示时间
        /// </summary>
        [Category("RRQM"), Description("显示时间")]
        public TimeSpan Time
        {
            get { return (TimeSpan)GetValue(TimeProperty); }
            set { SetValue(TimeProperty, value); }
        }

        /// <summary>
        /// 显示时间属性
        /// </summary>
        public static readonly DependencyProperty TimeProperty =
            DependencyProperty.Register("Time", typeof(TimeSpan), typeof(Clock), new PropertyMetadata(new TimeSpan(0, 0, 0), OnTimeChanged));

        private static void OnChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Clock clock = (Clock)d;
            clock.InvalidateVisual();
        }

        private static void OnTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Clock clock = (Clock)d;
            if (clock.secondPointer != null && clock.minutePointer != null && clock.hourPointer != null)
            {
                clock.secondPointer.RatioAngle = -90 + clock.Time.Seconds * 6;
                clock.minutePointer.RatioAngle = -90 + clock.Time.Minutes * 6 + clock.Time.Seconds / 60.0 * 6;
                clock.hourPointer.RatioAngle = -90 + clock.Time.Hours * 30 + clock.Time.Minutes / 60.0 * 30;
            }
        }
    }
}
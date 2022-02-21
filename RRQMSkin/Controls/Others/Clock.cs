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
using RRQMSkin.Primitives;
using System;
using System.ComponentModel;
using System.Windows;

namespace RRQMSkin.Controls
{
    /// <summary>
    /// 时钟
    /// </summary>
    public class Clock : RRQMControl
    {
        static Clock()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Clock), new FrameworkPropertyMetadata(typeof(Clock)));
        }

        private Pointer secondPointer;
        private Pointer minutePointer;
        private Pointer hourPointer;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.secondPointer = (Pointer)this.Template.FindName("secondPointer", this);
            this.minutePointer = (Pointer)this.Template.FindName("minutePointer", this);
            this.hourPointer = (Pointer)this.Template.FindName("hourPointer", this);
        }

        /// <summary>
        /// 显示时间
        /// </summary>
        [Category("RRQM"), Description("显示时间")]
        public TimeSpan Time
        {
            get => (TimeSpan)this.GetValue(TimeProperty);
            set => this.SetValue(TimeProperty, value);
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
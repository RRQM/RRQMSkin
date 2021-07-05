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

namespace RRQMSkin.Controls
{
    /// <summary>
    /// 速度表盘
    /// </summary>
    public class SpeedDial : RRQMControl
    {
        static SpeedDial()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SpeedDial), new FrameworkPropertyMetadata(typeof(SpeedDial)));
        }

        /// <summary>
        ///
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
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
            //((SpeedDial)d).InvalidateVisual();
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
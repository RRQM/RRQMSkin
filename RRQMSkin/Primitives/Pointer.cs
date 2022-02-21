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
using System.Windows.Media;

namespace RRQMSkin.Primitives
{
    /// <summary>
    /// 指针
    /// </summary>
    public class Pointer : RRQMShape
    {
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        protected override Geometry CreatGeometry()
        {
            double radius = Math.Min(this.ActualHeight, this.ActualWidth) / 2;

            if (radius > 0)
            {
                PathGeometry pathGeometry = new PathGeometry();
                PathFigure pathFigure1 = new PathFigure();
                pathFigure1.StartPoint = new Point(this.ActualWidth / 2 + (1 - this.RatioCenter.X) * this.PointerWidth, this.ActualHeight / 2);

                LineSegment lineSegment1 = new LineSegment();
                lineSegment1.Point = new Point(this.ActualWidth / 2 - this.RatioCenter.X * this.PointerWidth, this.ActualHeight / 2 + this.PointerHeight);
                pathFigure1.Segments.Add(lineSegment1);

                ArcSegment arcSegment = new ArcSegment();
                arcSegment.SweepDirection = SweepDirection.Clockwise;
                arcSegment.Point = new Point(this.ActualWidth / 2 - this.RatioCenter.X * this.PointerWidth, this.ActualHeight / 2 - this.PointerHeight);
                arcSegment.Size = new Size(this.PointerHeight, this.PointerHeight);
                pathFigure1.Segments.Add(arcSegment);

                pathFigure1.IsClosed = true;

                pathGeometry.Figures.Add(pathFigure1);

                pathGeometry.Transform = new RotateTransform(this.RatioAngle, this.ActualWidth / 2 - this.RatioCenter.X * this.PointerWidth + this.RatioCenter.X * this.PointerWidth, this.ActualHeight / 2 + (this.RatioCenter.Y - 0.5) * this.PointerHeight);
                return pathGeometry;
            }
            return new PathGeometry();
        }

        /// <summary>
        /// 半径旋转角度
        /// </summary>
        public double RatioAngle
        {
            get => (double)this.GetValue(RatioAngleProperty);
            set => this.SetValue(RatioAngleProperty, value);
        }

        /// <summary>
        /// 半径旋转角度属性
        /// </summary>
        public static readonly DependencyProperty RatioAngleProperty =
            DependencyProperty.Register("RatioAngle", typeof(double), typeof(Pointer), new PropertyMetadata(0.0, OnChanged));

        /// <summary>
        /// 指针宽度
        /// </summary>
        public double PointerHeight
        {
            get => (double)this.GetValue(PointerHeightProperty);
            set => this.SetValue(PointerHeightProperty, value);
        }

        /// <summary>
        /// 指针宽度属性
        /// </summary>
        public static readonly DependencyProperty PointerHeightProperty =
            DependencyProperty.Register("PointerHeight", typeof(double), typeof(Pointer), new PropertyMetadata(10.0, new PropertyChangedCallback(OnChanged)));

        /// <summary>
        /// 指针长度
        /// </summary>
        public double PointerWidth
        {
            get => (double)this.GetValue(PointerWidthProperty);
            set => this.SetValue(PointerWidthProperty, value);
        }

        /// <summary>
        /// 指针长度属性
        /// </summary>
        public static readonly DependencyProperty PointerWidthProperty =
            DependencyProperty.Register("PointerWidth", typeof(double), typeof(Pointer), new PropertyMetadata(100.0, new PropertyChangedCallback(OnChanged)));

        /// <summary>
        /// 旋转中心点
        /// </summary>
        public Point RatioCenter
        {
            get => (Point)this.GetValue(RatioCenterProperty);
            set => this.SetValue(RatioCenterProperty, value);
        }

        /// <summary>
        /// 旋转中心点属性
        /// </summary>
        public static readonly DependencyProperty RatioCenterProperty =
            DependencyProperty.Register("RatioCenter", typeof(Point), typeof(Pointer), new PropertyMetadata(new Point(0.3, 0.5), new PropertyChangedCallback(OnChanged)));

        private static void OnChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Pointer)d).InvalidateVisual();
        }
    }
}
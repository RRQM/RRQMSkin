using System;
using System.Windows;
using System.Windows.Media;

namespace RRQMSkin
{
    /// <summary>
    /// 刻度类型
    /// </summary>
    public class Dial : RRQMShape
    {
        /// <summary>
        ///
        /// </summary>
        public Dial()
        {
            this.SizeChanged += Dial_SizeChanged;
        }

        private void Dial_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.InvalidateVisual();
        }

        /// <summary>
        /// 线条颜色
        /// </summary>
        public new Brush Stroke { get; set; } = Brushes.Black;

        /// <summary>
        /// 线条粗细
        /// </summary>
        public new double StrokeThickness { get; set; } = 1;

        /// <summary>
        /// 最大环比例
        /// </summary>
        public double MaxRadiusRatio
        {
            get { return (double)GetValue(MaxRadiusRatioProperty); }
            set { SetValue(MaxRadiusRatioProperty, value); }
        }

        /// <summary>
        /// 最大环比例属性
        /// </summary>
        public static readonly DependencyProperty MaxRadiusRatioProperty =
            DependencyProperty.Register("MaxRadiusRatio", typeof(double), typeof(Dial),
                new PropertyMetadata(0.6, new PropertyChangedCallback(OnChanged)));

        /// <summary>
        /// 最小环比例
        /// </summary>
        public double MinRadiusRatio
        {
            get { return (double)GetValue(MinRadiusRatioProperty); }
            set { SetValue(MinRadiusRatioProperty, value); }
        }

        /// <summary>
        /// 最小环比例属性
        /// </summary>
        public static readonly DependencyProperty MinRadiusRatioProperty =
            DependencyProperty.Register("MinRadiusRatio", typeof(double), typeof(Dial), new PropertyMetadata(0.3, new PropertyChangedCallback(OnChanged)));

        /// <summary>
        /// 单位角度
        /// </summary>
        public double TickAngle
        {
            get { return (double)GetValue(TickAngleProperty); }
            set { SetValue(TickAngleProperty, value); }
        }

        /// <summary>
        /// 单位角度属性
        /// </summary>
        public static readonly DependencyProperty TickAngleProperty =
            DependencyProperty.Register("TickAngle", typeof(double), typeof(Dial), new PropertyMetadata(30.0, new PropertyChangedCallback(OnChanged)));

        /// <summary>
        /// 单位数量
        /// </summary>
        public int TickCount
        {
            get { return (int)GetValue(TickCountProperty); }
            set { SetValue(TickCountProperty, value); }
        }

        /// <summary>
        /// 单位数量属性
        /// </summary>
        public static readonly DependencyProperty TickCountProperty =
            DependencyProperty.Register("TickCount", typeof(int), typeof(Dial), new PropertyMetadata(5, new PropertyChangedCallback(OnChanged)));

        /// <summary>
        /// 开始角度
        /// </summary>
        public double StartAngle
        {
            get { return (double)GetValue(StartAngleProperty); }
            set { SetValue(StartAngleProperty, value); }
        }

        /// <summary>
        /// 开始角度属性
        /// </summary>
        public static readonly DependencyProperty StartAngleProperty =
            DependencyProperty.Register("StartAngle", typeof(double), typeof(Dial), new PropertyMetadata(0.0, new PropertyChangedCallback(OnChanged)));

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        protected override Geometry CreatGeometry()
        {
            double radius = Math.Min(this.ActualHeight, this.ActualWidth) / 2;

            if (radius > 0)
            {
                double minWidthRadius = this.MinRadiusRatio * this.ActualWidth / 2;
                double minHeightRadius = this.MinRadiusRatio * this.ActualHeight / 2;

                double maxWidthRadius = this.MaxRadiusRatio * this.ActualWidth / 2;
                double maxHeightRadius = this.MaxRadiusRatio * this.ActualHeight / 2;
                GeometryGroup geometryGroup = new GeometryGroup();

                for (int i = 0; i < TickCount; i++)
                {
                    LineGeometry lineGeometry = new LineGeometry();
                    lineGeometry.StartPoint = new Point(minWidthRadius * Math.Cos((StartAngle + i * TickAngle) * Math.PI / 180) + radius, minHeightRadius * Math.Sin((StartAngle + i * TickAngle) * Math.PI / 180) + radius);
                    lineGeometry.EndPoint = new Point(maxWidthRadius * Math.Cos((StartAngle + i * TickAngle) * Math.PI / 180) + radius, maxHeightRadius * Math.Sin((StartAngle + i * TickAngle) * Math.PI / 180) + radius);
                    geometryGroup.Children.Add(lineGeometry);
                }

                return geometryGroup;
            }

            return new PathGeometry();
        }

        private static void OnChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Dial)d).InvalidateVisual();
        }
    }
}
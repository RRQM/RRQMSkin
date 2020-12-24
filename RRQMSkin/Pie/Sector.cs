using System;
using System.Windows;
using System.Windows.Media;

namespace RRQMSkin
{
    /// <summary>
    /// 扇形环
    /// </summary>
    public class Sector : RRQMShape
    {
        /// <summary>
        ///
        /// </summary>
        public Sector()
        {
            this.SizeChanged += Sector_SizeChanged;
        }

        private void Sector_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.InvalidateVisual();
        }

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
            DependencyProperty.Register("MaxRadiusRatio", typeof(double), typeof(Sector),
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
            DependencyProperty.Register("MinRadiusRatio", typeof(double), typeof(Sector), new PropertyMetadata(0.3, new PropertyChangedCallback(OnChanged)));

        /// <summary>
        /// 结束角度
        /// </summary>
        public double EndAngle
        {
            get { return (double)GetValue(EndAngleProperty); }
            set { SetValue(EndAngleProperty, value); }
        }

        /// <summary>
        /// 结束角度属性
        /// </summary>
        public static readonly DependencyProperty EndAngleProperty =
            DependencyProperty.Register("EndAngle", typeof(double), typeof(Sector), new PropertyMetadata(60.0, new PropertyChangedCallback(OnChanged)));

        /// <summary>
        /// 圆形样式
        /// </summary>
        public RoundStyle RoundStyle
        {
            get { return (RoundStyle)GetValue(RoundStyleProperty); }
            set { SetValue(RoundStyleProperty, value); }
        }

        /// <summary>
        /// 圆形样式属性
        /// </summary>
        public static readonly DependencyProperty RoundStyleProperty =
            DependencyProperty.Register("RoundStyle", typeof(RoundStyle), typeof(Sector), new PropertyMetadata(RoundStyle.Nono, OnChanged));

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
            DependencyProperty.Register("StartAngle", typeof(double), typeof(Sector), new PropertyMetadata(0.0, new PropertyChangedCallback(OnChanged)));

        /// <summary>
        /// 填充类型
        /// </summary>
        public FillRule FillRule
        {
            get { return (FillRule)GetValue(FillRuleProperty); }
            set { SetValue(FillRuleProperty, value); }
        }

        /// <summary>
        /// 填充类型属性
        /// </summary>
        public static readonly DependencyProperty FillRuleProperty =
            DependencyProperty.Register("FillRule", typeof(FillRule), typeof(Sector), new PropertyMetadata(FillRule.Nonzero, OnChanged));

        /// <summary>
        /// 获取绘图类型
        /// </summary>
        /// <returns></returns>
        protected override Geometry CreatGeometry()
        {
            double radius = Math.Min(this.ActualHeight, this.ActualWidth) / 2;

            double widthRadius = this.ActualWidth / 2;
            double heightRadius = this.ActualHeight / 2;

            if (radius > 0 && this.EndAngle - this.StartAngle < 360)
            {
                double minRadius = this.MinRadiusRatio * radius;
                double maxRadius = this.MaxRadiusRatio * radius;

                Point p1 = new Point(minRadius * Math.Cos(StartAngle * Math.PI / 180) + widthRadius, minRadius * Math.Sin(StartAngle * Math.PI / 180) + heightRadius);
                Point p2 = new Point(minRadius * Math.Cos(EndAngle * Math.PI / 180) + widthRadius, minRadius * Math.Sin(EndAngle * Math.PI / 180) + heightRadius);

                Point p3 = new Point(maxRadius * Math.Cos(StartAngle * Math.PI / 180) + widthRadius, maxRadius * Math.Sin(StartAngle * Math.PI / 180) + heightRadius);
                Point p4 = new Point(maxRadius * Math.Cos(EndAngle * Math.PI / 180) + widthRadius, maxRadius * Math.Sin(EndAngle * Math.PI / 180) + heightRadius);

                PathGeometry pathGeometry = new PathGeometry();

                PathFigure pathFigure1 = new PathFigure();
                pathFigure1.StartPoint = p1;

                ArcSegment arcSegment1 = new ArcSegment();
                if (this.EndAngle - this.StartAngle > 180)
                {
                    arcSegment1.IsLargeArc = true;
                }
                arcSegment1.SweepDirection = SweepDirection.Clockwise;
                arcSegment1.Point = p2;
                arcSegment1.Size = new Size(minRadius, minRadius);
                pathFigure1.Segments.Add(arcSegment1);

                if (this.RoundStyle == RoundStyle.Nono || this.RoundStyle == RoundStyle.Start)
                {
                    LineSegment lineSegment2 = new LineSegment();
                    lineSegment2.Point = p4;
                    pathFigure1.Segments.Add(lineSegment2);
                }
                else
                {
                    ArcSegment arc1 = new ArcSegment();
                    arc1.Point = p4;
                    arc1.Size = new Size((maxRadius - minRadius) / 2, (maxRadius - minRadius) / 2);
                    pathFigure1.Segments.Add(arc1);
                }

                ArcSegment arcSegment2 = new ArcSegment();
                if (this.EndAngle - this.StartAngle > 180)
                {
                    arcSegment2.IsLargeArc = true;
                }
                arcSegment2.SweepDirection = SweepDirection.Counterclockwise;
                arcSegment2.Point = p3;
                arcSegment2.Size = new Size(maxRadius, maxRadius);
                pathFigure1.Segments.Add(arcSegment2);

                if (this.RoundStyle == RoundStyle.Nono || this.RoundStyle == RoundStyle.End)
                {
                    pathFigure1.IsClosed = true;
                }
                else
                {
                    ArcSegment arc1 = new ArcSegment();
                    arc1.Point = p1;
                    arc1.Size = new Size((maxRadius - minRadius) / 2, (maxRadius - minRadius) / 2);
                    pathFigure1.Segments.Add(arc1);
                }

                pathGeometry.FillRule = this.FillRule;
                pathGeometry.Figures.Add(pathFigure1);

                return pathGeometry;
            }
            else if (radius > 0 && this.EndAngle - this.StartAngle >= 360)
            {
                double minRadius = this.MinRadiusRatio * radius;
                double maxRadius = this.MaxRadiusRatio * radius;
                GeometryGroup geometryGroup = new GeometryGroup();

                EllipseGeometry ellipseGeometry1 = new EllipseGeometry();
                ellipseGeometry1.Center = new Point(widthRadius, heightRadius);
                ellipseGeometry1.RadiusX = minRadius;
                ellipseGeometry1.RadiusY = minRadius;
                geometryGroup.Children.Add(ellipseGeometry1);

                EllipseGeometry ellipseGeometry2 = new EllipseGeometry();
                ellipseGeometry2.Center = new Point(widthRadius, heightRadius);
                ellipseGeometry2.RadiusX = maxRadius;
                ellipseGeometry2.RadiusY = maxRadius;
                geometryGroup.Children.Add(ellipseGeometry2);
                return geometryGroup;
            }

            return new PathGeometry();
        }

        private static void OnChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Sector)d).InvalidateVisual();
        }
    }
}
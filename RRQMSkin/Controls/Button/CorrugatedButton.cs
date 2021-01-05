using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace RRQMSkin.Controls
{
    /// <summary>
    /// 水波纹按钮
    /// </summary>
    public class CorrugatedButton : Button
    {
        static CorrugatedButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CorrugatedButton), new FrameworkPropertyMetadata(typeof(CorrugatedButton)));
        }

        /// <summary>
        ///
        /// </summary>
        public CorrugatedButton()
        {
            this.Click += CorrugatedButton_Click;
            this.Loaded += CorrugatedButton_Loaded;
            this.SizeChanged += CorrugatedButton_SizeChanged;
            this.MouseLeave += CorrugatedButton_MouseLeave;
            this.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#05BBFB"));
            this.Content = "按钮";
        }

        private void CorrugatedButton_MouseLeave(object sender, MouseEventArgs e)
        {
            this.ellipse.RenderTransform = new ScaleTransform(0, 0);
        }

        private void CorrugatedButton_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ClipBound();
        }

        private void CorrugatedButton_Loaded(object sender, RoutedEventArgs e)
        {
            ClipBound();
        }

        private void ClipBound()
        {
            if (this.mainGrid == null)
            {
                return;
            }
            PathGeometry pathGeometry = new PathGeometry();

            PathFigure pathFigure = new PathFigure();
            pathFigure.StartPoint = new Point(this.CornerRadius.TopLeft, 0);

            LineSegment topLineSegment = new LineSegment();
            topLineSegment.Point = new Point(this.mainGrid.ActualWidth - this.CornerRadius.TopRight, 0);
            pathFigure.Segments.Add(topLineSegment);

            ArcSegment arcSegmentTopRight = new ArcSegment();
            arcSegmentTopRight.SweepDirection = SweepDirection.Clockwise;
            arcSegmentTopRight.Size = new Size(this.CornerRadius.TopRight, this.CornerRadius.TopRight);
            arcSegmentTopRight.Point = new Point(this.mainGrid.ActualWidth, this.CornerRadius.TopRight);
            pathFigure.Segments.Add(arcSegmentTopRight);

            LineSegment lineSegmentRight = new LineSegment();
            lineSegmentRight.Point = new Point(this.mainGrid.ActualWidth, this.mainGrid.ActualHeight - this.CornerRadius.BottomRight);
            pathFigure.Segments.Add(lineSegmentRight);

            ArcSegment arcSegmentBottomRight = new ArcSegment();
            arcSegmentBottomRight.SweepDirection = SweepDirection.Clockwise;
            arcSegmentBottomRight.Size = new Size(this.CornerRadius.BottomRight, this.CornerRadius.BottomRight);
            arcSegmentBottomRight.Point = new Point(this.mainGrid.ActualWidth - this.CornerRadius.BottomRight, this.mainGrid.ActualHeight);
            pathFigure.Segments.Add(arcSegmentBottomRight);

            LineSegment lineSegmentBottom = new LineSegment();
            lineSegmentBottom.Point = new Point(this.CornerRadius.BottomLeft, this.mainGrid.ActualHeight);
            pathFigure.Segments.Add(lineSegmentBottom);

            ArcSegment arcSegmentBottomLeft = new ArcSegment();
            arcSegmentBottomLeft.SweepDirection = SweepDirection.Clockwise;
            arcSegmentBottomLeft.Size = new Size(this.CornerRadius.BottomLeft, this.CornerRadius.BottomLeft);
            arcSegmentBottomLeft.Point = new Point(0, this.mainGrid.ActualHeight - this.CornerRadius.BottomLeft);
            pathFigure.Segments.Add(arcSegmentBottomLeft);

            LineSegment lineSegmentLeft = new LineSegment();
            lineSegmentLeft.Point = new Point(0, this.CornerRadius.TopLeft);
            pathFigure.Segments.Add(lineSegmentLeft);

            ArcSegment arcSegmentTopLeft = new ArcSegment();
            arcSegmentTopLeft.SweepDirection = SweepDirection.Clockwise;
            arcSegmentTopLeft.Size = new Size(this.CornerRadius.TopLeft, this.CornerRadius.TopLeft);
            arcSegmentTopLeft.Point = new Point(this.CornerRadius.TopLeft, 0);
            pathFigure.Segments.Add(arcSegmentTopLeft);

            pathGeometry.Figures.Add(pathFigure);

            mainGrid.Clip = pathGeometry;
        }

        private void CorrugatedButton_Click(object sender, RoutedEventArgs e)
        {
            if (!this.FlashEnable)
            {
                return;
            }
            ScaleTransform scale = new ScaleTransform();
            this.ellipse.RenderTransform = scale;

            this.ellipse.RenderTransformOrigin = new System.Windows.Point(0.5, 0.5);
            Point point = Mouse.GetPosition(this);
            this.ellipse.SetValue(Canvas.LeftProperty, point.X);
            this.ellipse.SetValue(Canvas.TopProperty, point.Y);

            double radius = new double[] { point.X, point.Y, this.ActualWidth - point.X, this.ActualHeight - point.Y }.Max();

            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.From = 0;
            doubleAnimation.To = radius * 2.5;
            doubleAnimation.Duration = FlashDuration;
            doubleAnimation.FillBehavior = FillBehavior.Stop;
            scale.BeginAnimation(ScaleTransform.ScaleXProperty, doubleAnimation);
            scale.BeginAnimation(ScaleTransform.ScaleYProperty, doubleAnimation);
        }

        private Grid mainGrid;

        /// <summary>
        ///
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            ellipse = (Ellipse)this.Template.FindName("CorrugatedEllipse", this);
            mainGrid = (Grid)this.Template.FindName("mainGrid", this);
        }

        private Ellipse ellipse;

        /// <summary>
        /// 圆角角度
        /// </summary>
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        /// <summary>
        /// 圆角角度属性
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(CorrugatedButton), new PropertyMetadata(new CornerRadius(5), OnChanged));

        /// <summary>
        /// 波浪颜色
        /// </summary>
        public Brush CorrugatedBrush
        {
            get { return (Brush)GetValue(CorrugatedBrushProperty); }
            set { SetValue(CorrugatedBrushProperty, value); }
        }

        /// <summary>
        /// 波浪颜色属性
        /// </summary>
        public static readonly DependencyProperty CorrugatedBrushProperty =
            DependencyProperty.Register("CorrugatedBrush", typeof(Brush), typeof(CorrugatedButton), new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#21AEFA"))));

        /// <summary>
        /// 动画时长
        /// </summary>
        public TimeSpan FlashDuration
        {
            get { return (TimeSpan)GetValue(FlashDurationProperty); }
            set { SetValue(FlashDurationProperty, value); }
        }

        /// <summary>
        /// 动画时长属性
        /// </summary>
        public static readonly DependencyProperty FlashDurationProperty =
            DependencyProperty.Register("FlashDuration", typeof(TimeSpan), typeof(CorrugatedButton), new PropertyMetadata(TimeSpan.FromSeconds(0.3)));

        /// <summary>
        /// 动画启用
        /// </summary>
        public bool FlashEnable
        {
            get { return (bool)GetValue(FlashEnableProperty); }
            set { SetValue(FlashEnableProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FlashEnable.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FlashEnableProperty =
            DependencyProperty.Register("FlashEnable", typeof(bool), typeof(CorrugatedButton), new PropertyMetadata(true));

        private static void OnChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CorrugatedButton corrugatedButton = (CorrugatedButton)d;
            corrugatedButton.ClipBound();
        }
    }
}
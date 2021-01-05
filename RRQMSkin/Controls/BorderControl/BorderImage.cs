using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace RRQMSkin.Controls
{
    public class BorderImage : Image
    {
        public BorderImage()
        {
            this.SizeChanged += BorderImage_SizeChanged; ;
        }

        private void BorderImage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ClipBound();
        }

        private void ClipBound()
        {
            if (this.IsEllipseClip)
            {
                EllipseGeometry ellipseGeometry = new EllipseGeometry();
                ellipseGeometry.RadiusX = this.ActualWidth / 2;
                ellipseGeometry.RadiusY = this.ActualHeight / 2;
                ellipseGeometry.Center = new Point(this.ActualWidth / 2, this.ActualHeight / 2);
                this.Clip = ellipseGeometry;
            }
            else
            {
                PathGeometry pathGeometry = new PathGeometry();

                PathFigure pathFigure = new PathFigure();
                pathFigure.StartPoint = new Point(this.CornerRadius.TopLeft, 0);

                LineSegment topLineSegment = new LineSegment();
                topLineSegment.Point = new Point(this.ActualWidth - this.CornerRadius.TopRight, 0);
                pathFigure.Segments.Add(topLineSegment);

                ArcSegment arcSegmentTopRight = new ArcSegment();
                arcSegmentTopRight.SweepDirection = SweepDirection.Clockwise;
                arcSegmentTopRight.Size = new Size(this.CornerRadius.TopRight, this.CornerRadius.TopRight);
                arcSegmentTopRight.Point = new Point(this.ActualWidth, this.CornerRadius.TopRight);
                pathFigure.Segments.Add(arcSegmentTopRight);

                LineSegment lineSegmentRight = new LineSegment();
                lineSegmentRight.Point = new Point(this.ActualWidth, this.ActualHeight - this.CornerRadius.BottomRight);
                pathFigure.Segments.Add(lineSegmentRight);

                ArcSegment arcSegmentBottomRight = new ArcSegment();
                arcSegmentBottomRight.SweepDirection = SweepDirection.Clockwise;
                arcSegmentBottomRight.Size = new Size(this.CornerRadius.BottomRight, this.CornerRadius.BottomRight);
                arcSegmentBottomRight.Point = new Point(this.ActualWidth - this.CornerRadius.BottomRight, this.ActualHeight);
                pathFigure.Segments.Add(arcSegmentBottomRight);

                LineSegment lineSegmentBottom = new LineSegment();
                lineSegmentBottom.Point = new Point(this.CornerRadius.BottomLeft, this.ActualHeight);
                pathFigure.Segments.Add(lineSegmentBottom);

                ArcSegment arcSegmentBottomLeft = new ArcSegment();
                arcSegmentBottomLeft.SweepDirection = SweepDirection.Clockwise;
                arcSegmentBottomLeft.Size = new Size(this.CornerRadius.BottomLeft, this.CornerRadius.BottomLeft);
                arcSegmentBottomLeft.Point = new Point(0, this.ActualHeight - this.CornerRadius.BottomLeft);
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

                this.Clip = pathGeometry;
            }
        }

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
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(BorderImage), new PropertyMetadata(new CornerRadius(0), OnChanged));

        public bool IsEllipseClip
        {
            get { return (bool)GetValue(IsEllipseClipProperty); }
            set { SetValue(IsEllipseClipProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsEllipseClip.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsEllipseClipProperty =
            DependencyProperty.Register("IsEllipseClip", typeof(bool), typeof(BorderImage), new PropertyMetadata(false, OnChanged));

        private static void OnChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BorderImage image = (BorderImage)d;
            image.ClipBound();
        }
    }
}
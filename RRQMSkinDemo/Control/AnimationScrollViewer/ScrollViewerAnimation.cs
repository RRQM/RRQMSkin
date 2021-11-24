using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace RRQMSkinDemo.Control.AnimationScrollViewer
{
    public class ScrollViewerAnimation : ScrollViewer
    {
        //记录上一次的滚动位置
        private double LastLocation = -1;
        #region OrientationProperty
        /// <summary>
        /// 滚动条排版方式
        /// </summary>
        public Orientation Orientation
        {
            get
            {
                return (Orientation)GetValue(OrientationProperty);
            }
            set
            {
                SetValue(OrientationProperty, value);
            }
        }
        public static readonly DependencyProperty OrientationProperty = DependencyProperty.RegisterAttached(
            "Orientation", typeof(Orientation), typeof(ScrollViewerAnimation), new PropertyMetadata(Orientation.Vertical));
        #endregion

        #region OrientationProperty
        /// <summary>
        /// 滚动条排版方式
        /// </summary>
        public bool GoEnd
        {
            get
            {
                return (bool)GetValue(GoEndProperty);
            }
            set
            {
                SetValue(GoEndProperty, value);
            }
        }
        public static readonly DependencyProperty GoEndProperty = DependencyProperty.RegisterAttached(
            "GoEnd", typeof(bool), typeof(ScrollViewerAnimation), new PropertyMetadata(false));
        #endregion

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            if (Orientation == Orientation.Vertical)
            {
                if (GoEnd && LastLocation == -1)
                {
                    LastLocation = ScrollableHeight;
                }
                double WheelChange = e.Delta;
                //可以更改一次滚动的距离倍数 (WheelChange可能为正负数!)
                double newOffset = LastLocation - (WheelChange * 2);
                //Animation并不会改变真正的VerticalOffset(只是它的依赖属性) 所以将VOffset设置到上一次的滚动位置 (相当于衔接上一个动画)
                ScrollToVerticalOffset(LastLocation);
                //碰到底部和顶部时的处理
                if (newOffset < 0)
                    newOffset = 0;
                if (newOffset > ScrollableHeight)
                    newOffset = ScrollableHeight;

                AnimateScroll(newOffset);
                LastLocation = newOffset;
                //告诉ScrollViewer我们已经完成了滚动
                e.Handled = true;
            }
            else
            {
                if (GoEnd && LastLocation == -1)
                {
                    LastLocation = ScrollableWidth;
                }
                double WheelChange = e.Delta;
                //可以更改一次滚动的距离倍数 (WheelChange可能为正负数!)
                double newOffset = LastLocation - (WheelChange * 2);
                //Animation并不会改变真正的VerticalOffset(只是它的依赖属性) 所以将VOffset设置到上一次的滚动位置 (相当于衔接上一个动画)
                ScrollToHorizontalOffset(LastLocation);
                //碰到底部和顶部时的处理
                if (newOffset < 0)
                    newOffset = 0;
                if (newOffset > ScrollableWidth)
                    newOffset = ScrollableWidth;

                AnimateHorizontalScroll(newOffset);
                LastLocation = newOffset;
                //告诉ScrollViewer我们已经完成了滚动
                e.Handled = true;
            }

        }
        private void AnimateScroll(double ToValue)
        {
            //为了避免重复，先结束掉上一个动画
            BeginAnimation(ScrollViewerBehavior.VerticalOffsetProperty, null);
            DoubleAnimation Animation = new DoubleAnimation();
            Animation.EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseOut };
            Animation.From = VerticalOffset;
            Animation.To = ToValue;
            //动画速度
            Animation.Duration = TimeSpan.FromMilliseconds(800);
            //考虑到性能，可以降低动画帧数
            //Timeline.SetDesiredFrameRate(Animation, 40);
            BeginAnimation(ScrollViewerBehavior.VerticalOffsetProperty, Animation);
        }

        private void AnimateHorizontalScroll(double ToValue)
        {
            //为了避免重复，先结束掉上一个动画
            BeginAnimation(ScrollViewerBehavior.VerticalOffsetProperty, null);
            DoubleAnimation Animation = new DoubleAnimation();
            Animation.EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseOut };
            Animation.From = HorizontalOffset;
            Animation.To = ToValue;
            //动画速度
            Animation.Duration = TimeSpan.FromMilliseconds(800);
            //考虑到性能，可以降低动画帧数
            //Timeline.SetDesiredFrameRate(Animation, 40);
            BeginAnimation(ScrollViewerBehavior.VerticalOffsetProperty, Animation);
        }

        public void GoToTop()
        {
            LastLocation = 0;
            this.ScrollToTop();
        }

        public void GoToEnd()
        {
            LastLocation = -1;
            this.ScrollToEnd();
        }

        public void SetVerticalOffset(double offset)
        {
            LastLocation = offset;
            this.AnimateScroll(offset);
        }

        public void SetHorizontalOffset(double offset)
        {
            LastLocation = offset;
            this.AnimateHorizontalScroll(offset);
        }

        public void GoToHorizontalTop()
        {
            LastLocation = 0;
            this.ScrollToLeftEnd();
        }

        public void GoToHorizontalEnd()
        {
            LastLocation = -1;
            this.ScrollToRightEnd();
        }
    }
}

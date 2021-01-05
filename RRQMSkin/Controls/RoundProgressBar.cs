using System.ComponentModel;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace RRQMSkin.Controls
{
    /// <summary>
    /// 环形进度条
    /// </summary>
    public class RoundProgressBar : RangeBase
    {
        static RoundProgressBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RoundProgressBar), new FrameworkPropertyMetadata(typeof(RoundProgressBar)));
        }

        /// <summary>
        ///
        /// </summary>
        [Browsable(false)]
        public double Angle
        {
            get { return (double)GetValue(AngleProperty); }
            private set { SetValue(AngleProperty, value); }
        }

        /// <summary>
        ///
        /// </summary>
        public static readonly DependencyProperty AngleProperty =
            DependencyProperty.Register("Angle", typeof(double), typeof(RoundProgressBar), new PropertyMetadata(0.0));

        /// <summary>
        /// 进度比
        /// </summary>
        public double Progress
        {
            get { return (double)GetValue(ProgressProperty); }
            private set { SetValue(ProgressProperty, value); }
        }

        /// <summary>
        /// 进度比属性
        /// </summary>
        public static readonly DependencyProperty ProgressProperty =
            DependencyProperty.Register("Progress", typeof(double), typeof(RoundProgressBar), new PropertyMetadata(0.0));

        /// <summary>
        ///
        /// </summary>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        protected override void OnValueChanged(double oldValue, double newValue)
        {
            base.OnValueChanged(oldValue, newValue);
            OnChanged();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="oldMaximum"></param>
        /// <param name="newMaximum"></param>
        protected override void OnMaximumChanged(double oldMaximum, double newMaximum)
        {
            base.OnMaximumChanged(oldMaximum, newMaximum);
            OnChanged();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="oldMinimum"></param>
        /// <param name="newMinimum"></param>
        protected override void OnMinimumChanged(double oldMinimum, double newMinimum)
        {
            base.OnMinimumChanged(oldMinimum, newMinimum);
            OnChanged();
        }

        private void OnChanged()
        {
            this.Progress = this.Value / (this.Maximum - this.Minimum);
            this.Angle = this.Progress * 360;
        }
    }
}
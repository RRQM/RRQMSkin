using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace RRQMSkin.Controls
{
    /// <summary>
    /// 验证滑块
    /// </summary>
    public class ValidationSlider : Slider
    {
        static ValidationSlider()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ValidationSlider), new FrameworkPropertyMetadata(typeof(ValidationSlider)));
        }

        Grid grid;
        System.Windows.Controls.Primitives.Thumb thumb;
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.grid = this.Template.FindName("shadeGrid", this) as Grid;
            this.thumb = this.Template.FindName("Thumb", this) as System.Windows.Controls.Primitives.Thumb;
        }

        protected override void OnValueChanged(double oldValue, double newValue)
        {
            base.OnValueChanged(oldValue, newValue);
            if (this.grid != null)
            {
                this.grid.HorizontalAlignment = HorizontalAlignment.Right;
                double thumbWidth = (this.thumb == null ? 0 : this.thumb.ActualWidth);
                this.grid.Width = (1 - (this.Value / this.Maximum)) * (this.ActualWidth - thumbWidth) + thumbWidth;
            }

            if (newValue == this.Maximum)
            {
                this.Verification = true;
                if (this.thumb != null)
                {
                    this.thumb.IsEnabled = false;
                }
                this.OnVerify?.Invoke(this);
                if (this.OnVerifyCommand != null && this.OnVerifyCommand.CanExecute(null))
                {
                    this.OnVerifyCommand.Execute(null);
                }
            }
        }

        public event Action<object> OnVerify;

        public ICommand OnVerifyCommand
        {
            get => (ICommand)this.GetValue(OnVerifyCommandProperty);
            set => this.SetValue(OnVerifyCommandProperty, value);
        }

        // Using a DependencyProperty as the backing store for OnVerifyCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OnVerifyCommandProperty =
            DependencyProperty.Register("OnVerifyCommand", typeof(ICommand), typeof(ValidationSlider), new PropertyMetadata(null));

        public object Content
        {
            get => (object)this.GetValue(ContentProperty);
            set => this.SetValue(ContentProperty, value);
        }

        // Using a DependencyProperty as the backing store for Content.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(object), typeof(ValidationSlider), new PropertyMetadata(null));




        public object ThumbContent
        {
            get => (object)this.GetValue(ThumbContentProperty);
            set => this.SetValue(ThumbContentProperty, value);
        }

        // Using a DependencyProperty as the backing store for ThumbContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ThumbContentProperty =
            DependencyProperty.Register("ThumbContent", typeof(object), typeof(ValidationSlider), new PropertyMetadata(null));





        public Brush ShadeBackground
        {
            get => (Brush)this.GetValue(ShadeBackgroundProperty);
            set => this.SetValue(ShadeBackgroundProperty, value);
        }

        // Using a DependencyProperty as the backing store for ShadeBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShadeBackgroundProperty =
            DependencyProperty.Register("ShadeBackground", typeof(Brush), typeof(ValidationSlider), new PropertyMetadata(null));




        public bool Verification
        {
            get => (bool)this.GetValue(VerificationProperty);
            private set => this.SetValue(VerificationProperty, value);
        }

        // Using a DependencyProperty as the backing store for Verification.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VerificationProperty =
            DependencyProperty.Register("Verification", typeof(bool), typeof(ValidationSlider), new PropertyMetadata(false));


    }
}
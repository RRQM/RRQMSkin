using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace RRQMSkin.Controls
{
    [TemplatePart(Name = "PART_ClearButtonHost", Type = typeof(ButtonBase))]
    public class ClearSearchBox : TextBox
    {
        private ButtonBase clearButtonHost;
        static ClearSearchBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ClearSearchBox), new FrameworkPropertyMetadata(typeof(ClearSearchBox)));
        }

        #region 输入框内是否有数据
        [Browsable(false)]
        public bool IsHasText
        {
            get => (bool)this.GetValue(IsHasTextProperty);
            private set => this.SetValue(IsHasTextPropertyKey, value);
        }
        private static readonly DependencyPropertyKey IsHasTextPropertyKey =
            DependencyProperty.RegisterReadOnly(
                "IsHasText",
                typeof(Boolean),
                typeof(ClearSearchBox),
                new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender));
        public static readonly DependencyProperty IsHasTextProperty =
            IsHasTextPropertyKey.DependencyProperty;


        #endregion

        #region  继承事件
        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);
            this.IsHasText = !String.IsNullOrEmpty(this.Text);
        }

        //模板化控件在加载ControlTemplate后会调用OnApplyTemplate
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (this.clearButtonHost != null)
            {
                this.clearButtonHost.Click -= this.HandleClearButtonClick;
            }
            this.clearButtonHost = this.GetTemplateChild("PART_ClearButtonHost") as ButtonBase;
            if (this.clearButtonHost != null)
            {
                this.clearButtonHost.Click += this.HandleClearButtonClick;
            }

        }

        #endregion

        #region 方法
        //清除事件
        private void HandleClearButtonClick(object sender, RoutedEventArgs e)
        {
            this.Reset();
        }

        public void Reset()
        {
            this.Text = string.Empty;
            clearbackground?.Invoke(this, null);
        }
        #endregion

        #region 水印
        [Category("Extend Properties")]
        public String InputHint
        {
            get => (String)this.GetValue(InputHintProperty);
            set => this.SetValue(InputHintProperty, value);
        }

        public static readonly DependencyProperty InputHintProperty =
          DependencyProperty.Register("InputHint", typeof(String), typeof(ClearSearchBox), new PropertyMetadata(String.Empty));

        [Category("Extend Properties")]
        public Brush InputForeground
        {
            get => (Brush)this.GetValue(InputForegroundProperty);
            set => this.SetValue(InputForegroundProperty, value);
        }
        public static readonly DependencyProperty InputForegroundProperty =
          DependencyProperty.Register("InputForeground", typeof(Brush), typeof(ClearSearchBox), new PropertyMetadata(Brushes.Black));

        [Category("Extend Properties")]
        public HorizontalAlignment InputHorizontalContentAlignment
        {
            get => (HorizontalAlignment)this.GetValue(InputHorizontalContentAlignmentProperty);
            set => this.SetValue(InputHorizontalContentAlignmentProperty, value);
        }
        public static readonly DependencyProperty InputHorizontalContentAlignmentProperty =
          DependencyProperty.Register("InputHorizontalContentAlignment", typeof(HorizontalAlignment), typeof(ClearSearchBox), new PropertyMetadata(HorizontalAlignment.Left));

        [Category("Extend Properties")]
        public VerticalAlignment InputVerticalContentAlignment
        {
            get => (VerticalAlignment)this.GetValue(InputVerticalContentAlignmentProperty);
            set => this.SetValue(InputVerticalContentAlignmentProperty, value);
        }


        public static readonly DependencyProperty InputVerticalContentAlignmentProperty =
          DependencyProperty.Register("InputVerticalContentAlignment", typeof(VerticalAlignment), typeof(ClearSearchBox), new PropertyMetadata(VerticalAlignment.Center));

        [Category("Extend Properties")]
        public bool isClearBtn
        {
            get => (bool)this.GetValue(isClearBtnProperty);
            set => this.SetValue(isClearBtnProperty, value);
        }


        public static readonly DependencyProperty isClearBtnProperty =
          DependencyProperty.Register("isClearBtn", typeof(bool), typeof(ClearSearchBox), new PropertyMetadata(true));

        public event MouseButtonEventHandler clearbackground;
        #endregion

        #region 圆角属性
        [Category("Extend Properties")]
        public CornerRadius CornerRadius
        {
            get => (CornerRadius)this.GetValue(CornerRadiusProperty);
            set => this.SetValue(CornerRadiusProperty, value);
        }
        public static readonly DependencyProperty CornerRadiusProperty =
          DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(ClearSearchBox), new PropertyMetadata(new CornerRadius(0)));

        [Category("Extend Properties")]
        public HorizontalAlignment TextHorizontalContentAlignment
        {
            get => (HorizontalAlignment)this.GetValue(TextHorizontalContentAlignmentProperty);
            set => this.SetValue(TextHorizontalContentAlignmentProperty, value);
        }
        public static readonly DependencyProperty TextHorizontalContentAlignmentProperty =
          DependencyProperty.Register("TextHorizontalContentAlignment", typeof(HorizontalAlignment), typeof(ClearSearchBox), new PropertyMetadata(HorizontalAlignment.Left));

        [Category("Extend Properties")]
        public VerticalAlignment TextVerticalContentAlignment
        {
            get => (VerticalAlignment)this.GetValue(TextVerticalContentAlignmentProperty);
            set => this.SetValue(TextVerticalContentAlignmentProperty, value);
        }
        public static readonly DependencyProperty TextVerticalContentAlignmentProperty =
          DependencyProperty.Register("TextVerticalContentAlignment", typeof(VerticalAlignment), typeof(ClearSearchBox), new PropertyMetadata(VerticalAlignment.Center));
        #endregion

        [Category("Extend Properties")]
        public Brush BackForeground
        {
            get => (Brush)this.GetValue(BackForegroundProperty);
            set => this.SetValue(BackForegroundProperty, value);
        }
        public static readonly DependencyProperty BackForegroundProperty =
          DependencyProperty.Register("BackForeground", typeof(Brush), typeof(ClearSearchBox), new PropertyMetadata(Brushes.Black));
    }
}

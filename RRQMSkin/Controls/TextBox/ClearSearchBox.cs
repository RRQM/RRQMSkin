using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            get { return (bool)GetValue(IsHasTextProperty); }
            private set { SetValue(IsHasTextPropertyKey, value); }
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
            IsHasText = !String.IsNullOrEmpty(Text);
        }

        //模板化控件在加载ControlTemplate后会调用OnApplyTemplate
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (clearButtonHost != null)
            {
                clearButtonHost.Click -= HandleClearButtonClick;
            }
            clearButtonHost = GetTemplateChild("PART_ClearButtonHost") as ButtonBase;
            if (clearButtonHost != null)
            {
                clearButtonHost.Click += HandleClearButtonClick;
            }

        }

        #endregion

        #region 方法
        //清除事件
        private void HandleClearButtonClick(object sender, RoutedEventArgs e)
        {
            Reset();
        }

        public void Reset()
        {
            Text = string.Empty;
            clearbackground?.Invoke(this, null);
        }
        #endregion

        #region 水印
        [Category("Extend Properties")]
        public String InputHint
        {
            get { return (String)GetValue(InputHintProperty); }
            set { SetValue(InputHintProperty, value); }
        }

        public static readonly DependencyProperty InputHintProperty =
          DependencyProperty.Register("InputHint", typeof(String), typeof(ClearSearchBox), new PropertyMetadata(String.Empty));

        [Category("Extend Properties")]
        public Brush InputForeground
        {
            get { return (Brush)GetValue(InputForegroundProperty); }
            set { SetValue(InputForegroundProperty, value); }
        }
        public static readonly DependencyProperty InputForegroundProperty =
          DependencyProperty.Register("InputForeground", typeof(Brush), typeof(ClearSearchBox), new PropertyMetadata(Brushes.Black));

        [Category("Extend Properties")]
        public HorizontalAlignment InputHorizontalContentAlignment
        {
            get { return (HorizontalAlignment)GetValue(InputHorizontalContentAlignmentProperty); }
            set { SetValue(InputHorizontalContentAlignmentProperty, value); }
        }
        public static readonly DependencyProperty InputHorizontalContentAlignmentProperty =
          DependencyProperty.Register("InputHorizontalContentAlignment", typeof(HorizontalAlignment), typeof(ClearSearchBox), new PropertyMetadata(HorizontalAlignment.Left));

        [Category("Extend Properties")]
        public VerticalAlignment InputVerticalContentAlignment
        {
            get { return (VerticalAlignment)GetValue(InputVerticalContentAlignmentProperty); }
            set { SetValue(InputVerticalContentAlignmentProperty, value); }
        }


        public static readonly DependencyProperty InputVerticalContentAlignmentProperty =
          DependencyProperty.Register("InputVerticalContentAlignment", typeof(VerticalAlignment), typeof(ClearSearchBox), new PropertyMetadata(VerticalAlignment.Center));

        [Category("Extend Properties")]
        public bool isClearBtn
        {
            get { return (bool)GetValue(isClearBtnProperty); }
            set { SetValue(isClearBtnProperty, value); }
        }


        public static readonly DependencyProperty isClearBtnProperty =
          DependencyProperty.Register("isClearBtn", typeof(bool), typeof(ClearSearchBox), new PropertyMetadata(true));

        public event MouseButtonEventHandler clearbackground;
        #endregion

        #region 圆角属性
        [Category("Extend Properties")]
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        public static readonly DependencyProperty CornerRadiusProperty =
          DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(ClearSearchBox), new PropertyMetadata(new CornerRadius(0)));

        [Category("Extend Properties")]
        public HorizontalAlignment TextHorizontalContentAlignment
        {
            get { return (HorizontalAlignment)GetValue(TextHorizontalContentAlignmentProperty); }
            set { SetValue(TextHorizontalContentAlignmentProperty, value); }
        }
        public static readonly DependencyProperty TextHorizontalContentAlignmentProperty =
          DependencyProperty.Register("TextHorizontalContentAlignment", typeof(HorizontalAlignment), typeof(ClearSearchBox), new PropertyMetadata(HorizontalAlignment.Left));

        [Category("Extend Properties")]
        public VerticalAlignment TextVerticalContentAlignment
        {
            get { return (VerticalAlignment)GetValue(TextVerticalContentAlignmentProperty); }
            set { SetValue(TextVerticalContentAlignmentProperty, value); }
        }
        public static readonly DependencyProperty TextVerticalContentAlignmentProperty =
          DependencyProperty.Register("TextVerticalContentAlignment", typeof(VerticalAlignment), typeof(ClearSearchBox), new PropertyMetadata(VerticalAlignment.Center));
        #endregion

        [Category("Extend Properties")]
        public Brush BackForeground
        {
            get { return (Brush)GetValue(BackForegroundProperty); }
            set { SetValue(BackForegroundProperty, value); }
        }
        public static readonly DependencyProperty BackForegroundProperty =
          DependencyProperty.Register("BackForeground", typeof(Brush), typeof(ClearSearchBox), new PropertyMetadata(Brushes.Black));
    }
}

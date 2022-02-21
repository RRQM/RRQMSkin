using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace RRQMSkin.Controls
{
    public class LabelTag : Label
    {
        private ButtonBase closeButtonHost;
        public event RoutedEventHandler CloseClick;
        static LabelTag()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LabelTag), new FrameworkPropertyMetadata(typeof(LabelTag)));
        }
        #region  继承事件

        //模板化控件在加载ControlTemplate后会调用OnApplyTemplate
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (this.closeButtonHost != null)
            {
                this.closeButtonHost.Click -= this.HandleClearButtonClick;
            }
            this.closeButtonHost = this.GetTemplateChild("PART_CloseButtonHost") as ButtonBase;
            if (this.closeButtonHost != null)
            {
                this.closeButtonHost.Click += this.HandleClearButtonClick;
            }

        }

        #endregion

        private void HandleClearButtonClick(object sender, RoutedEventArgs e)
        {
            CloseClick?.Invoke(this, null);
        }


        [Category("Extend Properties")]
        public String Text
        {
            get => (String)this.GetValue(TextProperty);
            set => this.SetValue(TextProperty, value);
        }

        public static readonly DependencyProperty TextProperty =
          DependencyProperty.Register("Text", typeof(String), typeof(LabelTag), new PropertyMetadata(String.Empty));

        [Category("Extend Properties")]
        public Brush TxtForeground
        {
            get => (Brush)this.GetValue(TxtForegroundProperty);
            set => this.SetValue(TxtForegroundProperty, value);
        }
        public static readonly DependencyProperty TxtForegroundProperty =
          DependencyProperty.Register("TxtForeground", typeof(Brush), typeof(LabelTag), new PropertyMetadata(Brushes.Black));

        [Category("Extend Properties")]
        public ulong TagID
        {
            get => (ulong)this.GetValue(TagIDProperty);
            set => this.SetValue(TagIDProperty, value);
        }
        public static readonly DependencyProperty TagIDProperty =
          DependencyProperty.Register("TagID", typeof(ulong), typeof(LabelTag), new PropertyMetadata(new ulong()));

        [Category("Extend Properties")]
        public Visibility CloseBtnVisibility
        {
            get => (Visibility)this.GetValue(CloseBtnVisibilityProperty);
            set => this.SetValue(CloseBtnVisibilityProperty, value);
        }
        public static readonly DependencyProperty CloseBtnVisibilityProperty =
          DependencyProperty.Register("CloseBtnVisibility", typeof(Visibility), typeof(LabelTag), new PropertyMetadata(Visibility.Visible));
    }
}

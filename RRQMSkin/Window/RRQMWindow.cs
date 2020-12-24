using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shell;

namespace RRQMSkin
{
    public class RRQMWindow : Window
    {
        static RRQMWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RRQMWindow), new FrameworkPropertyMetadata(typeof(RRQMWindow)));
        }

        public RRQMWindow()
        {
            this.Background = Brushes.White;
            this.ResizeMode = RRQMResizeMode.CanResize;
            base.WindowStyle = System.Windows.WindowStyle.None;
            this.Loaded += Windows_Loaded;
            base.Icon = new BitmapImage(new Uri("pack://application:,,,/RRQMSkin;component/Icons/RRQM.ico", UriKind.RelativeOrAbsolute));

            Image minImage = new Image();
            minImage.Width = minImage.Height = 15;
            minImage.Source = new BitmapImage(new Uri("pack://application:,,,/RRQMSkin;component/icons/最小化.png", UriKind.RelativeOrAbsolute));
            this.MinButtonContent = minImage;

            Image maxImage = new Image();
            maxImage.Width = maxImage.Height = 15;
            maxImage.Source = new BitmapImage(new Uri("pack://application:,,,/RRQMSkin;component/icons/正常.png", UriKind.RelativeOrAbsolute));
            this.MaxButtonContent = maxImage;

            Image norImage = new Image();
            norImage.Width = norImage.Height = 15;
            norImage.Source = new BitmapImage(new Uri("pack://application:,,,/RRQMSkin;component/icons/最大化.png", UriKind.RelativeOrAbsolute));
            this.NormalButtonContent = norImage;

            Image cloImage = new Image();
            cloImage.Width = cloImage.Height = 15;
            cloImage.Source = new BitmapImage(new Uri("pack://application:,,,/RRQMSkin;component/icons/关闭.png", UriKind.RelativeOrAbsolute));
            this.CloseButtonContent = cloImage;

            MinWindowCommand = new Command(() => { this.WindowState = WindowState.Minimized; });
            MaxOrNormalWindowCommand = new Command(() => { WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized; });
            CloseWindowCommand = new Command(() => { this.Close(); });

            WindowChrome windowChrome = new WindowChrome();
            windowChrome.CaptionHeight = 0;
            windowChrome.ResizeBorderThickness = new Thickness(5);
            this.SetValue(WindowChrome.WindowChromeProperty, windowChrome);
        }

        private void Windows_Loaded(object sender, RoutedEventArgs e)
        {
            OnStateChanged(e);
        }

        protected bool mRestoreForDragMove;
        protected Grid titleGrid;
        protected Border mainBorder;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            titleGrid = (Grid)this.Template.FindName("title", this);
            mainBorder = (Border)this.Template.FindName("mainBorder", this);
            this.titleGrid.MouseLeftButtonDown += titleGrid_MouseLeftButtonDown;
            this.titleGrid.MouseMove += titleGrid_MouseMove;
            this.titleGrid.MouseLeftButtonUp += (s, e) => { mRestoreForDragMove = false; };
        }

        private void titleGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                mRestoreForDragMove = false;
                if (ResizeMode != RRQMResizeMode.CanResize) return;
                WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
            }
            else
            {
                if (e.ButtonState == MouseButtonState.Pressed)
                {
                    mRestoreForDragMove = WindowState == WindowState.Maximized;
                    DragMove();
                }
            }
        }

        private void titleGrid_MouseMove(object sender, MouseEventArgs e)
        {
            if (mRestoreForDragMove)
            {
                mRestoreForDragMove = false;
                WindowState = WindowState.Normal;
                var point = e.MouseDevice.GetPosition(this);
                Left = point.X - this.titleGrid.ActualWidth * point.X / SystemParameters.WorkArea.Width - this.mainBorder.Margin.Left;
                Top = point.Y - this.titleGrid.ActualHeight * point.Y / SystemParameters.WorkArea.Height - this.mainBorder.Margin.Top;
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    DragMove();
                }
            }
        }

        /// <summary>
        /// 最小化按钮
        /// </summary>
        public object MinButtonContent
        {
            get { return (object)GetValue(MinButtonContentProperty); }
            set { SetValue(MinButtonContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MinButtonContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinButtonContentProperty =
            DependencyProperty.Register("MinButtonContent", typeof(object), typeof(RRQMWindow), new PropertyMetadata("最小"));

        /// <summary>
        /// 最大化按钮
        /// </summary>
        public object MaxButtonContent
        {
            get { return (object)GetValue(MaxButtonContentProperty); }
            set { SetValue(MaxButtonContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MaxOrNormalButtonContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxButtonContentProperty =
            DependencyProperty.Register("MaxButtonContent", typeof(object), typeof(RRQMWindow), new PropertyMetadata("最大"));

        /// <summary>
        /// 正常化按钮
        /// </summary>
        public object NormalButtonContent
        {
            get { return (object)GetValue(NormalButtonContentProperty); }
            set { SetValue(NormalButtonContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NormalButtonContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NormalButtonContentProperty =
            DependencyProperty.Register("NormalButtonContent", typeof(object), typeof(RRQMWindow), new PropertyMetadata("正常"));

        public object MaxOrNormalContent
        {
            get { return (object)GetValue(MaxOrNormalContentProperty); }
            private set { SetValue(MaxOrNormalContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MaxOrNormalContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxOrNormalContentProperty =
            DependencyProperty.Register("MaxOrNormalContent", typeof(object), typeof(RRQMWindow), new PropertyMetadata(null));

        /// <summary>
        /// 关闭按钮
        /// </summary>
        public object CloseButtonContent
        {
            get { return (object)GetValue(CloseButtonContentProperty); }
            set { SetValue(CloseButtonContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CloseButtonContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CloseButtonContentProperty =
            DependencyProperty.Register("CloseButtonContent", typeof(object), typeof(RRQMWindow), new PropertyMetadata("关闭"));

        /// <summary>
        /// 窗口头信息
        /// </summary>
        public object TitleContent
        {
            get { return (object)GetValue(TitleContentProperty); }
            set { SetValue(TitleContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TitleContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleContentProperty =
            DependencyProperty.Register("TitleContent", typeof(object), typeof(RRQMWindow), new PropertyMetadata("若汝棋茗"));

        public new RRQMWindowStyle WindowStyle
        {
            get { return (RRQMWindowStyle)GetValue(WindowStyleProperty); }
            set { SetValue(WindowStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WindowStyle.  This enables animation, styling, binding, etc...
        public static new readonly DependencyProperty WindowStyleProperty =
            DependencyProperty.Register("WindowStyle", typeof(RRQMWindowStyle), typeof(RRQMWindow), new PropertyMetadata(RRQMWindowStyle.SingleBorderWindow));

        public bool ContentFill
        {
            get { return (bool)GetValue(ContentFillProperty); }
            set { SetValue(ContentFillProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ContentFill.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContentFillProperty =
            DependencyProperty.Register("ContentFill", typeof(bool), typeof(RRQMWindow), new PropertyMetadata(false));

        public new RRQMResizeMode ResizeMode
        {
            get { return (RRQMResizeMode)GetValue(ResizeModeProperty); }
            set { SetValue(ResizeModeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ResizeMode.  This enables animation, styling, binding, etc...
        public static new readonly DependencyProperty ResizeModeProperty =
            DependencyProperty.Register("ResizeMode", typeof(RRQMResizeMode), typeof(RRQMWindow), new PropertyMetadata(RRQMResizeMode.CanResize, OnResizeChanged));

        private void SetBaseResizeMode(System.Windows.ResizeMode resizeMode)
        {
            base.ResizeMode = resizeMode;
        }

        private static void OnResizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RRQMWindow window = (RRQMWindow)d;
            switch (window.ResizeMode)
            {
                case RRQMResizeMode.NoResize:
                    window.SetBaseResizeMode(System.Windows.ResizeMode.NoResize);
                    break;

                case RRQMResizeMode.CanResize:
                    window.SetBaseResizeMode(System.Windows.ResizeMode.CanResize);
                    break;
            }
        }

        #region Command

        public Command MinWindowCommand { get; set; }
        public Command MaxOrNormalWindowCommand { get; set; }
        public Command CloseWindowCommand { get; set; }

        #endregion Command

        protected override void OnStateChanged(EventArgs e)
        {
            switch (this.WindowState)
            {
                case WindowState.Maximized:
                    this.MaxWidth = SystemParameters.WorkArea.Width + 16;
                    this.MaxHeight = SystemParameters.WorkArea.Height + 16;
                    this.BorderThickness = new Thickness(5); //最大化后需要调整

                    this.MaxOrNormalContent = this.MaxButtonContent;
                    break;

                case WindowState.Normal:
                    this.BorderThickness = new Thickness(0);
                    this.MaxWidth = SystemParameters.WorkArea.Width + 16;
                    this.MaxHeight = SystemParameters.WorkArea.Height + 16;
                    this.MaxOrNormalContent = this.NormalButtonContent;
                    break;
            }
        }
    }
}
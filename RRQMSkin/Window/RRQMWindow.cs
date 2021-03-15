//------------------------------------------------------------------------------
//  此代码版权归作者本人若汝棋茗所有
//  源代码使用协议遵循本仓库的开源协议，若本仓库没有设置，则按MIT开源协议授权
//  CSDN博客：https://blog.csdn.net/qq_40374647
//  哔哩哔哩视频：https://space.bilibili.com/94253567
//  源代码仓库：https://gitee.com/RRQM_Home
//  交流QQ群：234762506
//  感谢您的下载和使用
//------------------------------------------------------------------------------
//------------------------------------------------------------------------------
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shell;
using RRQMMVVM;

namespace RRQMSkin.Windows
{
    public class RRQMWindow :System.Windows.Window
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

            MinWindowCommand = new ExecuteCommand(() => { this.WindowState = WindowState.Minimized; });
            MaxOrNormalWindowCommand = new ExecuteCommand(() => { WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized; });
            CloseWindowCommand = new ExecuteCommand(() => { this.Close(); });

            WindowChrome windowChrome = new WindowChrome();
            windowChrome.CaptionHeight = 0;
            windowChrome.ResizeBorderThickness = new Thickness(5);
            this.SetValue(WindowChrome.WindowChromeProperty, windowChrome);
        }

        private void Windows_Loaded(object sender, RoutedEventArgs e)
        {
            OnStateChanged(e);
        }

        private bool mRestoreForDragMove;
        private Grid titleGrid;
        private Border mainBorder;

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

        public ExecuteCommand MinWindowCommand { get; set; }
        public ExecuteCommand MaxOrNormalWindowCommand { get; set; }
        public ExecuteCommand CloseWindowCommand { get; set; }

        #endregion Command

        protected override void OnStateChanged(EventArgs e)
        {
            switch (this.WindowState)
            {
                case WindowState.Maximized:
                    this.MaxWidth = SystemParameters.WorkArea.Width + 16;
                    this.MaxHeight = SystemParameters.WorkArea.Height + 16;
                    this.BorderThickness = new Thickness(5); //最大化后需要调整

                    break;

                case WindowState.Normal:
                    this.BorderThickness = new Thickness(0);
                    this.MaxWidth = SystemParameters.WorkArea.Width + 16;
                    this.MaxHeight = SystemParameters.WorkArea.Height + 16;
                    break;
            }
        }
    }
}
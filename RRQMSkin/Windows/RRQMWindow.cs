//------------------------------------------------------------------------------
//  此代码版权归作者本人若汝棋茗所有
//  源代码使用协议遵循本仓库的开源协议及附加协议，若本仓库没有设置，则按MIT开源协议授权
//  CSDN博客：https://blog.csdn.net/qq_40374647
//  哔哩哔哩视频：https://space.bilibili.com/94253567
//  源代码仓库：https://gitee.com/RRQM_Home
//  交流QQ群：234762506
//  感谢您的下载和使用
//------------------------------------------------------------------------------
//------------------------------------------------------------------------------
using RRQMSkin.MVVM;
using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RRQMSkin.Windows
{
    public enum RRQMResizeMode
    {
        NoResize,
        CanResize,
    }

    public enum RRQMWindowStyle
    {
        SingleBorderWindow,
        ToolWindow,
    }

    /// <summary>
    /// 自定义窗口
    /// </summary>
    public class RRQMWindow : Window
    {
        /// <summary>
        /// 圆角属性
        /// </summary>
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)this.GetValue(CornerRadiusProperty); }
            set { this.SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(RRQMWindow), new PropertyMetadata(new CornerRadius(0), OnCornerRadiusChanged));



        // Using a DependencyProperty as the backing store for ResizeMode.  This enables animation, styling, binding, etc...
        public static new readonly DependencyProperty ResizeModeProperty =
            DependencyProperty.Register("ResizeMode", typeof(RRQMResizeMode), typeof(RRQMWindow), new PropertyMetadata(RRQMResizeMode.CanResize));


        // Using a DependencyProperty as the backing store for TitleContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleContentProperty =
            DependencyProperty.Register("TitleContent", typeof(object), typeof(RRQMWindow), new PropertyMetadata("若汝棋茗"));


        private HwndSource _hwndSource;
        private Border mainBorder;
        private Border headerBorder;
        private Grid titleGrid;
        private Grid windowGrid;

        static RRQMWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RRQMWindow), new FrameworkPropertyMetadata(typeof(RRQMWindow)));
        }

        private static void OnCornerRadiusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RRQMWindow window=(RRQMWindow)d;
            if (window.headerBorder!=null)
            {
                window.headerBorder.CornerRadius = new CornerRadius(window.CornerRadius.TopLeft, window.CornerRadius.TopRight, 0, 0);
            }
        }

        /// <summary>
        ///构造函数
        /// </summary>
        public RRQMWindow()
        {
            base.ResizeMode = System.Windows.ResizeMode.NoResize;
            base.WindowStyle = WindowStyle.None;
            this.Background = Brushes.White;
            this.Icon = new BitmapImage(new Uri("pack://application:,,,/RRQMSkin;component/Icons/RRQM.ico", UriKind.RelativeOrAbsolute));

            this.MinWindowCommand = new ExecuteCommand(() => { this.WindowState = WindowState.Minimized; });
            this.MaxOrNormalWindowCommand = new ExecuteCommand(() =>
            {
                this.WindowState = this.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
            });
            this.CloseWindowCommand = new ExecuteCommand(() => { this.Close(); });
            this.Loaded += this.RRQMWindow_Loaded;
        }

        private void RRQMWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this._hwndSource = (HwndSource)PresentationSource.FromVisual(this);
            this.OnStateChanged(null);
        }

        internal enum ResizeDirection
        {
            Left = 1,
            Right = 2,
            Top = 3,
            TopLeft = 4,
            TopRight = 5,
            Bottom = 6,
            BottomLeft = 7,
            BottomRight = 8,
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        public ICommand CloseWindowCommand { get; set; }

        /// <summary>
        /// 最大化切换
        /// </summary>
        public ICommand MaxOrNormalWindowCommand { get; set; }

        /// <summary>
        /// 最小化
        /// </summary>
        public ICommand MinWindowCommand { get; set; }

        /// <summary>
        /// ResizeMode
        /// </summary>
        public new RRQMResizeMode ResizeMode
        {
            get => (RRQMResizeMode)this.GetValue(ResizeModeProperty);
            set => this.SetValue(ResizeModeProperty, value);
        }

        /// <summary>
        /// 窗口头信息
        /// </summary>
        public object TitleContent
        {
            get => (object)this.GetValue(TitleContentProperty);
            set => this.SetValue(TitleContentProperty, value);
        }

        /// <summary>
        /// OnApplyTemplate
        /// </summary>
        public override void OnApplyTemplate()
        {
            this.mainBorder = (Border)this.Template.FindName("mainBorder", this);
            

            this.titleGrid = (Grid)this.Template.FindName("title", this);
            this.windowGrid = (Grid)this.Template.FindName("windowGrid", this);
            this.headerBorder = (Border)this.Template.FindName("header", this);
            if (this.CornerRadius != null)
            {
                headerBorder.CornerRadius = new CornerRadius(CornerRadius.TopLeft, CornerRadius.TopRight, 0, 0);
            }

            this.titleGrid.MouseLeftButtonDown += this.titleGrid_MouseLeftButtonDown;
            this.titleGrid.MouseMove += this.titleGrid_MouseMove;

            RowDefinition row0 = new RowDefinition();
            row0.Height = new GridLength(8);
            RowDefinition row1 = new RowDefinition();
            RowDefinition row2 = new RowDefinition();
            row2.Height = new GridLength(8);

            ColumnDefinition column0 = new ColumnDefinition();
            column0.Width = new GridLength(8);
            ColumnDefinition column1 = new ColumnDefinition();
            ColumnDefinition column2 = new ColumnDefinition();
            column2.Width = new GridLength(8);

            this.windowGrid.RowDefinitions.Add(row0);
            this.windowGrid.RowDefinitions.Add(row1);
            this.windowGrid.RowDefinitions.Add(row2);
            this.windowGrid.ColumnDefinitions.Add(column0);
            this.windowGrid.ColumnDefinitions.Add(column1);
            this.windowGrid.ColumnDefinitions.Add(column2);

            this.mainBorder.SetValue(Grid.RowProperty, 0);
            this.mainBorder.SetValue(Grid.ColumnProperty, 0);
            this.mainBorder.SetValue(Grid.RowSpanProperty, 3);
            this.mainBorder.SetValue(Grid.ColumnSpanProperty, 3);

            this.AddResizeRectangle();

            base.OnApplyTemplate();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreviewMouseMove(MouseEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed)
                this.Cursor = Cursors.Arrow;
            base.OnPreviewMouseMove(e);
        }

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int WM_LBUTTONUP = 0x0202;


        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStateChanged(EventArgs e)
        {
            if (double.IsInfinity(this.MaxWidth))
            {
                this.MaxWidth = SystemParameters.WorkArea.Width;
            }
            if (double.IsInfinity(this.MaxHeight))
            {
                this.MaxHeight = SystemParameters.WorkArea.Height;
            }
            switch (this.WindowState)
            {
                case WindowState.Minimized:
                case WindowState.Maximized:
                    if (lastState== WindowState.Normal)
                    {
                        this.corner = this.CornerRadius;
                    }
                    this.CornerRadius = default;
                    this.mainBorder.Padding = new Thickness(0.0);
                    break;

                case WindowState.Normal:
                    this.mainBorder.Padding = new Thickness(10.0);
                    if (e!=null)
                    {
                        this.CornerRadius = this.corner;
                    }
                    break;
            }
            lastState = this.WindowState;
            base.OnStateChanged(e);
        }

        private WindowState lastState;
        private CornerRadius corner;

        private void AddResizeRectangle()
        {
            Brush brush = Brushes.Transparent;
            Rectangle rect1 = new Rectangle() { Name = "TopLeft", Fill = brush };
            rect1.SetValue(Grid.RowProperty, 0);
            rect1.SetValue(Grid.ColumnProperty, 0);

            Rectangle rect2 = new Rectangle() { Name = "Top", Fill = brush };
            rect2.SetValue(Grid.RowProperty, 0);
            rect2.SetValue(Grid.ColumnProperty, 1);

            Rectangle rect3 = new Rectangle() { Name = "TopRight", Fill = brush };
            rect3.SetValue(Grid.RowProperty, 0);
            rect3.SetValue(Grid.ColumnProperty, 2);

            Rectangle rect4 = new Rectangle() { Name = "Right", Fill = brush };
            rect4.SetValue(Grid.RowProperty, 1);
            rect4.SetValue(Grid.ColumnProperty, 2);

            Rectangle rect5 = new Rectangle() { Name = "BottomRight", Fill = brush };
            rect5.SetValue(Grid.RowProperty, 2);
            rect5.SetValue(Grid.ColumnProperty, 2);

            Rectangle rect6 = new Rectangle() { Name = "Bottom", Fill = brush };
            rect6.SetValue(Grid.RowProperty, 2);
            rect6.SetValue(Grid.ColumnProperty, 1);

            Rectangle rect7 = new Rectangle() { Name = "BottomLeft", Fill = brush };
            rect7.SetValue(Grid.RowProperty, 2);
            rect7.SetValue(Grid.ColumnProperty, 0);

            Rectangle rect8 = new Rectangle() { Name = "Left", Fill = brush };
            rect8.SetValue(Grid.RowProperty, 1);
            rect8.SetValue(Grid.ColumnProperty, 0);

            this.windowGrid.Children.Add(rect1);
            this.windowGrid.Children.Add(rect2);
            this.windowGrid.Children.Add(rect3);
            this.windowGrid.Children.Add(rect4);
            this.windowGrid.Children.Add(rect5);
            this.windowGrid.Children.Add(rect6);
            this.windowGrid.Children.Add(rect7);
            this.windowGrid.Children.Add(rect8);

            foreach (var item in this.windowGrid.Children)
            {
                if (item is Rectangle)
                {
                    Rectangle resizeRectangle = (Rectangle)item;
                    resizeRectangle.PreviewMouseDown += this.ResizeRectangle_PreviewMouseDown;
                    resizeRectangle.MouseMove += this.ResizeRectangle_MouseMove;
                }
            }
        }

        private void ResizeRectangle_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.ResizeMode == RRQMResizeMode.NoResize || this.WindowState == WindowState.Maximized)
            {
                return;
            }

            Rectangle rectangle = sender as Rectangle;

            if (rectangle != null)
            {
                switch (rectangle.Name)
                {
                    case "Top":
                        this.Cursor = Cursors.SizeNS;
                        break;

                    case "Bottom":
                        this.Cursor = Cursors.SizeNS;
                        break;

                    case "Left":
                        this.Cursor = Cursors.SizeWE;
                        break;

                    case "Right":
                        this.Cursor = Cursors.SizeWE;
                        break;

                    case "TopLeft":
                        this.Cursor = Cursors.SizeNWSE;
                        break;

                    case "TopRight":
                        this.Cursor = Cursors.SizeNESW;
                        break;

                    case "BottomLeft":
                        this.Cursor = Cursors.SizeNESW;
                        break;

                    case "BottomRight":
                        this.Cursor = Cursors.SizeNWSE;
                        break;

                    default:
                        break;
                }
            }
        }

        private void ResizeRectangle_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.ResizeMode == RRQMResizeMode.NoResize || this.WindowState == WindowState.Maximized)
            {
                return;
            }
            Rectangle rectangle = sender as Rectangle;

            if (rectangle != null)
            {
                switch (rectangle.Name)
                {
                    case "Top":
                        this.Cursor = Cursors.SizeNS;
                        this.ResizeWindow(ResizeDirection.Top);
                        break;

                    case "Bottom":
                        this.Cursor = Cursors.SizeNS;
                        this.ResizeWindow(ResizeDirection.Bottom);
                        break;

                    case "Left":
                        this.Cursor = Cursors.SizeWE;
                        this.ResizeWindow(ResizeDirection.Left);
                        break;

                    case "Right":
                        this.Cursor = Cursors.SizeWE;
                        this.ResizeWindow(ResizeDirection.Right);
                        break;

                    case "TopLeft":
                        this.Cursor = Cursors.SizeNWSE;
                        this.ResizeWindow(ResizeDirection.TopLeft);
                        break;

                    case "TopRight":
                        this.Cursor = Cursors.SizeNESW;
                        this.ResizeWindow(ResizeDirection.TopRight);
                        break;

                    case "BottomLeft":
                        this.Cursor = Cursors.SizeNESW;
                        this.ResizeWindow(ResizeDirection.BottomLeft);
                        break;

                    case "BottomRight":
                        this.Cursor = Cursors.SizeNWSE;
                        this.ResizeWindow(ResizeDirection.BottomRight);
                        break;

                    default:
                        break;
                }
            }
        }

        private void ResizeWindow(ResizeDirection direction)
        {
            SendMessage(this._hwndSource.Handle, 0x112, (IntPtr)(61440 + direction), IntPtr.Zero);
        }

        private void titleGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (this.ResizeMode != RRQMResizeMode.CanResize) return;
                this.WindowState = this.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
            }
        }
        private void titleGrid_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                SendMessage(this._hwndSource.Handle, WM_SYSCOMMAND, (IntPtr)61458, IntPtr.Zero);
            }
        }
    }
}
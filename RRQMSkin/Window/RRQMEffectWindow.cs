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
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Shapes;
using System.Windows.Shell;

namespace RRQMSkin.Windows
{
    /// <summary>
    /// 自定义窗口
    /// </summary>
    public class RRQMEffectWindow : RRQMWindow
    {
        #region Fields

        /// <summary>
        /// 圆角角度属性
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(RRQMEffectWindow), new PropertyMetadata(new CornerRadius(0)));

        // Using a DependencyProperty as the backing store for ShadowBlurRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShadowBlurRadiusProperty =
            DependencyProperty.Register("ShadowBlurRadius", typeof(double), typeof(RRQMEffectWindow), new PropertyMetadata(5.0));

        // Using a DependencyProperty as the backing store for ShadowColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShadowColorProperty =
            DependencyProperty.Register("ShadowColor", typeof(Color), typeof(RRQMEffectWindow), new PropertyMetadata(Colors.Black));

        // Using a DependencyProperty as the backing store for AllowsTransparency.  This enables animation, styling, binding, etc...
        private static new readonly DependencyProperty AllowsTransparencyProperty =
            DependencyProperty.Register("AllowsTransparency", typeof(bool), typeof(RRQMEffectWindow), new PropertyMetadata(true));

        private HwndSource _hwndSource;

        private Grid windowGrid;

        #endregion Fields

        #region Constructors

        static RRQMEffectWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RRQMEffectWindow), new FrameworkPropertyMetadata(typeof(RRQMEffectWindow)));
        }

        /// <summary>
        ///
        /// </summary>
        public RRQMEffectWindow()
        {
            base.Background = Brushes.White;
            this.ResizeMode = RRQMResizeMode.CanResize;
            this.BorderThickness = new Thickness(1.0);
            this.ShadowBlurRadius = 10.0;
            base.AllowsTransparency = true;
            this.Loaded += RRQMWindow_Loaded;
            WindowChrome windowChrome = new WindowChrome();
            windowChrome.ResizeBorderThickness = new Thickness(0);
            this.SetValue(WindowChrome.WindowChromeProperty, null);
        }

        #endregion Constructors

        #region Enums

        public enum ResizeDirection
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

        #endregion Enums

        #region Properties

        public new bool AllowsTransparency
        {
            get { return (bool)GetValue(AllowsTransparencyProperty); }
            private set { SetValue(AllowsTransparencyProperty, value); }
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
        /// 阴影扩散度
        /// </summary>
        public double ShadowBlurRadius
        {
            get { return (double)GetValue(ShadowBlurRadiusProperty); }
            set { SetValue(ShadowBlurRadiusProperty, value); }
        }

        /// <summary>
        /// 阴影颜色
        /// </summary>
        public Color ShadowColor
        {
            get { return (Color)GetValue(ShadowColorProperty); }
            set { SetValue(ShadowColorProperty, value); }
        }

        #endregion Properties

        #region Methods

        private Border mainBorder;

        /// <summary>
        ///
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            // titleGrid = (Grid)this.Template.FindName("title", this);
            mainBorder = (Border)this.Template.FindName("mainBorder", this);
            //this.titleGrid.MouseLeftButtonDown += titleGrid_MouseLeftButtonDown;
            //this.titleGrid.MouseMove += titleGrid_MouseMove;
            windowGrid = (Grid)this.Template.FindName("windowGrid", this);

            mainBorder.Margin = new Thickness(this.ShadowBlurRadius);
            DropShadowEffect shadowEffect = new DropShadowEffect();
            shadowEffect.ShadowDepth = 0;
            shadowEffect.Color = this.ShadowColor;
            shadowEffect.BlurRadius = this.ShadowBlurRadius;
            mainBorder.Effect = shadowEffect;

            RowDefinition row0 = new RowDefinition();
            row0.Height = new GridLength(10);
            RowDefinition row1 = new RowDefinition();
            RowDefinition row2 = new RowDefinition();
            row2.Height = new GridLength(10);

            ColumnDefinition column0 = new ColumnDefinition();
            column0.Width = new GridLength(10);
            ColumnDefinition column1 = new ColumnDefinition();
            ColumnDefinition column2 = new ColumnDefinition();
            column2.Width = new GridLength(10);

            windowGrid.RowDefinitions.Add(row0);
            windowGrid.RowDefinitions.Add(row1);
            windowGrid.RowDefinitions.Add(row2);
            windowGrid.ColumnDefinitions.Add(column0);
            windowGrid.ColumnDefinitions.Add(column1);
            windowGrid.ColumnDefinitions.Add(column2);

            mainBorder.SetValue(Grid.RowProperty, 0);
            mainBorder.SetValue(Grid.ColumnProperty, 0);
            mainBorder.SetValue(Grid.RowSpanProperty, 3);
            mainBorder.SetValue(Grid.ColumnSpanProperty, 3);

            AddResizeRectangle();
        }

        protected override void OnInitialized(EventArgs e)
        {
            SourceInitialized += MainWindow_SourceInitialized;
            base.OnInitialized(e);
        }

        protected override void OnPreviewMouseMove(MouseEventArgs e)
        {
            base.OnPreviewMouseMove(e);
            if (e.LeftButton != MouseButtonState.Pressed)
                Cursor = Cursors.Arrow;
        }

        protected override void OnStateChanged(EventArgs e)
        {
            switch (this.WindowState)
            {
                case WindowState.Maximized:

                    this.mainBorder.Margin = new Thickness(0);

                    break;

                case WindowState.Normal:
                    this.mainBorder.Margin = new Thickness(this.ShadowBlurRadius);

                    break;
            }
            base.OnStateChanged(e);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, UInt32 msg, IntPtr wParam, IntPtr lParam);

        //    }
        private void AddResizeRectangle()
        {
            Rectangle rect1 = new Rectangle() { Name = "TopLeft", Fill = Brushes.Transparent };
            rect1.SetValue(Grid.RowProperty, 0);
            rect1.SetValue(Grid.ColumnProperty, 0);

            Rectangle rect2 = new Rectangle() { Name = "Top", Fill = Brushes.Transparent };
            rect2.SetValue(Grid.RowProperty, 0);
            rect2.SetValue(Grid.ColumnProperty, 1);

            Rectangle rect3 = new Rectangle() { Name = "TopRight", Fill = Brushes.Transparent };
            rect3.SetValue(Grid.RowProperty, 0);
            rect3.SetValue(Grid.ColumnProperty, 2);

            Rectangle rect4 = new Rectangle() { Name = "Right", Fill = Brushes.Transparent };
            rect4.SetValue(Grid.RowProperty, 1);
            rect4.SetValue(Grid.ColumnProperty, 2);

            Rectangle rect5 = new Rectangle() { Name = "BottomRight", Fill = Brushes.Transparent };
            rect5.SetValue(Grid.RowProperty, 2);
            rect5.SetValue(Grid.ColumnProperty, 2);

            Rectangle rect6 = new Rectangle() { Name = "Bottom", Fill = Brushes.Transparent };
            rect6.SetValue(Grid.RowProperty, 2);
            rect6.SetValue(Grid.ColumnProperty, 1);

            Rectangle rect7 = new Rectangle() { Name = "BottomLeft", Fill = Brushes.Transparent };
            rect7.SetValue(Grid.RowProperty, 2);
            rect7.SetValue(Grid.ColumnProperty, 0);

            Rectangle rect8 = new Rectangle() { Name = "Left", Fill = Brushes.Transparent };
            rect8.SetValue(Grid.RowProperty, 1);
            rect8.SetValue(Grid.ColumnProperty, 0);

            windowGrid.Children.Add(rect1);
            windowGrid.Children.Add(rect2);
            windowGrid.Children.Add(rect3);
            windowGrid.Children.Add(rect4);
            windowGrid.Children.Add(rect5);
            windowGrid.Children.Add(rect6);
            windowGrid.Children.Add(rect7);
            windowGrid.Children.Add(rect8);

            foreach (var item in windowGrid.Children)
            {
                if (item is Rectangle)
                {
                    Rectangle resizeRectangle = (Rectangle)item;
                    resizeRectangle.PreviewMouseDown += ResizeRectangle_PreviewMouseDown;
                    resizeRectangle.MouseMove += ResizeRectangle_MouseMove;
                }
            }
        }

        private void MainWindow_SourceInitialized(object sender, EventArgs e)
        {
            _hwndSource = (HwndSource)PresentationSource.FromVisual(this);
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
                        Cursor = Cursors.SizeNS;
                        break;

                    case "Bottom":
                        Cursor = Cursors.SizeNS;
                        break;

                    case "Left":
                        Cursor = Cursors.SizeWE;
                        break;

                    case "Right":
                        Cursor = Cursors.SizeWE;
                        break;

                    case "TopLeft":
                        Cursor = Cursors.SizeNWSE;
                        break;

                    case "TopRight":
                        Cursor = Cursors.SizeNESW;
                        break;

                    case "BottomLeft":
                        Cursor = Cursors.SizeNESW;
                        break;

                    case "BottomRight":
                        Cursor = Cursors.SizeNWSE;
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
                        Cursor = Cursors.SizeNS;
                        ResizeWindow(ResizeDirection.Top);
                        break;

                    case "Bottom":
                        Cursor = Cursors.SizeNS;
                        ResizeWindow(ResizeDirection.Bottom);
                        break;

                    case "Left":
                        Cursor = Cursors.SizeWE;
                        ResizeWindow(ResizeDirection.Left);
                        break;

                    case "Right":
                        Cursor = Cursors.SizeWE;
                        ResizeWindow(ResizeDirection.Right);
                        break;

                    case "TopLeft":
                        Cursor = Cursors.SizeNWSE;
                        ResizeWindow(ResizeDirection.TopLeft);
                        break;

                    case "TopRight":
                        Cursor = Cursors.SizeNESW;
                        ResizeWindow(ResizeDirection.TopRight);
                        break;

                    case "BottomLeft":
                        Cursor = Cursors.SizeNESW;
                        ResizeWindow(ResizeDirection.BottomLeft);
                        break;

                    case "BottomRight":
                        Cursor = Cursors.SizeNWSE;
                        ResizeWindow(ResizeDirection.BottomRight);
                        break;

                    default:
                        break;
                }
            }
        }

        private void ResizeWindow(ResizeDirection direction)
        {
            SendMessage(_hwndSource.Handle, 0x112, (IntPtr)(61440 + direction), IntPtr.Zero);
        }

        private void RRQMWindow_Loaded(object sender, RoutedEventArgs e)
        {
            OnStateChanged(e);
        }

        #endregion Methods
    }
}
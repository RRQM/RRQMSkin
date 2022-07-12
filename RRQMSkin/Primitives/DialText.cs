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
using RRQMSkin.Controls;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace RRQMSkin.Primitives
{
    /// <summary>
    /// 环形文字
    /// </summary>
    public class DialText : RRQMControl
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="drawingContext"></param>
        protected sealed override void OnRender(DrawingContext drawingContext)
        {
            if (this.Text == null || this.Text.Length == 0)
            {
                return;
            }
            double radius = Math.Min(this.ActualHeight, this.ActualWidth) / 2;
            if (radius > 0)
            {
                double widthRadius = this.RadiusRatio * this.ActualWidth / 2;
                double heightRadius = this.RadiusRatio * this.ActualHeight / 2;

                string[] texts;
                if (this.TextShowStyle == TextShowStyle.Split)
                {
                    texts = this.Text.Split(',');
                }
                else
                {
                    List<string> str = new List<string>();
                    foreach (var item in this.Text)
                    {
                        str.Add(item.ToString());
                    }
                    texts = str.ToArray();
                }

                Canvas canvas = new Canvas();
                canvas.Width = this.ActualWidth;
                canvas.Height = this.ActualHeight;
                canvas.Background = Brushes.Transparent;
                for (int i = 0; i < texts.Length; i++)
                {
                    double width = GetTextDisplayWidth(texts[i], this.FontFamily, this.FontStyle, this.FontWeight, FontStretches.Normal, this.FontSize);

                    TextBlock textBlock = new TextBlock();
                    textBlock.Foreground = this.Foreground;
                    textBlock.FontFamily = this.FontFamily;
                    textBlock.FontStyle = this.FontStyle;
                    textBlock.FontWeight = this.FontWeight;
                    textBlock.FontSize = this.FontSize;
                    textBlock.Text = texts[i];
                    textBlock.SetValue(Canvas.LeftProperty, widthRadius * Math.Cos((this.StartAngle + this.TickAngle * i) * Math.PI / 180) + radius - width / 2);
                    textBlock.SetValue(Canvas.TopProperty, heightRadius * Math.Sin((this.StartAngle + this.TickAngle * i) * Math.PI / 180) + radius - this.FontSize / 2);
                    canvas.Children.Add(textBlock);
                }

                GeometryDrawing geometryDrawing = new GeometryDrawing();

                geometryDrawing.Geometry = new RectangleGeometry(new Rect(0, 0, this.ActualWidth, this.ActualHeight));

                VisualBrush visualBrush = new VisualBrush();
                visualBrush.Visual = canvas;

                geometryDrawing.Brush = visualBrush;

                drawingContext.DrawDrawing(geometryDrawing);
            }
        }

        /// <summary>
        /// 半径比例
        /// </summary>
        public double RadiusRatio
        {
            get => (double)this.GetValue(RadiusRatioProperty);
            set => this.SetValue(RadiusRatioProperty, value);
        }

        /// <summary>
        /// 半径比例属性
        /// </summary>
        public static readonly DependencyProperty RadiusRatioProperty =
            DependencyProperty.Register("RadiusRatio", typeof(double), typeof(DialText), new PropertyMetadata(0.3, OnChanged));

        /// <summary>
        /// 单位角度
        /// </summary>
        public double TickAngle
        {
            get => (double)this.GetValue(TickAngleProperty);
            set => this.SetValue(TickAngleProperty, value);
        }

        /// <summary>
        /// 单位角度属性
        /// </summary>
        public static readonly DependencyProperty TickAngleProperty =
            DependencyProperty.Register("TickAngle", typeof(double), typeof(DialText), new PropertyMetadata(30.0, OnChanged));

        /// <summary>
        /// 文字显示样式
        /// </summary>
        public TextShowStyle TextShowStyle
        {
            get => (TextShowStyle)this.GetValue(TextShowStyleProperty);
            set => this.SetValue(TextShowStyleProperty, value);
        }

        /// <summary>
        /// 文字显示样式属性
        /// </summary>
        public static readonly DependencyProperty TextShowStyleProperty =
            DependencyProperty.Register("TextShowStyle", typeof(TextShowStyle), typeof(DialText), new PropertyMetadata(TextShowStyle.Split, OnChanged));

        /// <summary>
        /// 显示文本
        /// </summary>
        public string Text
        {
            get => (string)this.GetValue(TextProperty);
            set => this.SetValue(TextProperty, value);
        }

        /// <summary>
        /// 显示文本属性
        /// </summary>
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(DialText), new PropertyMetadata("1,2,3", OnChanged));

        /// <summary>
        /// 开始角度
        /// </summary>
        public double StartAngle
        {
            get => (double)this.GetValue(StartAngleProperty);
            set => this.SetValue(StartAngleProperty, value);
        }

        /// <summary>
        /// 开始角度属性
        /// </summary>
        public static readonly DependencyProperty StartAngleProperty =
            DependencyProperty.Register("StartAngle", typeof(double), typeof(DialText), new PropertyMetadata(0.0, OnChanged));

        private static void OnChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((DialText)d).InvalidateVisual();
        }

        private static double GetTextDisplayWidth(string str, FontFamily fontFamily, FontStyle fontStyle, FontWeight fontWeight, FontStretch fontStretch, double FontSize)
        {
#pragma warning disable
            Typeface typeface = new Typeface(fontFamily, fontStyle, fontWeight, fontStretch);
            var formattedText = new FormattedText(str, CultureInfo.CurrentUICulture, FlowDirection.LeftToRight, typeface, FontSize, Brushes.Black);
            Size size = new Size(formattedText.Width, formattedText.Height);
            return size.Width;
        }
    }
}
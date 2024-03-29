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
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RRQMSkin.Controls
{
    public class InputBox : TextBox
    {
        static InputBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(InputBox), new FrameworkPropertyMetadata(typeof(InputBox)));
        }

        public InputBox()
        {
            this.PreviewTextInput += this.TipTextBox_PreviewTextInput;
            this.TextChanged += this.InputBox_TextChanged;
        }

        private void InputBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            switch (this.InputFilter)
            {
                case InputFilter.Nono:
                    this.IsComplies = true;
                    break;

                case InputFilter.Uint:
                    this.IsComplies = MetarnetRegex.IsNotNagtive(textBox.Text);
                    break;

                case InputFilter.Number:
                    this.IsComplies = MetarnetRegex.IsNumber(textBox.Text);
                    break;

                case InputFilter.Chinese:
                    this.IsComplies = MetarnetRegex.IsChineseCh(textBox.Text);
                    break;

                case InputFilter.MobilePhone:
                    this.IsComplies = MetarnetRegex.IsMobilePhone(textBox.Text);
                    break;

                case InputFilter.Email:
                    this.IsComplies = MetarnetRegex.IsEmail(textBox.Text);
                    break;

                case InputFilter.URL:
                    this.IsComplies = MetarnetRegex.IsURL(textBox.Text);
                    break;

                case InputFilter.Letter:
                    this.IsComplies = MetarnetRegex.IsEnglisCh(textBox.Text);
                    break;

                case InputFilter.IDcard:
                    this.IsComplies = MetarnetRegex.IsIDcard(textBox.Text);
                    break;

                case InputFilter.IPv4:
                    this.IsComplies = MetarnetRegex.IsIPv4(textBox.Text);
                    break;

                case InputFilter.IPv6:
                    this.IsComplies = MetarnetRegex.IsIPV6(textBox.Text);
                    break;
            }
        }

        private bool isShow = true;
        private Grid grid;
        public static readonly string[] number = new string[] { ".", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

        private void TipTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            switch (this.InputFilter)
            {
                case InputFilter.Nono:
                    break;

                case InputFilter.Uint:
                    {
                        if (e.Text != ".")
                        {
                            if (number.Contains(e.Text))
                            {
                                e.Handled = false;
                            }
                            else
                            {
                                e.Handled = true;
                            }
                        }
                        else
                        {
                            e.Handled = true;
                        }
                        break;
                    }

                case InputFilter.Number:
                    {
                        if (e.Text != ".")
                        {
                            if (number.Contains(e.Text))
                            {
                                e.Handled = false;
                            }
                            else
                            {
                                e.Handled = true;
                            }
                        }
                        else
                        {
                            if (((TextBox)sender).Text.Contains("."))
                            {
                                e.Handled = true;
                            }
                            else
                            {
                                e.Handled = false;
                            }
                        }

                        break;
                    }

                case InputFilter.Chinese:
                    e.Handled = !MetarnetRegex.IsChineseCh(e.Text);
                    break;

                case InputFilter.MobilePhone:
                    {
                        if (textBox.Text.Length == 0)
                        {
                            if (e.Text != "1")
                            {
                                e.Handled = true;
                            }
                        }
                        break;
                    }
                case InputFilter.Email:
                    break;

                case InputFilter.URL:
                    break;

                case InputFilter.Letter:
                    {
                        Regex re = new Regex("[^A-z]+");
                        e.Handled = re.IsMatch(e.Text);
                        break;
                    }
            }
        }

        /// <summary>
        /// 提示文本
        /// </summary>
        [TypeConverter(typeof(TipTextBoxConverter))]
        public TextBlock TipText
        {
            get => (TextBlock)this.GetValue(TipTextProperty);
            set => this.SetValue(TipTextProperty, value);
        }

        /// <summary>
        /// 提示文本属性
        /// </summary>
        public static readonly DependencyProperty TipTextProperty =
            DependencyProperty.Register("TipText", typeof(TextBlock), typeof(InputBox), new PropertyMetadata(null));

        protected override void OnGotKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            base.OnGotKeyboardFocus(e);
            if (this.TipText != null)
            {
                this.TipText.Visibility = Visibility.Hidden;
            }
        }

        protected override void OnLostKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            base.OnLostKeyboardFocus(e);
            if (string.IsNullOrEmpty(this.Text) && this.TipText != null)
            {
                this.TipText.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// 输入是否合法
        /// </summary>
        public bool IsComplies
        {
            get => (bool)this.GetValue(IsCompliesProperty);
            private set => this.SetValue(IsCompliesProperty, value);
        }

        // Using a DependencyProperty as the backing store for IsComplies.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsCompliesProperty =
            DependencyProperty.Register("IsComplies", typeof(bool), typeof(InputBox), new PropertyMetadata(true));

        public InputFilter InputFilter
        {
            get => (InputFilter)this.GetValue(InputFilterProperty);
            set => this.SetValue(InputFilterProperty, value);
        }

        // Using a DependencyProperty as the backing store for InputFilter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InputFilterProperty =
            DependencyProperty.Register("InputFilter", typeof(InputFilter), typeof(InputBox), new PropertyMetadata(InputFilter.Nono, OnFilterChanged));

        private static void OnFilterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            InputBox inputBox = (InputBox)d;
            if (inputBox.InputFilter == InputFilter.Nono || inputBox.InputFilter == InputFilter.Chinese)
            {
                inputBox.SetValue(InputMethod.IsInputMethodEnabledProperty, true);
            }
            else
            {
                inputBox.SetValue(InputMethod.IsInputMethodEnabledProperty, false);
            }
        }

        public CornerRadius CornerRadius
        {
            get => (CornerRadius)this.GetValue(CornerRadiusProperty);
            set => this.SetValue(CornerRadiusProperty, value);
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(InputBox), new PropertyMetadata(new CornerRadius(0), OnCornerRadiusChanged));

        private static void OnCornerRadiusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            InputBox inputBox = (InputBox)d;
            inputBox.InvalidateVisual();
        }
    }
}
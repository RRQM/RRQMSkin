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
using System;
using System.Windows;
using System.Windows.Input;

namespace RRQMSkin.Controls
{
    public class DoubleEditTextBlock : InputBox
    {
        static DoubleEditTextBlock()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DoubleEditTextBlock), new FrameworkPropertyMetadata(typeof(DoubleEditTextBlock)));
        }

        /// <summary>
        /// 编辑完成
        /// </summary>
        public event Action<DoubleEditTextBlock, string> EditFinished;

        public bool DoubleEditEnable
        {
            get { return (bool)GetValue(DoubleEditEnableProperty); }
            set { SetValue(DoubleEditEnableProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DoubleEditEnable.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DoubleEditEnableProperty =
            DependencyProperty.Register("DoubleEditEnable", typeof(bool), typeof(DoubleEditTextBlock), new PropertyMetadata(false, DoubleEditEnableChanged));

        public ICommand EditFinishedCommand
        {
            get { return (ICommand)GetValue(EditFinishedCommandProperty); }
            set { SetValue(EditFinishedCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EditFinishedCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EditFinishedCommandProperty =
            DependencyProperty.Register("EditFinishedCommand", typeof(ICommand), typeof(DoubleEditTextBlock), new PropertyMetadata(null));

        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CommandParameter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register("CommandParameter", typeof(object), typeof(DoubleEditTextBlock), new PropertyMetadata(null));

        private static void DoubleEditEnableChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DoubleEditTextBlock textBlock = (DoubleEditTextBlock)d;
            if (textBlock.DoubleEditEnable)
            {
                textBlock.PreviewMouseDoubleClick += textBlock.DoubleEditTextBlock_MouseDoubleClick;
                textBlock.PreviewKeyUp += textBlock.TextBlock_PreviewKeyUp;
                textBlock.LostKeyboardFocus += textBlock.DoubleEditTextBlock_PreviewLostKeyboardFocus;
            }
            else
            {
                textBlock.PreviewKeyUp -= textBlock.TextBlock_PreviewKeyUp;
                textBlock.MouseDoubleClick -= textBlock.DoubleEditTextBlock_MouseDoubleClick; ;
                textBlock.LostKeyboardFocus -= textBlock.DoubleEditTextBlock_PreviewLostKeyboardFocus;
            }
        }

        private void TextBlock_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.IsReadOnly = true;
                if (this.EditFinishedCommand != null && this.EditFinishedCommand.CanExecute(this.CommandParameter))
                {
                    this.EditFinishedCommand.Execute(this.CommandParameter);
                }
            }
        }

        private void DoubleEditTextBlock_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.IsReadOnly = false;
        }

        private void DoubleEditTextBlock_PreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            this.IsReadOnly = true;
            this.EditFinished?.Invoke(this,this.Text);
            if (this.EditFinishedCommand != null && this.EditFinishedCommand.CanExecute(this.CommandParameter))
            {
                this.EditFinishedCommand.Execute(this.CommandParameter);
            }
        }
    }
}
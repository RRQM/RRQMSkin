using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RRQMSkin
{
    public class DoubleEditTextBlock : InputBox
    {
        static DoubleEditTextBlock()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DoubleEditTextBlock), new FrameworkPropertyMetadata(typeof(DoubleEditTextBlock)));
        }

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
            if (this.EditFinishedCommand != null && this.EditFinishedCommand.CanExecute(this.CommandParameter))
            {
                this.EditFinishedCommand.Execute(this.CommandParameter);
            }
        }
    }
}

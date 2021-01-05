using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RRQMSkin.Controls
{
    public class SearchBox : InputBox
    {
        static SearchBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SearchBox), new FrameworkPropertyMetadata(typeof(SearchBox)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            Button commandButton = (Button)this.Template.FindName("commandButton", this);
            if (commandButton != null)
            {
                commandButton.Click += CommandButton_Click;
            }
        }

        private void CommandButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Command == null)
            {
                return;
            }
            if (this.Command.CanExecute(this.CommandParameter))
            {
                this.Command.Execute(this.CommandParameter);
            }
        }

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Command.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(SearchBox), new PropertyMetadata(null));

        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CommandParameter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register("CommandParameter", typeof(object), typeof(SearchBox), new PropertyMetadata(null));
    }
}
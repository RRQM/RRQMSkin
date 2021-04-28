using System;
using System.Timers;
using System.Windows;
using System.Windows.Controls;

namespace RRQMSkin.Demo.Display
{
    /// <summary>
    /// MainPanel.xaml 的交互逻辑
    /// </summary>
    public partial class MainPanel : WrapPanel
    {
        public MainPanel()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Timer timer = new Timer(1000);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                this.Dispatcher.Invoke(() =>
                {
                    this.clock.Time = new TimeSpan(DateTime.Now.Ticks); ;
                });
            }
            catch (Exception)
            {
            }
        }
    }
}
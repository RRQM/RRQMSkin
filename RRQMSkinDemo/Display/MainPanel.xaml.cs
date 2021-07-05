//------------------------------------------------------------------------------
//  此代码版权归作者本人若汝棋茗所有
//  源代码使用协议遵循本仓库的开源协议及附加协议，若本仓库没有设置，则按MIT开源协议授权
//  CSDN博客：https://blog.csdn.net/qq_40374647
//  哔哩哔哩视频：https://space.bilibili.com/94253567
//  源代码仓库：https://gitee.com/RRQM_Home
//  交流QQ群：234762506
//  感谢您的下载和使用
//------------------------------------------------------------------------------
using System;
using System.Timers;
using System.Windows;
using System.Windows.Controls;

namespace RRQMSkinDemo.Display
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
                    //this.clock.Time = new TimeSpan(DateTime.Now.Ticks); ;
                });
            }
            catch (Exception)
            {
            }
        }

        private void MyTag_CloseClick(object sender, RoutedEventArgs e)
        {
            var temp = sender as RRQMSkin.Controls.LabelTag;
            temp.Text = $"我被点击了 我的ID是:{temp.TagID}";
        }
    }
}
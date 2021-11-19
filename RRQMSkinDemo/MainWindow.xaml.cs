//------------------------------------------------------------------------------
//  此代码版权归作者本人若汝棋茗所有
//  源代码使用协议遵循本仓库的开源协议及附加协议，若本仓库没有设置，则按MIT开源协议授权
//  CSDN博客：https://blog.csdn.net/qq_40374647
//  哔哩哔哩视频：https://space.bilibili.com/94253567
//  源代码仓库：https://gitee.com/RRQM_Home
//  交流QQ群：234762506
//  本仓库其他贡献者：
//  Mr:牧影：https://gitee.com/fuck666
//  火狼：https://gitee.com/wudiliang
//  感谢您的下载和使用
//------------------------------------------------------------------------------
//------------------------------------------------------------------------------
using RRQMSkin.Windows;

namespace RRQMSkinDemo
{  
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : RRQMWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            RRQMEffectWindow window = new RRQMEffectWindow();
            window.Width = 400;
            window.Height = 500;
            window.MinWidth = 300;
            window.MinHeight = 400;
            window.MaxWidth = 500;
            window.MaxHeight = 600;
            window.Show();
        }

        private void SearchBoxDisplay_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }
    }
}
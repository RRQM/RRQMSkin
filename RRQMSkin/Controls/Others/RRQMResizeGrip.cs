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
using System.Windows;
using System.Windows.Controls;

namespace RRQMSkin.Controls
{
    public class RRQMResizeGrip : Decorator
    {
        public bool ResizeGripEnable
        {
            get => (bool)this.GetValue(ResizeGripEnableProperty);
            set => this.SetValue(ResizeGripEnableProperty, value);
        }

        // Using a DependencyProperty as the backing store for ResizeGripEnable.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ResizeGripEnableProperty =
            DependencyProperty.Register("ResizeGripEnable", typeof(bool), typeof(RRQMResizeGrip), new PropertyMetadata(false));
    }
}
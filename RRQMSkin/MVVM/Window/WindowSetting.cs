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

namespace RRQMSkin.MVVM
{
    /// <summary>
    /// 显示窗口设置
    /// </summary>
    public class WindowSetting
    {
        /// <summary>
        /// 窗口ID
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// Window类型
        /// </summary>
        public Type WindowType { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public WindowState WindowState { get; set; }

        /// <summary>
        /// 显示类型
        /// </summary>
        public ShowMode ShowMode { get; set; }

        /// <summary>
        /// 构造函数参数
        /// </summary>
        public object[] Parameters { get; set; }


    }
}

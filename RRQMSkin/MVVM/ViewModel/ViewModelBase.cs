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
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace RRQMSkin.MVVM
{

    /// <summary>
    /// ViewModel基类
    /// </summary>
    [Serializable]
    public class ViewModelBase : ObservableObject
    {


        /// <summary>
        /// 判断是不是设计器模式
        /// </summary>
        public bool IsInDesignMode
        {
            get { return DesignerProperties.GetIsInDesignMode(new DependencyObject()); }
        }

        [field: NonSerialized]
        internal FrameworkElement view;

        /// <summary>
        /// 目标View
        /// </summary>
        public FrameworkElement View
        {
            get { return view; }
        }
    }
}

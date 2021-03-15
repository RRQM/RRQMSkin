//------------------------------------------------------------------------------
//  此代码版权归作者本人若汝棋茗所有
//  源代码使用协议遵循本仓库的开源协议，若本仓库没有设置，则按MIT开源协议授权
//  CSDN博客：https://blog.csdn.net/qq_40374647
//  哔哩哔哩视频：https://space.bilibili.com/94253567
//  源代码仓库：https://gitee.com/RRQM_Home
//  交流QQ群：234762506
//  感谢您的下载和使用
//------------------------------------------------------------------------------
//------------------------------------------------------------------------------
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace RRQMSkin.Charts.Primitives
{
    /// <summary>
    /// 若汝棋茗绘图基础类
    /// </summary>
    public abstract class RRQMShape : Shape
    {
        /// <summary>
        ///
        /// </summary>
        public RRQMShape()
        {
            this.SizeChanged += Sector_SizeChanged;
        }

        public bool IsInDesignMode
        {
            get { return DesignerProperties.GetIsInDesignMode(this); }
        }

        private void Sector_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.InvalidateVisual();
        }
        /// <summary>
        ///
        /// </summary>
        protected override Geometry DefiningGeometry { get { return CreatGeometry(); } }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        protected abstract Geometry CreatGeometry();
    }
}
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
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RRQMSkin.Controls
{
    public class RRQMControl : Control
    {
        public RRQMControl()
        {
            this.SizeChanged += this.PieChart_SizeChanged;
        }

        private void PieChart_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.InvalidateArrange();
            this.InvalidateMeasure();
            this.InvalidateVisual();
        }
        public bool IsInDesignMode
        {
            get { return DesignerProperties.GetIsInDesignMode(this); }
        }
    }
}

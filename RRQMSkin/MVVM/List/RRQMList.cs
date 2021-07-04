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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRQMSkin.MVVM
{
    /// <summary>
    /// 继承ObservableCollection的集合
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RRQMList<T>: ObservableCollection<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public RRQMList()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        public RRQMList(List<T> list)
        {
            foreach (var item in list)
            {
                Add(item);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        public RRQMList(IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                Add(item);
            }
        }
    }
}

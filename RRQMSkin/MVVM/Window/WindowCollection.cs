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
using System.Collections.Concurrent;
using System.Linq;
using System.Windows;

namespace RRQMSkin.MVVM
{
    /// <summary>
    /// 窗口集合
    /// </summary>
    public class WindowCollection
    {
        private ConcurrentDictionary<string, Window> windows = new ConcurrentDictionary<string, Window>();
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="id"></param>
        /// <param name="window"></param>
        internal void Add(string id, Window window)
        {
            window.SetValue(WindowHelper.RRQMIDProperty, id);
            this.count++;
            window.Closed += this.Window_Closed;
            window.Activate();
            if (!this.windows.TryAdd(id, window))
            {
                throw new Exception("ID重复");
            }
        }

        internal void CloseTypeWindow(Type type)
        {
            foreach (var key in this.windows.Keys)
            {
                Window window;
                if (this.windows.TryGetValue(key, out window))
                {
                    if (window.GetType().FullName == type.FullName)
                    {
                        this.windows.TryRemove(key, out _);
                    }
                }
            }
        }

        internal void CloseWindow(string id)
        {
            Window window;
            if (this.windows.TryGetValue(id, out window))
            {
                window.Close();
            }
        }

        internal void CloseAllWindow()
        {
            foreach (var key in this.windows.Keys)
            {
                Window window;
                if (this.windows.TryGetValue(key, out window))
                {
                    window.Close();
                }
            }
        }

        internal Window GetWindow(string id)
        {
            Window window;
            this.windows.TryGetValue(id, out window);
            return window;
        }
        /// <summary>
        /// 获取所有窗口
        /// </summary>
        /// <returns></returns>
        public Window[] GetWindows()
        {
            return this.windows.Values.ToArray();
        }
        int count;
        internal string GetRandomID()
        {
            string key = "win" + this.count;
            while (this.windows.ContainsKey(key))
            {
                this.count++;
                key = "win" + this.count;
            }
            return key;
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            foreach (var key in this.windows.Keys)
            {
                Window window;
                if (this.windows.TryGetValue(key, out window))
                {
                    if (window == sender)
                    {
                        this.windows.TryRemove(key, out _);
                        break;
                    }
                }
            }
        }
    }
}

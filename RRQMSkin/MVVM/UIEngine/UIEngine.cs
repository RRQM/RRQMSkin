using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RRQMSkin.MVVM
{
    /// <summary>
    /// UI引擎
    /// </summary>
    public class UIEngine
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public UIEngine()
        {
            this.uis = new Dictionary<object, UIElement>();
        }
        private readonly Dictionary<object, UIElement> uis;

        /// <summary>
        /// 注册单例
        /// </summary>
        /// <typeparam name="TUI"></typeparam>
        /// <param name="args"></param>
        /// <returns></returns>
        public UIElement RegisterUI<TUI>(object[] args) where TUI : UIElement
        {
            TUI obj = (TUI)Activator.CreateInstance(typeof(TUI), args);
            this.uis.Add(obj,obj);
            return obj;
        }

        /// <summary>
        /// 获取单例
        /// </summary>
        /// <typeparam name="TUI"></typeparam>
        /// <param name="args"></param>
        /// <returns></returns>
        public UIElement GetSingleUI<TUI>(object[] args) where TUI : UIElement
        {
            foreach (var item in this.uis.Values)
            {
                if (item.GetType()==typeof(TUI))
                {
                    return item;
                }
            }
            return this.RegisterUI<TUI>(args);
        }
    }
}

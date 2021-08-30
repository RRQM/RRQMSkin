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
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RRQMSkin.MVVM
{
    /*
    若汝棋茗
    */
    /// <summary>
    /// ViewModelLocator基类
    /// </summary>
    public class SimpleIoc
    {
        private SimpleIoc()
        {

        }
        private static SimpleIoc instance;

        /// <summary>
        /// 默认单例实例
        /// </summary>
        public static SimpleIoc Default
        {
            get
            {
                if (instance == null)
                {
                    instance = new SimpleIoc();
                }

                return instance;
            }
        }

        private ConcurrentDictionary<ViewModelBase, FrameworkElement> viewModelBases = new ConcurrentDictionary<ViewModelBase, FrameworkElement>();

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="element"></param>
        /// <param name="viewModel"></param>
        public void Register(ViewModelBase viewModel, FrameworkElement element)
        {
            if (element!=null)
            {
                element.DataContext = viewModel;
            }
            viewModel.view = element;
            viewModelBases.TryAdd(viewModel, element);
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="viewModel"></param>
        public void Register(ViewModelBase viewModel)
        {
            this.Register(viewModel, null);
        }

        public void Register<T>(FrameworkElement element) where T : ViewModelBase, new()
        {
            this.Register((ViewModelBase)Activator.CreateInstance(typeof(T)), element);
        }

        public void Register<T>() where T : ViewModelBase, new()
        {
            this.Register((ViewModelBase)Activator.CreateInstance(typeof(T)), null);
        }

        /// <summary>
        /// 获取ViewModel
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetViewModelInstance<T>() where T : ViewModelBase
        {
            foreach (var item in viewModelBases.Keys)
            {
                if (item.GetType() == typeof(T))
                {
                    return (T)item;
                }
            }
            return default;
        }
    }
}

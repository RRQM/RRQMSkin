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
using System.Linq;
using System.Windows;

namespace RRQMSkin.MVVM
{
    /// <summary>
    /// 窗口管理
    /// </summary>
    public static class WindowsManager
    {
        private static WindowCollection windows = new WindowCollection();
        /// <summary>
        /// 获取已创建的窗口
        /// </summary>
        public static Window[] Windows => windows.GetWindows();

        /// <summary>
        /// 创建新窗体
        /// </summary>
        /// <param name="windowSetting"></param>
        /// <returns>窗口ID</returns>
        public static string CreateWindow(WindowSetting windowSetting)
        {
            Window window = Activator.CreateInstance(windowSetting.WindowType, windowSetting.Parameters) as Window;
            window.WindowState = windowSetting.WindowState;

            windows.Add(windowSetting.ID, window);

            switch (windowSetting.ShowMode)
            {
                case ShowMode.Show:
                    window.Show();
                    break;
                case ShowMode.Dialog:
                    window.ShowDialog();
                    break;
            }
            return windowSetting.ID;
        }

        /// <summary>
        /// 创建新窗体
        /// </summary>
        /// <param name="id"></param>
        /// <param name="windowType"></param>
        /// <param name="parameters"></param>
        /// <returns>窗口ID</returns>
        public static string CreateWindow(string id, Type windowType, params object[] parameters)
        {
            Window window = Activator.CreateInstance(windowType, parameters) as Window;
            windows.Add(id, window);
            window.Show();
            return id;
        }
        public static Window CreateWindow<T>(string id, params object[] parameters) where T : Window
        {
            Window window = Activator.CreateInstance(typeof(T), parameters) as Window;
            if (string.IsNullOrEmpty(id))
            {
                id = windows.GetRandomID();
            }
            windows.Add(id, window);
            window.Show();
            return window;
        }

        public static Window CreateWindow<T>(string id) where T : Window
        {
            return CreateWindow<T>(id, null);
        }

        public static Window CreateWindow<T>() where T : Window
        {
            return CreateWindow<T>(null, null);
        }

        /// <summary>
        /// 创建新窗体
        /// </summary>
        /// <param name="windowType"></param>
        /// <param name="parameters"></param>
        /// <returns>窗口ID</returns>
        public static string CreateWindow(Type windowType, params object[] parameters)
        {
            Window window = Activator.CreateInstance(windowType, parameters) as Window;
            string id = windows.GetRandomID();
            windows.Add(id, window);
            window.Show();
            return id;
        }

        /// <summary>
        /// 创建对话框窗体
        /// </summary>
        /// <param name="id"></param>
        /// <param name="windowType"></param>
        /// <param name="parameters"></param>
        public static object CreateDialogWindow(string id, Type windowType, params object[] parameters)
        {
            Window window = Activator.CreateInstance(windowType, parameters) as Window;

            windows.Add(id, window);
            return window.ShowDialog();
        }

        /// <summary>
        /// 创建对话框窗体
        /// </summary>
        /// <param name="windowType"></param>
        /// <param name="parameters"></param>
        public static object CreateDialogWindow(Type windowType, params object[] parameters)
        {
            Window window = Activator.CreateInstance(windowType, parameters) as Window;

            windows.Add(windows.GetRandomID(), window);
            return window.ShowDialog();
        }

        /// <summary>
        /// 创建对话框窗体
        /// </summary>
        /// <param name="window"></param>
        public static object CreateDialogWindow(Window window)
        {
            windows.Add(windows.GetRandomID(), window);
            return window.ShowDialog();
        }

        /// <summary>
        /// 创建对话框窗体
        /// </summary>
        /// <param name="id"></param>
        /// <param name="window"></param>
        /// <returns></returns>
        public static object CreateDialogWindow(string id, Window window)
        {
            windows.Add(id, window);
            return window.ShowDialog();
        }

        /// <summary>
        /// 创建新窗体
        /// </summary>
        /// <param name="id"></param>
        /// <param name="windowType"></param>
        /// <param name="showMode"></param>
        public static void CreateWindow(string id, string windowType, ShowMode showMode)
        {
            var types = AppDomain.CurrentDomain.GetAssemblies()
                        .SelectMany(a => a.GetTypes().Where(t => t.Name == windowType))
                        .ToArray();
            if (types.Length == 1)
            {
                Window window = Activator.CreateInstance(types[0]) as Window;

                windows.Add(id, window);

                switch (showMode)
                {
                    case ShowMode.Show:
                        window.Show();
                        break;
                    case ShowMode.Dialog:
                        window.ShowDialog();
                        break;
                }
            }
            else
            {
                throw new Exception("没有找到该窗体类型或窗体类型不唯一");
            }


        }


        /// <summary>
        /// 创建新窗体
        /// </summary>
        /// <param name="id"></param>
        /// <param name="window"></param>
        /// <param name="showMode"></param>
        public static void CreateWindow(string id, Window window, ShowMode showMode)
        {
            windows.Add(id, window);
            switch (showMode)
            {
                case ShowMode.Show:
                    window.Show();
                    break;
                case ShowMode.Dialog:
                    window.ShowDialog();
                    break;
            }
        }

        /// <summary>
        /// 创建新窗体
        /// </summary>
        /// <param name="window"></param>
        /// <param name="showMode"></param>
        /// <returns></returns>
        public static string CreateWindow(Window window, ShowMode showMode)
        {
            string id = windows.GetRandomID();
            windows.Add(id, window);
            switch (showMode)
            {
                case ShowMode.Show:
                    window.Show();
                    break;
                case ShowMode.Dialog:
                    window.ShowDialog();
                    break;
            }
            return id;
        }

        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="window"></param>
        public static void CloseWindow(Window window)
        {
            window.Close();
        }

        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="id"></param>
        public static void CloseWindow(string id)
        {
            windows.CloseWindow(id);
        }

        /// <summary>
        /// 通过ID获取Window
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Window GetWindow(string id)
        {
            return windows.GetWindow(id);
        }

        /// <summary>
        /// 关闭查找到的第一个窗口
        /// </summary>
        /// <param name="windowType"></param>
        public static void CloseTypeWindow(Type windowType)
        {
            windows.CloseTypeWindow(windowType);
        }


        /// <summary>
        /// 关闭所有窗口
        /// </summary>
        public static void CloseAllWindow()
        {
            windows.CloseAllWindow();
        }
    }
}

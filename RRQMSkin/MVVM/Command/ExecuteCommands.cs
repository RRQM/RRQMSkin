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
using System.Windows.Input;

namespace RRQMSkin.MVVM
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="parameter"></param>
    public delegate void DelegateCommand<T>(T parameter);

    /// <summary>
    /// 绑定有参数命令
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ExecuteCommand<T> : ICommand
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        public ExecuteCommand(DelegateCommand<T> command)
        {
            this.delCommand = command;
        }

        DelegateCommand<T> delCommand;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {

            if (this.delCommand == null)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            T t = (T)parameter;
            if (this.CanExecute(parameter))
            {
                CanExecuteChanged?.Invoke(this, null);
                this.delCommand.Invoke(t);
            }
        }
    }
}

using RRQMSkin.MVVM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RRQMSkinDemo.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            List<IEventAction> actionInfos = new List<IEventAction>();
            actionInfos.Add(new EventAction<object, MouseButtonEventArgs>("MouseDown", this.Test));
            actionInfos.Add(new EventAction<object, MouseButtonEventArgs>("MouseUp", this.Test));
            this.testEvents = actionInfos;
        }

        

        private int t;

        public int T { get => t; set => SetProperty(ref t, value); }

        #region 变量（Variable）

        #endregion

        #region 命令（Command）

        #endregion

        #region 属性（Attribute）
        private IEnumerable<IEventAction> testEvents;

        public IEnumerable<IEventAction> TestEvents
        {
            get { return testEvents; }
            set { testEvents = value; }
        }
        #endregion

        #region 公共方法（publicMethod）

        #endregion

        #region 私有方法（PrivateMethod）
        private void Test(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine(1);
        }
        #endregion

        #region 事件方法（EventMethod）

        #endregion
    }
}

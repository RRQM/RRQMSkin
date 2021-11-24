using RRQMSkin.MVVM;
using RRQMSkinDemo.View;
using RRQMSkinDemo.ViewModel.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RRQMSkinDemo.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            List<IEventAction> actionInfos = new List<IEventAction>();
            actionInfos.Add(new EventAction<object, RoutedEventArgs>("Loaded", Loaded));
            Left_Navigation = new ObservableCollection<UIControlsModel>();
            this.testEvents = actionInfos;

        }

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

        private ObservableCollection<UIControlsModel> left_navigation;
        public ObservableCollection<UIControlsModel> Left_Navigation
        {
            get => left_navigation;
            set => SetProperty(ref left_navigation, value);
        }

        #endregion

        #region 公共方法（publicMethod）

        #endregion

        #region 私有方法（PrivateMethod）
        private void Loaded(object sender, RoutedEventArgs e)
        {
            Left_Navigation.Add(new UIControlsModel
            {
                Title = $"Emoji解析",
                Child = new UserChat(),
                ToolTipText = $"Emoji弹框与Emoji图文混排"
            });
            for (int i = 0; i < 30; i++)
            {
                Left_Navigation.Add(new UIControlsModel
                {
                    Title = $"测试{i + 1}",
                    Child = new Border(),
                    ToolTipText = $"提示:{i + 1}"
                }) ;
            }
        }
        #endregion

        #region 事件方法（EventMethod）

        #endregion
    }
}

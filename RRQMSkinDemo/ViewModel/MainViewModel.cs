using RRQMSkin.MVVM;
using RRQMSkinDemo.View;
using RRQMSkinDemo.ViewModel.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace RRQMSkinDemo.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            List<IEventAction> actionInfos = new List<IEventAction>();
            actionInfos.Add(new EventAction<object, RoutedEventArgs>("Loaded", this.Loaded));
            this.Left_Navigation = new ObservableCollection<UIControlsModel>();
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
            get { return this.testEvents; }
            set { this.testEvents = value; }
        }

        private ObservableCollection<UIControlsModel> left_navigation;
        public ObservableCollection<UIControlsModel> Left_Navigation
        {
            get => this.left_navigation;
            set => this.SetProperty(ref this.left_navigation, value);
        }

        #endregion

        #region 公共方法（publicMethod）

        #endregion

        #region 私有方法（PrivateMethod）
        private void Loaded(object sender, RoutedEventArgs e)
        {
            this.Left_Navigation.Add(new UIControlsModel
            {
                Title = $"Emoji解析",
                Child = new UserChat(),
                ToolTipText = $"Emoji弹框与Emoji图文混排"
            });
            for (int i = 0; i < 30; i++)
            {
                this.Left_Navigation.Add(new UIControlsModel
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

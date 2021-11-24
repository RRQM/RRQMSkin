using RRQM.Emoji;
using RRQMSkin.MVVM;
using RRQMSkinDemo.Control.AnimationScrollViewer;
using RRQMSkinDemo.ViewModel.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace RRQMSkinDemo.ViewModel
{
    public class UserChatViewModel : ViewModelBase
    {
        public UserChatViewModel() 
        {
            List<IEventAction> actionInfos = new List<IEventAction>();
            actionInfos.Add(new EventAction<object, RoutedEventArgs>("Loaded", Loaded));
            MessBox = new ObservableCollection<string>();
            this.testEvents = actionInfos;
        }


        private bool emoji_Popup;

        public bool Emoji_Popup
        {
            get { return emoji_Popup; }
            set { SetProperty(ref emoji_Popup, value); }
        }


        private IEnumerable<IEventAction> testEvents;

        public IEnumerable<IEventAction> TestEvents
        {
            get { return testEvents; }
            set { testEvents = value; }
        }

        private ObservableCollection<string> messBox;
        public ObservableCollection<string> MessBox
        {
            get => messBox;
            set => SetProperty(ref messBox, value);
        }


        ScrollViewerAnimation Scroll;
        private void Loaded(object sender, RoutedEventArgs e)
        {
            MessBox.Add($"[doge]RRQMSkin");
            MessBox.Add($"RRQMSkin[doge]");
            MessBox.Add($"[doge]若汝棋茗");
            MessBox.Add($"RRQMSkin[doge]");
            MessBox.Add($"[doge]测试");
            MessBox.Add($"[doge][doge][doge][doge][doge]");
            MessBox.Add($"RRQMSkin[doge]RRQMSkin[doge]RRQMSkin[doge]");
            var temp = (RRQMSkinDemo.View.UserChat)sender;
            Scroll = temp.Scroll;
        }

        
        public ICommand EmojiClick =>
            new ExecuteCommand<object>((p) => 
            {
                var temp = p as EmojiALL;
                if (temp != null)
                {
                    var temp1 = temp.DataContext as EmojiALLViewModel;
                    if (temp1.EmojiList.Count > 1)
                    {
                        var temoEmoji = temp1.EmojiList[0];
                        temp1.EmojiList = new ObservableCollection<ImageList>();
                        temp1.EmojiList.Add(temoEmoji);
                    }
                    Emoji_Popup = true;
                }
            });

        public ICommand AddClick =>
            new ExecuteCommand<object>((p) =>
            {
                var temp = p as FlowDocument;
                if (temp != null)
                {
                    var str = share.GetSendMessage(temp);
                    if (!string.IsNullOrEmpty(str))
                    {
                        MessBox.Add(str);
                        Scroll?.GoToEnd();
                        temp.Blocks.Clear();
                    }
                }
            });


    }
}

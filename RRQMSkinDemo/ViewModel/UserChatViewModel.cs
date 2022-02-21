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
            actionInfos.Add(new EventAction<object, RoutedEventArgs>("Loaded", this.Loaded));
            this.MessBox = new ObservableCollection<string>();
            this.testEvents = actionInfos;
        }


        private bool emoji_Popup;

        public bool Emoji_Popup
        {
            get { return this.emoji_Popup; }
            set { this.SetProperty(ref this.emoji_Popup, value); }
        }


        private IEnumerable<IEventAction> testEvents;

        public IEnumerable<IEventAction> TestEvents
        {
            get { return this.testEvents; }
            set { this.testEvents = value; }
        }

        private ObservableCollection<string> messBox;
        public ObservableCollection<string> MessBox
        {
            get => this.messBox;
            set => this.SetProperty(ref this.messBox, value);
        }


        ScrollViewerAnimation Scroll;
        private void Loaded(object sender, RoutedEventArgs e)
        {
            this.MessBox.Add($"[doge]RRQMSkin");
            this.MessBox.Add($"RRQMSkin[doge]");
            this.MessBox.Add($"[doge]若汝棋茗");
            this.MessBox.Add($"RRQMSkin[doge]");
            this.MessBox.Add($"[doge]测试");
            this.MessBox.Add($"[doge][doge][doge][doge][doge]");
            this.MessBox.Add($"RRQMSkin[doge]RRQMSkin[doge]RRQMSkin[doge]");
            var temp = (RRQMSkinDemo.View.UserChat)sender;
            this.Scroll = temp.Scroll;
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
                    this.Emoji_Popup = true;
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
                        this.MessBox.Add(str);
                        this.Scroll?.GoToEnd();
                        temp.Blocks.Clear();
                    }
                }
            });


    }
}

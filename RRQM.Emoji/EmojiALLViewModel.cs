using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RRQM.Emoji
{
    public class EmojiALLViewModel : INotifyPropertyChanged
    {
        public ulong forwardScene { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;


        private bool _PNGCheck;
        public bool PNGCheck
        {
            get
            {
                return _PNGCheck;
            }
            set
            {
                _PNGCheck = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PNGCheck"));
            }
        }

        private ObservableCollection<ImageList> _EmojiList;
        public ObservableCollection<ImageList> EmojiList
        {
            get
            {
                return _EmojiList;
            }
            set
            {
                _EmojiList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("EmojiList"));
            }
        }
        public ICommand CheckEmoji =>
          new DelegateCommand((p) => 
          {
          
          });

        private ObservableCollection<Emoji> _EmojiArray;
        public ObservableCollection<Emoji> EmojiArray
        {
            get
            {
                return _EmojiArray;
            }
            set
            {
                _EmojiArray = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("EmojiArray"));
            }
        }

        private ObservableCollection<ImageList> _PNGEmojiArray;
        public ObservableCollection<ImageList> PNGEmojiArray
        {
            get
            {
                return _PNGEmojiArray;
            }
            set
            {
                _PNGEmojiArray = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PNGEmojiArray"));
            }
        }

        private bool OnLoadInit = false;

        public void Init() 
        {
            if (!OnLoadInit)
            {
                OnLoadInit = true;
                try
                {
                    var Emoji_temp = EmojiLoadingGIF.AnayXML("Emoji");
                    foreach (var item in Emoji_temp)
                    {
                        item.CheckArray = (data) =>
                        {
                            EmojiArray = new ObservableCollection<Emoji>(data.EmojiArray);
                            PNGCheck = false;
                        };
                        if (item == Emoji_temp[0])
                        {
                            item.Check = true;
                        }
                    }
                    EmojiList = new ObservableCollection<ImageList>(Emoji_temp);
                }
                catch (Exception ex)
                {
                    OnLoadInit = false;
                    throw ex;
                }
            }
        }
        

    }
}

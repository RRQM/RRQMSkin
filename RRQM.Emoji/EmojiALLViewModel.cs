using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
                return this._PNGCheck;
            }
            set
            {
                this._PNGCheck = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PNGCheck"));
            }
        }

        private ObservableCollection<ImageList> _EmojiList;
        public ObservableCollection<ImageList> EmojiList
        {
            get
            {
                return this._EmojiList;
            }
            set
            {
                this._EmojiList = value;
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
                return this._EmojiArray;
            }
            set
            {
                this._EmojiArray = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("EmojiArray"));
            }
        }

        private ObservableCollection<ImageList> _PNGEmojiArray;
        public ObservableCollection<ImageList> PNGEmojiArray
        {
            get
            {
                return this._PNGEmojiArray;
            }
            set
            {
                this._PNGEmojiArray = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PNGEmojiArray"));
            }
        }

        private bool OnLoadInit = false;

        public void Init() 
        {
            if (!this.OnLoadInit)
            {
                this.OnLoadInit = true;
                try
                {
                    var Emoji_temp = EmojiLoadingGIF.AnayXML("Emoji");
                    foreach (var item in Emoji_temp)
                    {
                        item.CheckArray = (data) =>
                        {
                            this.EmojiArray = new ObservableCollection<Emoji>(data.EmojiArray);
                            this.PNGCheck = false;
                        };
                        if (item == Emoji_temp[0])
                        {
                            item.Check = true;
                        }
                    }
                    this.EmojiList = new ObservableCollection<ImageList>(Emoji_temp);
                }
                catch (Exception ex)
                {
                    this.OnLoadInit = false;
                    throw ex;
                }
            }
        }
        

    }
}

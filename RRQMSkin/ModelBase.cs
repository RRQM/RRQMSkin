using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RRQMSkin
{
    public class ModelBase : INotifyPropertyChanged
    {
        /// <summary>
        ///
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 属性改变时
        /// </summary>
        /// <param name="propertyName"></param>
        public void OnPropertyChanged([CallerMemberName] string propertyName = "none passed")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
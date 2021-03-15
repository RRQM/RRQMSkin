//------------------------------------------------------------------------------
//  此代码版权归作者本人若汝棋茗所有
//  源代码使用协议遵循本仓库的开源协议，若本仓库没有设置，则按MIT开源协议授权
//  CSDN博客：https://blog.csdn.net/qq_40374647
//  哔哩哔哩视频：https://space.bilibili.com/94253567
//  源代码仓库：https://gitee.com/RRQM_Home
//  交流QQ群：234762506
//  感谢您的下载和使用
//------------------------------------------------------------------------------
//------------------------------------------------------------------------------
using System.Windows.Input;
using RRQMMVVM;

namespace RRQMSkin.Controls
{
    public class XmlKeyAndValue : ObservableObject
    {
        public XmlKeyAndValue()
        {
            this.RemoveCommand = new ExecuteCommand(Remove);
        }

        public event XmlEdit XmlEdited;

        public XmlTreeView Parent { get; set; }

        public ExecuteCommand RemoveCommand { get; set; }

        private string key;

        public string Key
        {
            get { return key; }
            set { key = value; OnPropertyChanged(); }
        }

        private string keyValue;

        public string Value
        {
            get { return keyValue; }
            set
            {
                if (this.Parent != null)
                {
                    XmlChangedEventArgs args = new XmlChangedEventArgs();
                    args.XmlChangedType = XmlDataType.Attribute;
                    args.OperationType = OperationType.Updata;
                    args.ChangedObject = this;
                    args.ParentName = this.Parent.Header.ToString();
                    args.OldValue = keyValue;
                    args.NewValue = value;
                    XmlEdited?.Invoke(this, args);
                }

                keyValue = value;
                OnPropertyChanged();
            }
        }

        private bool isEditable;

        public bool IsEditable
        {
            get { return isEditable; }
            set { isEditable = value; OnPropertyChanged(); }
        }

        private void Remove()
        {
            if (this.Parent != null)
            {
                Parent.RemoveAttribute(this, true);
            }
        }

        public override string ToString()
        {
            return string.Format("{0}：{1}", key, Value);
        }
    }
}
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
using System.Collections.ObjectModel;

namespace RRQMSkin.Controls
{
    internal class XmlChild : RRQMMVVM.ObservableObject
    {
        private bool expanderVisibility;

        public bool ExpanderVisibility
        {
            get { return expanderVisibility; }
            set { expanderVisibility = value; OnPropertyChanged(); }
        }

        private ObservableCollection<XmlKeyAndValue> attributeCollection;

        public ObservableCollection<XmlKeyAndValue> AttributeCollection
        {
            get { return attributeCollection; }
            set { attributeCollection = value; }
        }

        public void AddAttribute(XmlKeyAndValue keyAndValue, bool isTrigger)
        {
            //keyAndValue.XmlEdited += XmlEdited;
            //keyAndValue.Parent = this;
            //AttributeCollection.Add(keyAndValue);
            //if (isTrigger)
            //{
            //    XmlChangedEventArgs args = new XmlChangedEventArgs();
            //    args.XmlChangedType = XmlDataType.Attribute;
            //    args.OperationType = OperationType.Add;
            //    args.ParentName = this.Header.ToString();
            //    args.ChangedObject = keyAndValue;
            //    XmlEdited?.Invoke(this, args);
            //}
        }
    }
}
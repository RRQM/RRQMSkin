using System.Collections.ObjectModel;

namespace RRQMSkin.Controls
{
    internal class XmlChild : ModelBase
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
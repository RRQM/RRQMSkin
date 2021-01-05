namespace RRQMSkin.Controls
{
    public class XmlKeyAndValue : ModelBase
    {
        public XmlKeyAndValue()
        {
            this.RemoveCommand = new Command(Remove);
        }

        public event XmlEdit XmlEdited;

        public XmlTreeView Parent { get; set; }

        public Command RemoveCommand { get; set; }

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
namespace RRQMSkin.Xml
{
    public class XmlChangedEventArgs
    {
        public XmlDataType XmlChangedType { get; set; }
        public OperationType OperationType { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public string ParentName { get; set; }
        public object ChangedObject { get; set; }
    }

    public enum OperationType
    {
        Add,
        Remove,
        Updata
    }
}
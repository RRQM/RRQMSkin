using System.Xml;

namespace RRQMSkin.Xml
{
    internal class XML
    {
        internal void Open(string path)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(path);

            XmlNode rootNode = xml.DocumentElement;
            XmlChild xmlChild = new XmlChild();
            // GetNode(rootNode,xmlChild);
        }

        //private void GetNode(XmlNode rootNode, XmlChild xmlChild)
        //{
        //    XmlNodeList nodeList = rootNode.ChildNodes;
        //    foreach (XmlNode Node in nodeList)
        //    {
        //        XmlTreeView rootViewItem = new XmlTreeView();
        //        rootViewItem.XmlEdited += xmlEdited;
        //        rootViewItem.IsEditable = viewItem.IsEditable;
        //        if (Node.Attributes.Count > 0)
        //        {
        //            rootViewItem.AttributeCollection = new ObservableCollection<XmlKeyAndValue>();
        //            foreach (XmlAttribute item in Node.Attributes)
        //            {
        //                XmlKeyAndValue keyAndValue = new XmlKeyAndValue();
        //                keyAndValue.Key = item.Name;
        //                keyAndValue.Value = item.Value;
        //                rootViewItem.AddAttribute(keyAndValue, false);
        //            }
        //        }

        //        if (Node.ChildNodes.Count == 0)
        //        {
        //            rootViewItem.ExpanderVisibility = Visibility.Hidden;
        //        }
        //        else
        //        {
        //        }

        //        rootViewItem.Header = Node.Name;
        //        viewItem.Items.Add(rootViewItem);
        //        switch (Node.Name)
        //        {
        //            case "Steps":
        //                {
        //                    rootViewItem.Background = Brushes.Red;
        //                    rootViewItem.IsExpanded = true;
        //                    break;
        //                }
        //            case "Step":
        //                {
        //                    rootViewItem.Background = Brushes.GreenYellow;
        //                    break;
        //                }
        //            default:
        //                rootViewItem.Background = Brushes.AliceBlue;
        //                break;
        //        }
        //        AddNode(Node, rootViewItem, xmlEdited);
        //}
        //}
    }
}
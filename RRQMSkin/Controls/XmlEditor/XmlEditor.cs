using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml;

namespace RRQMSkin.Controls
{
    public delegate void XmlEdit(object sender, XmlChangedEventArgs e);

    public class XmlEditor : TreeView
    {
        // Using a DependencyProperty as the backing store for IsEditable.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsEditableProperty =
            DependencyProperty.Register("IsEditable", typeof(bool), typeof(XmlEditor), new PropertyMetadata(true, OnEditableChanged));

        static XmlEditor()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(XmlEditor), new FrameworkPropertyMetadata(typeof(XmlEditor)));
        }

        public event XmlEdit XmlEdited;

        public bool IsEditable
        {
            get { return (bool)GetValue(IsEditableProperty); }
            set { SetValue(IsEditableProperty, value); }
        }

        public string ConvertXmlToString(XmlDocument xmlDoc)
        {
            MemoryStream stream = new MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(stream, null);
            writer.Formatting = Formatting.Indented;
            xmlDoc.Save(writer);
            StreamReader sr = new StreamReader(stream, System.Text.Encoding.UTF8);
            stream.Position = 0;
            string xmlString = sr.ReadToEnd();
            sr.Close();
            stream.Close();
            return xmlString;
        }

        public void Copy()
        {
            XmlTreeView selectItem = (XmlTreeView)this.SelectedItem;

            string xml = "{Recipe}" + GetNodeXml(selectItem);

            Clipboard.SetDataObject(xml);
        }

        public string GetNodeXml(XmlTreeView root)
        {
            XmlDocument xml = new XmlDocument();

            XmlElement node = xml.CreateElement(root.Header.ToString());
            xml.AppendChild(node);

            SearchNode(xml, node, root);

            return ConvertXmlToString(xml);
        }

        public void PasteChild()
        {
            if (this.IsEditable)
            {
                if (this.SelectedItem != null)
                {
                    XmlTreeView selectItem = (XmlTreeView)this.SelectedItem;

                    string xml = Clipboard.GetText();
                    if (xml.IndexOf("{Recipe}") == 0)
                    {
                        PasteNodeChild(selectItem, xml.Replace("{Recipe}", ""));
                    }
                }
                else if (this.Items.Count == 0)
                {
                    string xml = Clipboard.GetText();
                    if (xml.IndexOf("{Recipe}") == 0)
                    {
                        PasteNodeChild(this, xml.Replace("{Recipe}", ""));
                    }
                }
            }
        }

        public void PasteThis()
        {
            if (IsEditable)
            {
                if (this.SelectedItem != null)
                {
                    XmlTreeView selectItem = (XmlTreeView)this.SelectedItem;

                    string xml = Clipboard.GetText();
                    if (xml.IndexOf("{Recipe}") == 0)
                    {
                        PasteNodeThis(selectItem, xml.Replace("{Recipe}", ""));
                    }
                }
                else if (this.Items.Count == 0)
                {
                    string xml = Clipboard.GetText();
                    if (xml.IndexOf("{Recipe}") == 0)
                    {
                        PasteNodeThis(this, xml.Replace("{Recipe}", ""));
                    }
                }
            }
        }

        public void Save(string path)
        {
            XmlDocument xml = new XmlDocument();
            XmlDeclaration dec = xml.CreateXmlDeclaration("1.0", "utf-8", null);
            XmlNode titleNode = xml.AppendChild(dec);

            XmlElement node = xml.CreateElement(((XmlTreeView)this.Items[0]).Header.ToString());
            xml.AppendChild(node);

            SaveNode(xml, node, (XmlTreeView)this.Items[0]);

            xml.Save(path);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyboardDevice.Modifiers == ModifierKeys.Control && e.Key == Key.C)
            {
                Copy();
            }

            if (e.KeyboardDevice.Modifiers == ModifierKeys.Control && e.Key == Key.V)
            {
                PasteChild();
            }
        }

        private static void AddNode(XmlNode xmlNode, XmlTreeView viewItem, XmlEdit xmlEdited)
        {
            XmlNodeList nodeList = xmlNode.ChildNodes;
            foreach (XmlNode Node in nodeList)
            {
                XmlTreeView rootViewItem = new XmlTreeView();
                rootViewItem.XmlEdited += xmlEdited;
                rootViewItem.IsEditable = viewItem.IsEditable;
                if (Node.Attributes.Count > 0)
                {
                    rootViewItem.AttributeCollection = new ObservableCollection<XmlKeyAndValue>();
                    foreach (XmlAttribute item in Node.Attributes)
                    {
                        XmlKeyAndValue keyAndValue = new XmlKeyAndValue();
                        keyAndValue.Key = item.Name;
                        keyAndValue.Value = item.Value;
                        rootViewItem.AddAttribute(keyAndValue, false);
                    }
                }

                if (Node.ChildNodes.Count == 0)
                {
                    rootViewItem.ExpanderVisibility = Visibility.Hidden;
                }
                else
                {
                }

                rootViewItem.Header = Node.Name;
                viewItem.Items.Add(rootViewItem);
                switch (Node.Name)
                {
                    case "Steps":
                        {
                            rootViewItem.Background = Brushes.Red;
                            rootViewItem.IsExpanded = true;
                            break;
                        }
                    case "Step":
                        {
                            rootViewItem.Background = Brushes.GreenYellow;
                            break;
                        }
                    default:
                        rootViewItem.Background = Brushes.AliceBlue;
                        break;
                }
                AddNode(Node, rootViewItem, xmlEdited);
            }
        }

        private static void OnEditableChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            XmlEditor recipeXmlEdit = (XmlEditor)d;

            foreach (ItemsControl item in recipeXmlEdit.Items)
            {
                ((XmlTreeView)item).IsEditable = recipeXmlEdit.IsEditable;
                SetEditable((XmlTreeView)item, recipeXmlEdit.IsEditable);
            }
        }

        private static void SetEditable(XmlTreeView treeViewGroup, bool isEditable)
        {
            foreach (XmlTreeView item in treeViewGroup.Items)
            {
                item.IsEditable = isEditable;
                if (item.AttributeCollection != null)
                {
                    foreach (XmlKeyAndValue keyAndValue in item.AttributeCollection)
                    {
                        keyAndValue.IsEditable = isEditable;
                    }
                }

                if (item.Items.Count > 0)
                {
                    SetEditable(item, isEditable);
                }
            }
        }

        private void Open()
        {
            //XmlDocument xml = new XmlDocument();
            //xml.Load(recipeXmlEdit.XmlPath);

            //XmlNode rootNode = xml.DocumentElement;
            //XmlTreeView rootViewItem = new XmlTreeView();
            //rootViewItem.XmlEdited += recipeXmlEdit.XmlEdited;

            //rootViewItem.IsEditable = false;
            //rootViewItem.Background = Brushes.Violet;
            //rootViewItem.Header = rootNode.Name;
            //recipeXmlEdit.AddChild(rootViewItem);

            //AddNode(rootNode, rootViewItem, recipeXmlEdit.XmlEdited);
        }

        private void PasteNodeChild(ItemsControl root, string xmlString)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(new MemoryStream(Encoding.UTF8.GetBytes(xmlString)));

            XmlNode rootNode = xml.DocumentElement;

            XmlTreeView rootViewItem = new XmlTreeView();
            rootViewItem.XmlEdited += this.XmlEdited;

            rootViewItem.IsEditable = true;
            rootViewItem.Background = Brushes.Violet;
            rootViewItem.Header = rootNode.Name;

            if (rootNode.ChildNodes.Count > 0)
            {
                rootViewItem.ExpanderVisibility = Visibility.Visible;
            }
            if (root is XmlTreeView)
            {
                ((XmlTreeView)root).AddNode(rootViewItem, true);
            }
            else
            {
                XmlChangedEventArgs args = new XmlChangedEventArgs();
                args.XmlChangedType = XmlDataType.Node;
                args.OperationType = OperationType.Add;
                args.ParentName = "Null";
                args.ChangedObject = rootViewItem;
                XmlEdited?.Invoke(this, args);
                root.Items.Add(rootViewItem);
            }

            AddNode(rootNode, rootViewItem, this.XmlEdited);
        }

        private void PasteNodeThis(ItemsControl root, string xmlString)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(new MemoryStream(Encoding.UTF8.GetBytes(xmlString)));

            XmlNode rootNode = xml.DocumentElement;

            XmlTreeView rootViewItem = new XmlTreeView();
            rootViewItem.XmlEdited += this.XmlEdited;

            rootViewItem.IsEditable = true;
            rootViewItem.Background = Brushes.Violet;
            rootViewItem.Header = rootNode.Name;

            if (rootNode.ChildNodes.Count > 0)
            {
                rootViewItem.ExpanderVisibility = Visibility.Visible;
            }
            if (root.Parent is XmlTreeView)
            {
                ((XmlTreeView)root.Parent).AddNode(rootViewItem, true);
            }
            else if (root is XmlEditor)
            {
                XmlChangedEventArgs args = new XmlChangedEventArgs();
                args.XmlChangedType = XmlDataType.Node;
                args.OperationType = OperationType.Add;
                args.ParentName = "Null";
                args.ChangedObject = rootViewItem;
                XmlEdited?.Invoke(this, args);
                root.Items.Add(rootViewItem);
            }

            AddNode(rootNode, rootViewItem, this.XmlEdited);
        }

        private void SaveNode(XmlDocument xml, XmlNode xmlNode, XmlTreeView viewGroup)
        {
            foreach (XmlTreeView item in viewGroup.Items)
            {
                XmlElement node = xml.CreateElement(item.Header.ToString());
                if (item.AttributeCollection != null)
                {
                    foreach (XmlKeyAndValue attribute in item.AttributeCollection)
                    {
                        node.SetAttribute(attribute.Key, attribute.Value);
                    }
                }
                xmlNode.AppendChild(node);
                if (item.Items.Count > 0)
                {
                    SaveNode(xml, node, item);
                }
            }
        }

        private void SearchNode(XmlDocument xml, XmlNode xmlNode, XmlTreeView viewGroup)
        {
            foreach (XmlTreeView item in viewGroup.Items)
            {
                XmlElement node = xml.CreateElement(item.Header.ToString());
                if (item.AttributeCollection != null)
                {
                    foreach (XmlKeyAndValue attribute in item.AttributeCollection)
                    {
                        node.SetAttribute(attribute.Key, attribute.Value);
                    }
                }
                xmlNode.AppendChild(node);
                if (item.Items.Count > 0)
                {
                    SearchNode(xml, node, item);
                }
            }
        }
    }
}
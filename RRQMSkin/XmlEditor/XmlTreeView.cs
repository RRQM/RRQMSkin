using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace RRQMSkin.Xml
{
    public class XmlTreeView : TreeViewItem
    {
        static XmlTreeView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(XmlTreeView), new FrameworkPropertyMetadata(typeof(XmlTreeView)));
        }

        public XmlTreeView()
        {
            this.XmlEdited += RecipeTreeViewGroup_XmlEdited;
            this.RemoveNodeCommand = new Command(RemoveNode);
            this.AddCommand = new Command(Add);
        }

        protected override void OnExpanded(RoutedEventArgs e)
        {
            base.OnExpanded(e);
            if (this.Header.ToString() == "Step")
            {
                this.IsExpanded = true;
                SearchItems(this);
            }
        }

        private void SearchItems(XmlTreeView treeViewGroup)
        {
            foreach (XmlTreeView item in treeViewGroup.Items)
            {
                item.IsExpanded = true;
                SearchItems(item);
            }
        }

        private void RecipeTreeViewGroup_XmlEdited(object sender, XmlChangedEventArgs e)
        {
            if (e.XmlChangedType == XmlDataType.Attribute && e.OperationType == OperationType.Remove)
            {
                RemoveAttribute((XmlKeyAndValue)e.ChangedObject, true);
            }
        }

        public bool IsEditable
        {
            get { return (bool)GetValue(IsEditableProperty); }
            set { SetValue(IsEditableProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsEditable.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsEditableProperty =
            DependencyProperty.Register("IsEditable", typeof(bool), typeof(XmlTreeView), new PropertyMetadata(false));

        private Border border;
        private Button addButton;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            border = (Border)Template.FindName("Bd", this);
            addButton = (Button)Template.FindName("addButton", this);
            border.MouseEnter += Border_MouseEnter;
            border.MouseLeave += Border_MouseLeave;
        }

        private void Border_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!this.IsEditable)
            {
                return;
            }
            DoubleAnimation animation = new DoubleAnimation();
            animation.From = 1;
            animation.To = 0;
            animation.Duration = TimeSpan.FromSeconds(0.6);
            this.addButton.BeginAnimation(Button.OpacityProperty, animation);
        }

        private void Border_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!this.IsEditable)
            {
                return;
            }
            DoubleAnimation animation = new DoubleAnimation();
            animation.From = 0;
            animation.To = 1;
            animation.Duration = TimeSpan.FromSeconds(0.6);
            this.addButton.BeginAnimation(Button.OpacityProperty, animation);
        }

        public Visibility ExpanderVisibility
        {
            get { return (Visibility)GetValue(ExpanderVisibilityProperty); }
            set { SetValue(ExpanderVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ExpanderVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ExpanderVisibilityProperty =
            DependencyProperty.Register("ExpanderVisibility", typeof(Visibility), typeof(XmlTreeView), new PropertyMetadata(Visibility.Visible));

        public ObservableCollection<XmlKeyAndValue> AttributeCollection
        {
            get { return (ObservableCollection<XmlKeyAndValue>)GetValue(AttributeCollectionProperty); }
            set { SetValue(AttributeCollectionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AttributeCollectionProperty =
            DependencyProperty.Register("AttributeCollection", typeof(ObservableCollection<XmlKeyAndValue>), typeof(XmlTreeView), new PropertyMetadata(null));

        public void AddAttribute(XmlKeyAndValue keyAndValue, bool isTrigger)
        {
            keyAndValue.XmlEdited += XmlEdited;
            keyAndValue.Parent = this;
            AttributeCollection.Add(keyAndValue);
            if (isTrigger)
            {
                XmlChangedEventArgs args = new XmlChangedEventArgs();
                args.XmlChangedType = XmlDataType.Attribute;
                args.OperationType = OperationType.Add;
                args.ParentName = this.Header.ToString();
                args.ChangedObject = keyAndValue;
                XmlEdited?.Invoke(this, args);
            }
        }

        public void AddNode(XmlTreeView recipeTreeView, bool isTrigger)
        {
            recipeTreeView.XmlEdited += XmlEdited;
            this.Items.Add(recipeTreeView);
            this.ExpanderVisibility = Visibility.Visible;
            if (isTrigger)
            {
                XmlChangedEventArgs args = new XmlChangedEventArgs();
                args.XmlChangedType = XmlDataType.Node;
                args.OperationType = OperationType.Add;
                args.ParentName = this.Header.ToString();
                args.ChangedObject = recipeTreeView;
                XmlEdited?.Invoke(this, args);
            }
        }

        public void RemoveAttribute(XmlKeyAndValue keyAndValue, bool isTrigger)
        {
            if (AttributeCollection.Contains(keyAndValue))
            {
                keyAndValue.XmlEdited -= XmlEdited;
                AttributeCollection.Remove(keyAndValue);
                if (isTrigger)
                {
                    XmlChangedEventArgs args = new XmlChangedEventArgs();
                    args.ParentName = this.Header.ToString();
                    args.XmlChangedType = XmlDataType.Attribute;
                    args.OperationType = OperationType.Remove;
                    args.ChangedObject = keyAndValue;
                    XmlEdited?.Invoke(this, args);
                }
            }
        }

        public Command RemoveNodeCommand { get; set; }
        public Command AddCommand { get; set; }

        public event XmlEdit XmlEdited;

        private void RemoveNode()
        {
            if (((ItemsControl)this.Parent).Items.Contains(this))
            {
                ((ItemsControl)this.Parent).Items.Remove(this);

                XmlChangedEventArgs args = new XmlChangedEventArgs();
                args.XmlChangedType = XmlDataType.Node;
                args.OperationType = OperationType.Remove;
                args.ChangedObject = this;
                XmlEdited?.Invoke(this, args);
            }
        }

        private void Add()
        {
            //AddNodeOrAttributeWindow nodeOrAttributeWindow = new AddNodeOrAttributeWindow(this);
            //nodeOrAttributeWindow.ShowDialog();
        }
    }
}
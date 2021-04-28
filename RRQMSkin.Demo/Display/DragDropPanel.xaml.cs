using RRQMSkin.DragDrop;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace RRQMSkin.Demo.Display
{
    /// <summary>
    /// DragDropListBox.xaml 的交互逻辑
    /// </summary>
    public partial class DragDropPanel : Border
    {
        public DragDropPanel()
        {
            InitializeComponent();
            for (int i = 0; i < 10; i++)
            {
                this.listBox_1.Items.Add("listBox_1_Item" + i);
            }

            for (int i = 0; i < 5; i++)
            {
                this.listBox_2.Items.Add("listBox_2_Item" + i);
            }
        }

        private AdornerLayer adornerLayer = null;

        private void ListBox_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            ListBox mListBox = (ListBox)sender;
            if (Mouse.LeftButton != MouseButtonState.Pressed)
                return;

            Point pos = e.GetPosition(mListBox);
            HitTestResult result = VisualTreeHelper.HitTest(mListBox, pos);
            if (result == null)
                return;

            ListBoxItem selectedItem = Utils.FindVisualParent<ListBoxItem>(result.VisualHit);
            if (selectedItem == null)
            {
                return;
            }
            selectedItem.IsSelected = true;

            DragDropAdorner adorner = new DragDropAdorner(mListBox);
            adorner.Opacity = 0.7;
            adorner.VerticalOffset = 20;
            adorner.HorizontalOffset = 20;

            List<FrameworkElement> frameworks = new List<FrameworkElement>();
            List<string> templateItems = new List<string>();
            foreach (string item in mListBox.SelectedItems)
            {
                int index = mListBox.Items.IndexOf(item);
                var listBoxItem = mListBox.ItemContainerGenerator.ContainerFromIndex(index) as FrameworkElement;
                frameworks.Add(listBoxItem);
                templateItems.Add(item);
            }

            adorner.DraggedElements = frameworks.ToArray();

            adornerLayer = AdornerLayer.GetAdornerLayer(this);
            adornerLayer.Add(adorner);

            DataObject dataObject = new DataObject(templateItems);

           System.Windows.DragDrop.DoDragDrop(mListBox, dataObject, DragDropEffects.Move);

            adornerLayer.Remove(adorner);
            adornerLayer = null;
        }

        private void ListBox_QueryContinueDrag(object sender, QueryContinueDragEventArgs e)
        {
            adornerLayer.Update();
        }

        private void ListBox2_Drop(object sender, DragEventArgs e)
        {
            List<string> dataItem = e.Data.GetData(typeof(List<string>)) as List<string>;
            foreach (string item in dataItem)
            {
                this.listBox_2.Items.Add(item);
                this.listBox_1.Items.Remove(item);
            }
        }
    }
}
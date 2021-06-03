//------------------------------------------------------------------------------
//  此代码版权归作者本人若汝棋茗所有
//  源代码使用协议遵循本仓库的开源协议及附加协议，若本仓库没有设置，则按MIT开源协议授权
//  CSDN博客：https://blog.csdn.net/qq_40374647
//  哔哩哔哩视频：https://space.bilibili.com/94253567
//  源代码仓库：https://gitee.com/RRQM_Home
//  交流QQ群：234762506
//  本仓库其他贡献者：
//  Mr:牧影：https://gitee.com/fuck666
//  火狼：https://gitee.com/wudiliang
//  感谢您的下载和使用
//------------------------------------------------------------------------------
//------------------------------------------------------------------------------
using RRQMSkin.DragDrop;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace RRQMSkinDemo.Display
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
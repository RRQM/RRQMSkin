using System.Windows.Controls;

namespace RRQMSkin
{
    public class RRQMTabItem : TabItem
    {
        public RRQMTabItem()
        {
            this.CloseItemCommand = new Command(CloseItem);
        }

        public Command CloseItemCommand { get; set; }

        private void CloseItem()
        {
            if (this.Parent is RRQMTabControl)
            {
                RRQMTabControl tabControl = (RRQMTabControl)this.Parent;
                tabControl.Items.Remove(this);
            }
        }
    }
}
using System.Windows;
using System.Windows.Controls.Primitives;

namespace RRQMSkin.Controls
{

    public class PathPopup : Popup
    {
        static PathPopup()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PathPopup), new FrameworkPropertyMetadata(typeof(PathPopup)));
        }
    }
}

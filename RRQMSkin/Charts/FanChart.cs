using System.Windows;
using System.Windows.Controls;

namespace RRQMSkin
{
    /// <summary>
    /// 扇形图
    /// </summary>
    public class FanChart : Control
    {
        static FanChart()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FanChart), new FrameworkPropertyMetadata(typeof(FanChart)));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRQMSkin.Windows
{
    public enum RRQMResizeMode
    {
        NoResize,
        CanResize,
    }

    public enum RRQMWindowStyle
    {
        SingleBorderWindow,
        ToolWindow,
    }

    internal enum ResizeDirection
    {
        Left = 1,
        Right = 2,
        Top = 3,
        TopLeft = 4,
        TopRight = 5,
        Bottom = 6,
        BottomLeft = 7,
        BottomRight = 8,
    }
}

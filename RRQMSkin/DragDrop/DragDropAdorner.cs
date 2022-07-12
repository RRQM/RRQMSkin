//------------------------------------------------------------------------------
//  此代码版权归作者本人若汝棋茗所有
//  源代码使用协议遵循本仓库的开源协议及附加协议，若本仓库没有设置，则按MIT开源协议授权
//  CSDN博客：https://blog.csdn.net/qq_40374647
//  哔哩哔哩视频：https://space.bilibili.com/94253567
//  源代码仓库：https://gitee.com/RRQM_Home
//  交流QQ群：234762506
//  感谢您的下载和使用
//------------------------------------------------------------------------------
//------------------------------------------------------------------------------
using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace RRQMSkin.DragDrop
{
    public class DragDropAdorner : Adorner
    {
        public DragDropAdorner(UIElement parent) : base(parent)
        {
            this.IsHitTestVisible = false; // Seems Adorner is hit test visible?
            this.mDraggedElement = parent as FrameworkElement;
        }

        public FrameworkElement[] DraggedElements { get; set; }
        public double VerticalOffset { get; set; } = 5;
        public double HorizontalOffset { get; set; } = 5;

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            if (this.DraggedElements != null)
            {
                Win32.POINT screenPos = new Win32.POINT();
                if (Win32.GetCursorPos(ref screenPos))
                {
                    Point pos = this.PointFromScreen(new Point(screenPos.X, screenPos.Y));
                    int i = 0;
                    foreach (var item in this.DraggedElements)
                    {
                        i++;
                        Rect rect = new Rect(pos.X - item.ActualWidth / 2 + i * this.HorizontalOffset, pos.Y - item.ActualHeight / 2 + i * this.VerticalOffset, item.ActualWidth, item.ActualHeight);
                        drawingContext.PushOpacity(1);
                        // Brush highlight = item.TryFindResource(SystemColors.HighlightBrushKey) as Brush;
                        Brush highlight = Brushes.Transparent;
                        if (highlight != null)
                        {
                            drawingContext.DrawRectangle(highlight, new Pen(Brushes.Transparent, 0), rect);
                            drawingContext.DrawRectangle(new VisualBrush(item), new Pen(Brushes.Transparent, 0), rect);
                            drawingContext.Pop();
                        }
                    }
                }
            }
            else if (this.mDraggedElement != null)
            {
                Win32.POINT screenPos = new Win32.POINT();
                if (Win32.GetCursorPos(ref screenPos))
                {
                    Point pos = this.PointFromScreen(new Point(screenPos.X, screenPos.Y));

                    Rect rect = new Rect(pos.X - this.mDraggedElement.ActualWidth / 2, pos.Y - this.mDraggedElement.ActualHeight / 2, this.mDraggedElement.ActualWidth, this.mDraggedElement.ActualHeight);
                    drawingContext.PushOpacity(1);
                    Brush highlight = Brushes.Transparent;
                    if (highlight != null)
                    {
                        drawingContext.DrawRectangle(highlight, new Pen(Brushes.Transparent, 0), rect);
                        drawingContext.DrawRectangle(new VisualBrush(this.mDraggedElement), new Pen(Brushes.Transparent, 0), rect);
                        drawingContext.Pop();
                    }
                }
            }
        }

        private FrameworkElement mDraggedElement = null;
    }

    public static class Win32
    {
        public struct POINT { public Int32 X; public Int32 Y; }

        // During drag-and-drop operations, the position of the mouse cannot be
        // reliably determined through GetPosition. This is because control of
        // the mouse (possibly including capture) is held by the originating
        // element of the drag until the drop is completed, with much of the
        // behavior controlled by underlying Win32 calls. As a workaround, you
        // might need to use Win32 externals such as GetCursorPos.
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(ref POINT point);
    }
}
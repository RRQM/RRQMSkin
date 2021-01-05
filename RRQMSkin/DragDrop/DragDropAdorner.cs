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
            IsHitTestVisible = false; // Seems Adorner is hit test visible?
            mDraggedElement = parent as FrameworkElement;
        }

        public FrameworkElement[] DraggedElements { get; set; }
        public double VerticalOffset { get; set; } = 5;
        public double HorizontalOffset { get; set; } = 5;

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            if (DraggedElements != null)
            {
                Win32.POINT screenPos = new Win32.POINT();
                if (Win32.GetCursorPos(ref screenPos))
                {
                    Point pos = PointFromScreen(new Point(screenPos.X, screenPos.Y));
                    int i = 0;
                    foreach (var item in DraggedElements)
                    {
                        i++;
                        Rect rect = new Rect(pos.X - item.ActualWidth / 2 + i * HorizontalOffset, pos.Y - item.ActualHeight / 2 + i * VerticalOffset, item.ActualWidth, item.ActualHeight);
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
            else if (mDraggedElement != null)
            {
                Win32.POINT screenPos = new Win32.POINT();
                if (Win32.GetCursorPos(ref screenPos))
                {
                    Point pos = PointFromScreen(new Point(screenPos.X, screenPos.Y));

                    Rect rect = new Rect(pos.X - mDraggedElement.ActualWidth / 2, pos.Y - mDraggedElement.ActualHeight / 2, mDraggedElement.ActualWidth, mDraggedElement.ActualHeight);
                    drawingContext.PushOpacity(1);
                    Brush highlight = Brushes.Transparent;
                    if (highlight != null)
                    {
                        drawingContext.DrawRectangle(highlight, new Pen(Brushes.Transparent, 0), rect);
                        drawingContext.DrawRectangle(new VisualBrush(mDraggedElement), new Pen(Brushes.Transparent, 0), rect);
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
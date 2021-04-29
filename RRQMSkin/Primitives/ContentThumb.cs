using System.Windows;
using System.Windows.Controls.Primitives;

namespace RRQMSkin.Primitives
{
    public class ContentThumb : Thumb
    {
        static ContentThumb()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ContentThumb), new FrameworkPropertyMetadata(typeof(ContentThumb)));
        }



        public object Content
        {
            get { return (object)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Content.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(object), typeof(ContentThumb), new PropertyMetadata(null));


    }
}
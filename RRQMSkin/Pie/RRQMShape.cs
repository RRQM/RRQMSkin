using System.Windows.Media;
using System.Windows.Shapes;

namespace RRQMSkin
{
    /// <summary>
    /// 若汝棋茗绘图基础类
    /// </summary>
    public abstract class RRQMShape : Shape
    {
        /// <summary>
        ///
        /// </summary>
        protected override Geometry DefiningGeometry { get { return CreatGeometry(); } }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        protected abstract Geometry CreatGeometry();
    }
}
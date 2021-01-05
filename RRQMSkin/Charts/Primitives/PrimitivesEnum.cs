using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRQMSkin.Charts.Primitives
{
    /// <summary>
    /// 圆角样式
    /// </summary>
    public enum RoundStyle
    {
        /// <summary>
        /// 无样式
        /// </summary>
        Nono,

        /// <summary>
        /// 开端
        /// </summary>
        Start,

        /// <summary>
        /// 结束
        /// </summary>
        End,

        /// <summary>
        /// 首尾都圆角
        /// </summary>
        Both
    }

    /// <summary>
    /// 文字显示样式
    /// </summary>
    public enum TextShowStyle
    {
        /// <summary>
        /// 分割类型显示
        /// </summary>
        Split,

        /// <summary>
        /// 按字符显示
        /// </summary>
        Every
    }
}

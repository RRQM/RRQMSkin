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
using RRQMSkin.Primitives;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace RRQMSkin.Controls
{
    /// <summary>
    /// 环形进度条
    /// </summary>
    public class RoundProgressBar : RangeBase
    {
        static RoundProgressBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RoundProgressBar), new FrameworkPropertyMetadata(typeof(RoundProgressBar)));
        }

        private Sector sector;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.sector = (Sector)this.Template.FindName("sector", this);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        protected override void OnValueChanged(double oldValue, double newValue)
        {
            base.OnValueChanged(oldValue, newValue);
            this.OnChanged();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="oldMaximum"></param>
        /// <param name="newMaximum"></param>
        protected override void OnMaximumChanged(double oldMaximum, double newMaximum)
        {
            base.OnMaximumChanged(oldMaximum, newMaximum);
            this.OnChanged();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="oldMinimum"></param>
        /// <param name="newMinimum"></param>
        protected override void OnMinimumChanged(double oldMinimum, double newMinimum)
        {
            base.OnMinimumChanged(oldMinimum, newMinimum);
            this.OnChanged();
        }

        private void OnChanged()
        {
            if (this.sector == null)
            {
                return;
            }
            double progress = this.Value / (this.Maximum - this.Minimum);
            this.sector.EndAngle = this.sector.StartAngle + progress * 360;
        }
    }
}
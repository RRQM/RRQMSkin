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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace RRQMSkin.Controls
{
    public class GifBox : BorderImage
    {
        /// <summary>
        /// 显示GIF动图
        /// </summary>
        private void ShowGifByAnimate(string filePath)
        {

            List<BitmapFrame> frameList = new List<BitmapFrame>();
            GifBitmapDecoder decoder = new GifBitmapDecoder(
                              new Uri(filePath, UriKind.RelativeOrAbsolute),
                              BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            if (decoder != null && decoder.Frames != null)
            {
                frameList.AddRange(decoder.Frames);
                ObjectAnimationUsingKeyFrames objKeyAnimate = new ObjectAnimationUsingKeyFrames();
                objKeyAnimate.Duration = new Duration(TimeSpan.FromSeconds(1));
                foreach (var item in frameList)
                {
                    DiscreteObjectKeyFrame k1_img1 = new DiscreteObjectKeyFrame(item);
                    objKeyAnimate.KeyFrames.Add(k1_img1);
                }
                this.Source = frameList[0];

                Storyboard board = new Storyboard();
                board.RepeatBehavior = RepeatBehavior.Forever;
                board.FillBehavior = FillBehavior.HoldEnd;
                board.Children.Add(objKeyAnimate);
                Storyboard.SetTarget(objKeyAnimate, this);
                Storyboard.SetTargetProperty(objKeyAnimate, new PropertyPath("(Image.Source)"));
                board.Begin();
            }

        }

    }
}

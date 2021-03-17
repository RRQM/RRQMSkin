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

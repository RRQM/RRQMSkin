using RRQM.Emoji;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace RRQMSkinDemo.ViewModel.Model
{
    public static class share
    {
        /// <summary>
        /// 将Document里的值都换成String
        /// </summary>
        /// <param name="fld"></param>
        /// <returns></returns>
        public static string GetSendMessage(FlowDocument fld)
        {
            if (fld == null)
            {
                return string.Empty;
            }
            string resutStr = string.Empty;
            foreach (var root in fld.Blocks)
            {
                if (root is Paragraph)
                {
                    foreach (var item in ((Paragraph)root).Inlines)
                    {
                        //如果是Emoji则进行转换
                        if (item is InlineUIContainer)
                        {
                            if (((InlineUIContainer)item).Child is System.Windows.Controls.Image)
                            {
                                System.Windows.Controls.Image img = (System.Windows.Controls.Image)((InlineUIContainer)item).Child;
                                resutStr += GetEmojiName(img.Source.ToString());
                            }

                        }
                        //如果是文本，则直接赋值
                        else if (item is Run)
                        {
                            resutStr += $"{((Run)item).Text}";
                        }
                        else if (item is LineBreak)
                        {
                            resutStr += $"\r\n";
                        }
                    }
                }

            }
            if (resutStr.Length > 4)
            {
                if (resutStr.Substring(resutStr.Length - 4, 4) == "\r\n")
                {
                    resutStr = resutStr.TrimEnd((char[])"\r\n".ToCharArray());
                }
            }
            return resutStr;
        }
        /// <summary>
        /// 获取要发送的Emoji名
        /// </summary>
        /// <param name="str">相对路径的值</param>
        /// <returns></returns>
        private static string GetEmojiName(string str)
        {
            var temp = EmojiLoadingGIF.PathGetEmojiUrl(str);
            if (temp != null)
            {
                return $"[{temp.Key}]";
            }
            return string.Empty;
        }
    }
}

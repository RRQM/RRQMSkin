using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Xml;

namespace RRQM.Emoji
{
    public static class EmojiLoadingGIF
    {
        private static List<Emoji> EmojiArrayDic = new List<Emoji>();
        /// <summary>
        /// 解析xml
        /// </summary>
        public static List<ImageList> AnayXML(string Type)
        {
            XmlDocument xmlDoc = new XmlDocument();
            Assembly _assembly = Assembly.GetExecutingAssembly();
            Stream _stream = _assembly.GetManifestResourceStream($"RRQM.Emoji.{Type}.xml");//文件需为嵌入的资源
            xmlDoc.Load(_stream);
            XmlNode root = xmlDoc.SelectSingleNode("array");
            XmlNodeList nodeList = root.ChildNodes;
            List<ImageList> EmojiList = new List<ImageList>();
            //循环列表，获得相应的内容
            foreach (XmlNode xn in nodeList)
            {
                XmlElement xe = (XmlElement)xn;
                XmlNodeList subList = xe.ChildNodes;
                foreach (XmlNode xmlNode in subList)
                {
                    if (xmlNode.Name == "array")
                    {
                        XmlElement lastXe = (XmlElement)xmlNode;
                        List<Emoji> list = new List<Emoji>();
                        foreach (XmlNode lastNode in lastXe)
                        {
                            if (lastNode.Name == "a")
                            {
                                list.Add(new Emoji()
                                {
                                    Key = lastNode.InnerText,
                                    Path = GetFileUrl(lastNode.Attributes[1].Value, xe.ChildNodes.Item(0).InnerText),
                                    Image = GetEmojiImage(lastNode.Attributes[1].Value, xe.ChildNodes.Item(0).InnerText),
                                    Type = GetFileType(xe.ChildNodes.Item(0).InnerText)
                                });
                            }
                        }
                        EmojiList.Add(new ImageList()
                        {
                            Key = xe.ChildNodes.Item(0).InnerText,
                            EmojiArray = list
                        });
                        EmojiArrayDic.AddRange(list);
                    }
                }
            }
            return EmojiList;
        }
        /// <summary>
        /// 返回Emoji图像
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private static string GetFileUrl(string name, string endName)
        {
            string imgUrl = string.Empty;
            if (endName.Contains("gif"))
            {
                imgUrl = "pack://application:,,,/RRQM.Emoji;component/Image/g" + name + $".gif";
                return imgUrl;
            }
            else
            {
                imgUrl = "pack://application:,,,/RRQM.Emoji;component/Image/" + name + $".png";
                return imgUrl;
            }
        }

        public static Emoji KeyGetEmojiUrl(string name)
        {
            var temp = EmojiArrayDic.Where(it => it.Key.Contains(name)).ToList();
            if (temp.Count > 0)
            {
                return temp[0];
            }
            return null;
        }

        public static Emoji PathGetEmojiUrl(string path)
        {
            var temp = EmojiArrayDic.Where(it => it.Path.Contains(path)).ToList();
            if (temp.Count > 0)
            {
                return temp[0];
            }
            return null;
        }

        private static int GetFileType(string endName)
        {
            if (endName.Contains("gif"))
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        private static BitmapImage GetEmojiImage(string name, string endName)
        {
            try
            {
                if (endName.Contains("gif"))
                {
                    BitmapImage bitmap = new BitmapImage();
                    string imgUrl = "pack://application:,,,/RRQM.Emoji;component/Image/g" + name + $".gif";
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(imgUrl, UriKind.RelativeOrAbsolute);
                    bitmap.EndInit();
                    return bitmap;
                }
                else
                {
                    try
                    {
                        BitmapImage bitmap = new BitmapImage();
                        string imgUrl = "pack://application:,,,/RRQM.Emoji;component/Image/" + name + $".png";
                        bitmap.BeginInit();
                        bitmap.UriSource = new Uri(imgUrl, UriKind.RelativeOrAbsolute);
                        bitmap.EndInit();
                        bitmap.DecodePixelWidth = 30;
                        bitmap.DecodePixelHeight = 30;
                        return bitmap;
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }
                    
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }

    public class ImageList : INotifyPropertyChanged
    {
        public Action<ImageList> CheckArray;
        public string Key { get; set; }
        public List<Emoji> EmojiArray { get; set; }
        public BitmapImage Icon 
        {
            get 
            {
                if (this.EmojiArray.Count > 0)
                {
                    return this.EmojiArray[0].Image;
                }
                return null;
            }
        }
        public ICommand CheckEmoji =>
          new DelegateCommand((p) =>
          {
              
          });

        private bool _Check;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool Check
        {
            get
            {
                return this._Check;
            }
            set
            {
                this._Check = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Check"));
                if (value)
                {
                    this.CheckArray?.Invoke(this);
                }
            }
        }
    }

    public class Emoji
    {
        public string Key { get; set; }
        public string Path { get; set; }
        public int Type { get; set; }
        public BitmapImage Image { get; set; }
    }
}

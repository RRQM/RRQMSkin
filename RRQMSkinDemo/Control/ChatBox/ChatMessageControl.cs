using RRQM.Emoji;
using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace RRQMSkinDemo.Control.ChatBox
{
    [TemplatePart(Name = TextBlockTemplateName, Type = typeof(TextBlock))]
    [TemplatePart(Name = RichTextBoxTemplateName, Type = typeof(RichTextBox))]
    public class ChatMessageControl : System.Windows.Controls.Control
    {
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(nameof(Text), typeof(string), typeof(ChatMessageControl), new PropertyMetadata(default(string), OnTextChanged));

        private const string RichTextBoxTemplateName = "PART_RichTextBox";
        private const string TextBlockTemplateName = "PART_TextBlock";


        public RichTextBox _richTextBox;
        private TextBlock _textBlock;

        static ChatMessageControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ChatMessageControl), new FrameworkPropertyMetadata(typeof(ChatMessageControl)));
        }

        public string Text
        {
            get => (string)this.GetValue(TextProperty);
            set => this.SetValue(TextProperty, value);
        }

        public override void OnApplyTemplate()
        {
            this._textBlock = (TextBlock)this.GetTemplateChild(TextBlockTemplateName);
            this._richTextBox = (RichTextBox)this.GetTemplateChild(RichTextBoxTemplateName);
            this.UpdateVisual();
        }

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (ChatMessageControl)d;

            obj.UpdateVisual();
        }

        private void Text_Msg(ref Paragraph paragraph, ref StringBuilder buffer,string msg) 
        {
            if (msg == null)
            {
                return;
            }
            foreach (var c in msg)
            {
                switch (c)
                {
                    case '[':
                        this._textBlock.Inlines.Add(buffer.ToString());
                        paragraph.Inlines.Add(buffer.ToString());
                        buffer.Clear();
                        buffer.Append(c);
                        break;

                    case ']':
                        buffer.Append(c);
                        var current = buffer.ToString();
                        if (current.StartsWith("["))
                        {
                            var emotionName = current;
                            var str = emotionName.Replace("[","");
                            str = str.Replace("]","");
                            var temp = EmojiLoadingGIF.KeyGetEmojiUrl(str);
                            if (temp != null)
                            {
                                {

                                    Image img = new Image()
                                    {
                                        //Source = Emotions[emotionName] as BitmapSource,
                                    };
                                    img.Width = 24;
                                    img.Height = 24;
                                    img.FocusVisualStyle = null;
                                    this._textBlock.Inlines.Add(new InlineUIContainer(img));

                                }
                                {
                                    Image img = new Image()
                                    {
                                        Source = temp.Image,
                                    };
                                    img.Width = 24;
                                    img.Height = 24;
                                    img.FocusVisualStyle = null;
                                    paragraph.Inlines.Add(new InlineUIContainer(img));

                                }

                                buffer.Clear();
                                continue;
                            }
                        }


                        this._textBlock.Inlines.Add(buffer.ToString());
                        paragraph.Inlines.Add(buffer.ToString());
                        buffer.Clear();
                        break;

                    default:
                        buffer.Append(c);
                        break;
                }
            }
        }

        private void UpdateVisual()
        {
            if (this._textBlock == null || this._richTextBox == null)
            {
                return;
            }

            this._textBlock.Inlines.Clear();
            this._richTextBox.Document.Blocks.Clear();

            var paragraph = new Paragraph();

            var buffer = new StringBuilder();

            this.Text_Msg(ref paragraph, ref buffer, this.Text);


            this._textBlock.Inlines.Add(buffer.ToString());
            paragraph.Inlines.Add(buffer.ToString());


            this._richTextBox.Document.Blocks.Add(paragraph);
        }

        private void RoutedEventHandler(object sender, RoutedEventArgs e)
        {
            ((MediaElement)sender).Position = ((MediaElement)sender).Position.Add(TimeSpan.FromMilliseconds(1));
        }
    }
}

using System;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;


namespace RRQMSkin.Controls.Video
{
    /// <summary>
    /// VideoPlayer.xaml 的交互逻辑
    /// </summary>
    public partial class VideoPlayer : UserControl
    {
        [Category("Extend Properties")]
        public string Source
        {
            get { return (string)GetValue(SourceProperty); }
            set
            {
                SetValue(SourceProperty, value);
            }
        }

        public static readonly DependencyProperty SourceProperty =
          DependencyProperty.Register("Source", typeof(string), typeof(VideoPlayer), new PropertyMetadata(string.Empty));

        [Category("Extend Properties")]
        public bool LoadedStart
        {
            get { return (bool)GetValue(LoadedStartProperty); }
            set
            {
                SetValue(LoadedStartProperty, value);
            }
        }
        public static readonly DependencyProperty LoadedStartProperty =
          DependencyProperty.Register("LoadedStart", typeof(bool), typeof(VideoPlayer), new PropertyMetadata(false));

        [Category("Extend Properties")]
        public bool IsMuted
        {
            get { return (bool)GetValue(IsMutedProperty); }
            set
            {
                SetValue(IsMutedProperty, value);
            }
        }
        public static readonly DependencyProperty IsMutedProperty =
          DependencyProperty.Register("IsMuted", typeof(bool), typeof(VideoPlayer), new PropertyMetadata(false));



        /// <summary>
        /// 视频暂停播放
        /// </summary>
        public Action VideoPause;
        /// <summary>
        /// 视频开始播放
        /// </summary>
        public Action VideoStart;
        /// <summary>
        /// 视频播放完毕
        /// </summary>
        public Action VideoEnd;
        /// <summary>
        /// 视频禁音按钮触发
        /// </summary>
        public Action<bool> VideoMuted;


        private bool VideoInit = false;
        private int CollTimer { get; set; }
        bool Pause = false;

        private void SetSource(string Paths)
        {
            if (Paths.Substring(0, 1) == "/")
            {
                Paths = Paths.Substring(1);
            }
            var path = Paths;
            if (File.Exists(Paths))//判断文件是否存在
            {
                if (!Paths.Contains(":\\"))//相对路径
                {
                    if (Paths.Length > 0)
                    {
                        
                        path = $"{AppDomain.CurrentDomain.BaseDirectory}{Paths}";
                    }
                }

            }
            if (path.Length > 0 && File.Exists(path))
            {
                this.video.Source = new Uri(path);
                this.video.Play();
                this.video.Pause();
                DispatcherTimer time = new DispatcherTimer();
                time.Interval = new TimeSpan(0, 0, 0,1);
                time.Tick += Time_Tick;
                time.Start();

                DispatcherTimer timeControl = new DispatcherTimer();
                timeControl.Interval = new TimeSpan(0, 0, 0, 1);
                timeControl.Tick += TimeControl_Tick;
                timeControl.Start();

                VideoInit = true;
                if (LoadedStart)
                {
                    video.Play();
                }
            }

        }

        private void TimeControl_Tick(object sender, EventArgs e)
        {
            if (this.CollTimer > 0)
            {
                this.CollTimer--;
            }
            else if (this.CollTimer == 0)
            {
                this.control_bor.Visibility = Visibility.Collapsed;
                this.control_btn.Visibility = Visibility.Collapsed;
                this.CollTimer = -1;
            }

        }

        public VideoPlayer()
        {
            InitializeComponent();
        }

        private bool IsDesignMode(Control ctl)
        {
            return DesignerProperties.GetIsInDesignMode(ctl);
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //播放器设置
            if (IsMuted)
            {
                Muted.IsChecked = true;
            }
            if (LoadedStart)
            {
                PlayCheck.IsChecked = true;
            }
            if (!this.IsDesignMode(this))
            {
                this.control_bor.Visibility = Visibility.Collapsed;
                this.control_btn.Visibility = Visibility.Collapsed;
                SetSource(Source);
            }

        }

        private void Time_Tick(object sender, EventArgs e)
        {
            if (!Pause)
            {
                this.Press.Value = Convert.ToInt32(this.video.Position.TotalMilliseconds);
            }
            if (this.video.NaturalDuration.HasTimeSpan)
            {
                this.StartTimer.Text = new TimeSpan(0, 0, Convert.ToInt32(this.video.Position.TotalSeconds)).ToString(@"mm\:ss");
                this.EndTimer.Text = $"-{new TimeSpan(0, 0, Convert.ToInt32(this.video.NaturalDuration.TimeSpan.TotalSeconds) - Convert.ToInt32(this.video.Position.TotalSeconds)).ToString(@"mm\:ss")}";
            }

        }

        private void PlayCheck_Checked(object sender, RoutedEventArgs e)
        {
            var temp = sender as CheckBox;
            if (temp.IsChecked.Value)
            {
                if (this.Press.Value == Press.Maximum)
                {
                    this.Press.Value = 0;
                    this.video.Position = new TimeSpan(0);
                }
                this.video.Play();
                if (VideoInit)
                    VideoStart?.Invoke();
            }
            else
            {
                this.video.Pause();
                if (VideoInit)
                    VideoPause?.Invoke();
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var temp = sender as CheckBox;
            this.video.IsMuted = temp.IsChecked.Value;

            if (VideoInit)
                VideoMuted?.Invoke(temp.IsChecked.Value);
        }

        private void video_MediaOpened(object sender, RoutedEventArgs e)
        {
            var temp = video.NaturalDuration.TimeSpan;
            this.StartTimer.Text = "00:00";
            this.EndTimer.Text = $"-{temp.ToString(@"mm\:ss")}";
            this.Press.Maximum = Convert.ToInt32(temp.TotalMilliseconds);
            this.Press.Value = 0;
        }

        private void Press_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var temp = sender as Slider;

            if (temp.Value == temp.Maximum)
            {
                this.PlayCheck.IsChecked = false;
                this.video.Pause();
                if (VideoInit)
                    VideoEnd?.Invoke();
            }

        }

        private void Press_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            this.Pause = true;
        }

        private void Press_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            this.Pause = false;
            TimeSpan ts = new TimeSpan(0, 0, 0, 0, Convert.ToInt32(this.Press.Value));
            this.video.Position = ts;
        }

        private void video_MouseMove(object sender, MouseEventArgs e)
        {
            this.control_bor.Visibility = Visibility.Visible;
            this.control_btn.Visibility = Visibility.Visible;
        }

        private void video_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!this.VideoGrid.IsMouseOver && !this.control_btn.IsMouseOver && !this.control_bor.IsMouseOver && !this.video.IsMouseOver)
            {
                this.CollTimer = 1;
            }

        }
    }
}

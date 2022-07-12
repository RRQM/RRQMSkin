using RRQMSkin.Windows;
using System.Collections.Concurrent;
using System.Timers;
using System.Windows;

namespace RRQMSkin
{
    public class RRQMDialog
    {
        public RRQMDialog(Window window)
        {
            this.window = window;
            Timer timer = new Timer(10);
            timer.Elapsed += this.Timer_Elapsed;
            timer.Start();
        }
        Window window;
        int tick;
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (this.tick == 0 || this.tick > 200)
            {
                this.tick = 0;
                if (this.queue.TryDequeue(out string msg))
                {
                    this.tick = 1;
                    Application.Current.Dispatcher.Invoke(() =>
                        {
                            DialogWindow dialogWindow = new DialogWindow(msg);
                            dialogWindow.Owner = this.window;
                            dialogWindow.Show();
                        });
                }
            }
            else
            {
                this.tick++;
            }

        }

        ConcurrentQueue<string> queue = new ConcurrentQueue<string>();
        public void Show(string mes)
        {
            this.queue.Enqueue(mes);
        }
    }
}

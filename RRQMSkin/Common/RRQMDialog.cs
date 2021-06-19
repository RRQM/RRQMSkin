using RRQMSkin.Windows;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;

namespace RRQMSkin
{
    public class RRQMDialog
    {
        public RRQMDialog(Window window = null)
        {
            this.window = window;
            Timer timer = new Timer(10);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }
        Window window;
        int tick;
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (tick == 0 || tick > 200)
            {
                tick = 0;
                if (queue.TryDequeue(out string msg))
                {
                    tick = 1;
                    Application.Current.Dispatcher.Invoke(() =>
                        {
                            DialogWindow dialogWindow = new DialogWindow(msg);
                            dialogWindow.Owner = window;
                            dialogWindow.Show();
                        });
                }
            }
            else
            {
                tick++;
            }

        }

        ConcurrentQueue<string> queue = new ConcurrentQueue<string>();
        public void Show(string mes)
        {
            queue.Enqueue(mes);
        }
    }
}

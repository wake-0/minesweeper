using System;
using System.Timers;
using PostSharp.Patterns.Model;
using System.Diagnostics;

namespace Minesweeper.Services
{
    [NotifyPropertyChanged]
    public class TimerService
    {
        private readonly Stopwatch watch;
        private readonly Timer timer;

        public TimeSpan Time { get; private set; }

        public TimerService()
        {
            timer = new Timer(500);
            watch = new Stopwatch();

            timer.Elapsed += TimerElapsed;
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            Time = watch.Elapsed;
        }

        public void Start()
        {
            Time = new TimeSpan();
            watch.Reset();

            watch.Start();
            timer.Start();
        }

        public void Stop()
        {
            watch.Stop();
            timer.Stop();
        }
    }
}

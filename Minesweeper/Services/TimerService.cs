using System;
using System.Timers;
using PostSharp.Patterns.Model;
using System.Diagnostics;

namespace Minesweeper.Services
{
    [NotifyPropertyChanged]
    public class TimerService
    {
        #region Fields
        private readonly Stopwatch watch;
        private readonly Timer timer;
        #endregion

        #region Properties
        public TimeSpan Time { get; private set; }
        #endregion

        #region Constructor
        public TimerService()
        {
            timer = new Timer(500);
            watch = new Stopwatch();

            timer.Elapsed += TimerElapsed;
        }
        #endregion

        #region Methods
        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            Time = watch.Elapsed;
        }

        public void Start()
        {
            watch.Start();
            timer.Start();
        }

        public void Reset()
        {
            Time = new TimeSpan();
            watch.Reset();
        }

        public void Stop()
        {
            watch.Stop();
            timer.Stop();
        }
        #endregion
    }
}

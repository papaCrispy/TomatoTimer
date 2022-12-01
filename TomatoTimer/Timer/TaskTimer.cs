using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace TomatoTimer.Timer
{
    public class TaskTimer
    {
        public readonly int constInterval = 1000; 

        private DispatcherTimer _dispatcherTimer;
        public DispatcherTimer DispatcherTimer
        {
            get { return _dispatcherTimer; }
            private set { _dispatcherTimer = value; }
        }

        private int _duration;
        public int Duration
        {
            get { return _duration; }
            set { _duration = value; }
        }

        public TaskTimer()
        {
            this.DispatcherTimer = new DispatcherTimer(DispatcherPriority.Normal);
            this.DispatcherTimer.Interval = TimeSpan.FromMilliseconds(constInterval);
        }
    }
}

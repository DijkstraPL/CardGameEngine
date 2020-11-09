using System;
using System.Timers;

namespace CardGame_Game.Game
{
    public class TurnTimer
    {
        public Timer Timer { get; } 
        public int Time { get; private set; } = TimePerTurn;

        public const int TimePerTurn = 90;

        public event EventHandler TimeEnded;

        public TurnTimer()
        {
            Timer = new Timer();
            Timer.Interval = 1000;
            Timer.Enabled = true;
            Timer.AutoReset = true;
            Timer.Elapsed += OnElapsed;
        }

        public void Start()
        {
            Timer.Start();
        }

        public void Reset()
        {
            Time = TimePerTurn;
        }

        private void OnElapsed(object sender, ElapsedEventArgs e)
        {
            Time--;
            if (Time < 0)
                TimeEnded?.Invoke(this, EventArgs.Empty);
        }
    }
}

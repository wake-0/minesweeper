using System;

namespace Minesweeper.Utils
{
    public class GameStatistic
    {
        public int Mines { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public TimeSpan Time { get; set; }
        public DateTime Date { get; private set; }

        public GameStatistic()
        {
            Date = DateTime.Now;
        }
    }
}

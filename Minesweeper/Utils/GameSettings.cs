using PostSharp.Patterns.Model;

namespace Minesweeper.Utils
{
    [NotifyPropertyChanged]
    public class GameSettings
    {
        public int NumberOfMines { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }

        public GameSettings()
        {
            NumberOfMines = 10;
            Rows = 20;
            Columns = 10;
        }
    }
}

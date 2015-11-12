using PostSharp.Patterns.Model;

namespace Minesweeper.Utils
{
    [NotifyPropertyChanged]
    public class GameSettings
    {
        public int NumberOfMines { get; set; }
        public int Size { get; set; }

        public GameSettings()
        {
            NumberOfMines = 5;
            Size = 5;
        }
    }
}

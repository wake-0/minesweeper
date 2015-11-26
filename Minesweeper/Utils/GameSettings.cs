using PostSharp.Patterns.Model;

namespace Minesweeper.Utils
{
    [NotifyPropertyChanged]
    public class GameSettings
    {
        private int numberOfMines;
        private int rows;
        private int columns;

        public int NumberOfMines
        {
            get { return numberOfMines; }
            set
            {
                if (value > Rows * Columns)
                {
                    numberOfMines = Rows * Columns;
                }
                else
                {
                    numberOfMines = value;
                }

            }
        }
        public int Rows
        {
            get { return rows; }
            set
            {
                if (value < 1)
                {
                    rows = 10;
                }
                else
                {
                    rows = value;
                }

            }
        }
        public int Columns
        {
            get { return columns; }
            set
            {
                if (value < 1)
                {
                    columns = 10;
                }
                else
                {
                    columns = value;
                }

            }
        }

        public GameSettings()
        {
            Rows = 10;
            Columns = 10;
            NumberOfMines = 10;
        }
    }
}

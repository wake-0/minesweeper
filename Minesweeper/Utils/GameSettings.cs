using PostSharp.Patterns.Model;

namespace Minesweeper.Utils
{
    [NotifyPropertyChanged]
    public class GameSettings
    {
        #region Fields
        private int numberOfMines;
        private int rows;
        private int columns;
        #endregion

        #region Properties
        public int NumberOfMines { get { return numberOfMines; } set { SetValue(ref numberOfMines, value, 1, Rows * Columns);} }
        public int Rows { get { return rows; } set { SetValue(ref rows, value); } }
        public int Columns { get { return columns; } set { SetValue(ref columns, value); } }
        #endregion

        #region Constructor
        public GameSettings()
        {
            Rows = 10;
            Columns = 10;
            NumberOfMines = 10;
        }
        #endregion

        #region Methods
        private void SetValue(ref int value, int newValue, int minValue = 1, int maxValue = 30)
        {
            if (newValue < minValue)
            {
                value = minValue;
            }
            else if (newValue > maxValue)
            {
                value = maxValue;
            }
            else
            {
                value = newValue;
            }
        }
        #endregion
    }
}

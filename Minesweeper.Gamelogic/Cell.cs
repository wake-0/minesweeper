using PostSharp.Patterns.Model;

namespace Minesweeper.Gamelogic
{
    [NotifyPropertyChanged]
    public class Cell
    {
        #region Fields
        private bool isToggled;
        #endregion

        #region Properties
        public int Number { get; set; }
        public int NumberOfMarkedNeighbours { get; set; }

        public int Row { get; set; }
        public int Column { get; set; }
        public CellType Type { get; set; }

        public bool IsToggled
        {
            get
            {
                return isToggled;
            }
            set
            {
                isToggled = value;
                if (isToggled)
                {
                    IsMarked = false;
                }
            }
        }
        public bool IsMarked { get; set; }
        #endregion

        #region Constructor
        public Cell(int row, int column)
        {
            Row = row;
            Column = column;

            Type = CellType.Number;
        }
        #endregion
    }
}

using PostSharp.Patterns.Model;
namespace Minesweeper.Gamelogic
{
    [NotifyPropertyChanged]
    public class Cell
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public Cell[,] Neighbours { get; set; }

        public CellType Type { get; set; }
        public bool State { get; set; }
        

        public Cell(int row, int column)
        {
            Row = row;
            Column = column;

            Neighbours = new Cell[3, 3] { 
                { null, null, null }, 
                { null, null, null }, 
                { null, null, null } };
        }

    }
}

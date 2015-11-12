using PostSharp.Patterns.Model;
using System.Linq;

namespace Minesweeper.Gamelogic
{
    [NotifyPropertyChanged]
    public class Cell
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public Cell[,] Neighbours { get; set; }

        public CellType Type { get; set; }
        public bool IsToggled { get; set; }

        [SafeForDependencyAnalysis]
        public int Number
        {
            get
            {
                if (Depends.Guard)
                {
                    Depends.On(Neighbours);
                }

                return Neighbours.OfType<Cell>().Count(c => c.Type == CellType.Mine);
            }
        }

        public bool IsMarked { get; set; }

        public Cell(int row, int column)
        {
            Row = row;
            Column = column;

            Neighbours = new Cell[3, 3] { 
                { null, null, null }, 
                { null, null, null }, 
                { null, null, null } };
        }

        public void OpenCell()
        {
            IsToggled = true;
            if (Type == CellType.Number)
            {
                // Open empty neighbours
                var emptyCells = Neighbours.OfType<Cell>().Where(c => c.Type == CellType.Number && c.Number == 0 && c != this && !c.IsToggled);
                foreach(var cell in emptyCells)
                {
                    cell.OpenCell();
                }
            }
        }
    }
}

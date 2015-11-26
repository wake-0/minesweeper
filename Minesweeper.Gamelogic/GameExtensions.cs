using System.Collections.Generic;

namespace Minesweeper.Gamelogic
{
    public static class GameExtensions
    {
        #region Methods
        public static List<Cell> GetNeighbours(this Cell[,] cells, Cell cell)
        {
            List<Cell> neighbours = new List<Cell>();
            int lowerRow = cell.Row - 1;
            int upperRow = cell.Row + 1;
            int lowerColumn = cell.Column - 1;
            int upperColumn = cell.Column + 1;

            for (int r = lowerRow; r <= upperRow; r++)
            {
                if (r >= cells.GetLength(0)) { continue; }
                for (int c = lowerColumn; c <= upperColumn; c++)
                {
                    if (r < 0 || c < 0 || (r == cell.Row && c == cell.Column) || c >= cells.GetLength(1)) { continue; }

                    neighbours.Add(cells[r, c]);
                }
            }

            return neighbours;
        }
        #endregion
    }
}

using Minesweeper.Gamelogic;
using Minesweeper.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace Minesweeper.Utils
{
    public static class GridExtensions
    {
        public static IEnumerable<CellControl> FindCellControls(this Grid grid)
        {
            return grid.Children.OfType<CellControl>();
        }

        public static IEnumerable<Cell> FindCells(this Grid grid)
        {
            return grid.Children.OfType<CellControl>().Select(cellControl => cellControl.Cell);
        }

        public static Cell FindCell(this Grid grid, int row, int column)
        {
            var cellControl = grid
                .FindCellControls()
                .FirstOrDefault(control => control.Cell.Column == column && control.Cell.Row == row);

            return cellControl == null ? null : cellControl.Cell;
        }

        public static void FindAndSetNeighbours(this Cell cell, Grid grid)
        {
            var cells = grid.FindCells();

            int lowerRow = cell.Row - 1;
            int upperRow = cell.Row + 1;
            int lowerColumn = cell.Column - 1;
            int upperColumn = cell.Column + 1;

            for (int row = lowerRow; row <= upperRow; row++)
            {
                for (int column = lowerColumn; column <= upperColumn; column++)
                {
                    if (row < 0 || column < 0 || (row == cell.Row && column == cell.Column)) { continue; }

                    var neighbourCell = grid.FindCell(row, column);

                    int neighbourRow = Math.Abs(cell.Row - row - 1);
                    int neighbourColumn = Math.Abs(cell.Column - column - 1);

                    cell.Neighbours[neighbourColumn, neighbourRow] = neighbourCell;
                }
            }
        }
    }
}

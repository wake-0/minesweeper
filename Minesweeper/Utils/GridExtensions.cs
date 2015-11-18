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
    }
}

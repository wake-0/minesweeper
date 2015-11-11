using Minesweeper.Gamelogic;
using Minesweeper.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Minesweeper.Utils
{
    public static class GameGridFactory
    {
        public static Grid CreateGameGrid(int size)
        {
            if (size <= 0) { throw new ArgumentException("Size should be > 0"); }

            Grid grid = new Grid();
            grid.Background = Brushes.White;

            // Create rows and columns
            for (int i = 0; i < size; i++)
            {
                var column = new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) };
                var row = new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) };

                grid.ColumnDefinitions.Add(column);
                grid.RowDefinitions.Add(row);
            }

            // Create cell controls
            for (int row = 0; row < size; row++)
            {
                for (int column = 0; column < size; column++)
                {
                    CellControl cell = new CellControl(new Cell(row, column));

                    Grid.SetRow(cell, row);
                    Grid.SetColumn(cell, column);
                    grid.Children.Add(cell);
                }
            }

            //Connect cell with neighbours
            for (int row = 0; row < size; row++)
            {
                for (int column = 0; column < size; column++)
                {
                    var cell = grid.FindCell(row, column);
                    if (cell != null)
                    {
                        cell.FindAndSetNeighbours(grid);
                    }
                }
            }

            return grid;
        }

        private static IEnumerable<CellControl> FindCellControls(this Grid grid)
        {
            return grid.Children.OfType<CellControl>();
        }

        private static IEnumerable<Cell> FindCells(this Grid grid)
        {
            return grid.Children.OfType<CellControl>().Select(cellControl => cellControl.Cell);
        }

        private static Cell FindCell(this Grid grid, int row, int column)
        {
            var cellControl = grid
                .FindCellControls()
                .FirstOrDefault(control => control.Cell.Column == column && control.Cell.Row == row);

            return cellControl == null ? null : cellControl.Cell;
        }

        private static void FindAndSetNeighbours(this Cell cell, Grid grid)
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

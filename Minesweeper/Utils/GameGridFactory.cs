using Minesweeper.Gamelogic;
using Minesweeper.Presentation;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Minesweeper.Utils
{
    public static class GameGridFactory
    {
        public static Grid CreateGameGrid(GameController controller, int size)
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
                    CellControl cell = new CellControl(controller, new Cell(row, column) { Type = CellType.Number });

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
    }
}

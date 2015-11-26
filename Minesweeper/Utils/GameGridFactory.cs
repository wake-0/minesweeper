using Minesweeper.Gamelogic;
using Minesweeper.Presentation;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Minesweeper.Utils
{
    public static class GameGridFactory
    {
        #region Methods
        public static Cell[,] CreateGame(int rows, int columns, int mines)
        {
            Cell[,] game = CreateCells(rows, columns);

            Random random = new Random();
            int maxIndex = rows * columns;

            // Continue this operation for number of mines
            for (int i = 0; i < mines; i++)
            {
                int randomIndex = 0;
                bool wasMine = false;
                do
                {
                    wasMine = false;

                    randomIndex = random.Next(0, maxIndex);

                    int row = randomIndex / columns;
                    int column = randomIndex % columns;
                    var cell = game[row, column];

                    if (cell.Type == CellType.Mine)
                    {
                        wasMine = true;
                    }
                    else
                    {
                        var neighbours = game.GetNeighbours(cell).Where(c => c.Type != CellType.Mine);
                        neighbours.ToList().ForEach(n => n.Number++);
                    }

                    cell.Type = CellType.Mine;
                }
                while (wasMine);
            }

            return game;
        }

        public static Grid CreateGameGrid(GameController controller, Cell[,] game)
        {
            Grid grid = new Grid();
            int createdColumns = 0;

            for (int row = 0; row < game.GetLength(0); row++)
            {
                grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(25, GridUnitType.Pixel) });

                for (int column = 0; column < game.GetLength(1); column++)
                {
                    // Create columns only once
                    if (createdColumns < game.GetLength(1))
                    {
                        grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(25, GridUnitType.Pixel) });
                        createdColumns++;
                    }

                    // Create cell controls
                    CellControl cell = new CellControl(controller, game[row, column]);

                    Grid.SetRow(cell, row);
                    Grid.SetColumn(cell, column);
                    grid.Children.Add(cell);
                }
            }

            return grid;
        }

        private static Cell[,] CreateCells(int rows, int columns)
        {
            Cell[,] cells = new Cell[rows, columns];

            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    cells[row, column] = new Cell(row, column);
                }
            }

            return cells;
        }
        #endregion
    }
}

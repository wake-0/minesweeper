using Minesweeper.Gamelogic;
using Minesweeper.Presentation;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Minesweeper.Utils
{
    public static class GameGridFactory
    {
        public static Cell[,] CreateGame(int size, int mines)
        {
            Cell[,] game = CreateCells(size, size);

            Random random = new Random();
            int maxIndex = size * size;

            // Continue this operation for number of mines
            for (int i = 0; i < mines; i++)
            {
                int randomIndex = 0;
                bool wasMine = false;
                do
                {
                    wasMine = false;

                    randomIndex = random.Next(0, maxIndex);

                    int row = randomIndex / size;
                    int column = randomIndex % size;

                    if (game[row, column].Type == CellType.Mine)
                    {
                        wasMine = true;
                    }
                    else
                    {
                        int lowerRow = row - 1;
                        int upperRow = row + 1;
                        int lowerColumn = column - 1;
                        int upperColumn = column + 1;

                        for (int r = lowerRow; r <= upperRow; r++)
                        {
                            if (r >= size) { continue; }
                            for (int c = lowerColumn; c <= upperColumn; c++)
                            {
                                if (r < 0 || c < 0 || (r == row && c == column) || c >= size) { continue; }

                                if (game[r, c].Type == CellType.Mine) { continue; }

                                game[r, c].Number++;
                            }
                        }

                    }

                    game[row, column].Type = CellType.Mine;
                }
                while (wasMine);
            }

            return game;
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
    }
}

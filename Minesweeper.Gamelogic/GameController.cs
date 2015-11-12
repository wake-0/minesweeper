using Minesweeper.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Minesweeper.Gamelogic
{
    public class GameController
    {
        private IEnumerable<Cell> cells;
        private readonly int numberOfMines;

        public event EventHandler GameOver;
        public event EventHandler GameWon;

        public GameController()
        {
            // Check number of mines < cells.count
            numberOfMines = 2;
        }

        public void SetCells(IEnumerable<Cell> cells)
        {
            this.cells = cells;
            SetMines();
        }

        public void OpenCell(Cell cell)
        {
            // Open cells and all depending cells
            cell.OpenCell();

            if (cell.Type == CellType.Mine)
            {
                OnGameOver();
            }
            else
            {
                CheckGameWon();
            }
        }

        private void CheckGameWon()
        {
            if (!cells.OfType<Cell>().Any(c => c.Type == CellType.Number && !c.IsToggled))
            {
                OnGameWon();
            }
        }

        private void OnGameOver()
        {
            if (GameOver != null)
            {
                GameOver.Invoke(this, new EventArgs());
            }
        }

        private void OnGameWon()
        {
            if (GameWon != null)
            {
                GameWon.Invoke(this, new EventArgs());
            }
        }

        private void SetMines()
        {
            Random random = new Random();

            // Continue this operation for number of mines
            for (int i = 0; i < numberOfMines; i++)
            {
                int randomIndex = 0;
                Cell randomCell = null;
                do
                {
                    randomIndex = random.Next(0, cells.Count());
                    randomCell = cells.ElementAt(randomIndex);
                }
                while (randomCell.Type == CellType.Mine);

                // Set as mine 
                randomCell.Type = CellType.Mine;
            }
        }
    }
}

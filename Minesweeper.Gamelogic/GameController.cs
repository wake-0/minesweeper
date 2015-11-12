using System;
using System.Collections.Generic;
using System.Linq;

namespace Minesweeper.Gamelogic
{
    public class GameController
    {
        private IEnumerable<Cell> cells;
        private int numberOfMines;
        private int steps;

        public event EventHandler GameOver;
        public event EventHandler GameWon;
        public event EventHandler FirstStep;

        public GameController()
        {
            // Check number of mines < cells.count
            numberOfMines = 2;
            steps = 0;
        }

        public void SetCells(IEnumerable<Cell> cells, int numberOfMines)
        {
            this.cells = cells;
            this.numberOfMines = numberOfMines;
            SetMines();
        }

        public void OpenCell(Cell cell)
        {
            if (steps == 0)
            {
                OnFirstStep();
            }

            // Open cells and all depending cells
            cell.OpenCell();

            if (cell.Type == CellType.Mine)
            {
                ToggleAllCells();
                OnGameOver();
            }
            else
            {
                CheckGameWon();
            }

            steps++;
        }

        private void CheckGameWon()
        {
            if (!cells.OfType<Cell>().Any(c => c.Type == CellType.Number && !c.IsToggled))
            {
                ToggleAllCells();
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

        private void OnFirstStep()
        {
            if (FirstStep != null)
            {
                FirstStep.Invoke(this, new EventArgs());
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

        private void ToggleAllCells()
        {
            cells.ToList().ForEach(c => c.IsToggled = true);
        }

        public void Reset()
        {
            steps = 0;
        }
    }
}

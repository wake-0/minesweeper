using System;
using System.Collections.Generic;
using System.Linq;
using PostSharp.Patterns.Model;

namespace Minesweeper.Gamelogic
{
    [NotifyPropertyChanged]
    public class GameController
    {
        private bool isFirstStep;
        private bool isGameOver;

        public event EventHandler GameOver;
        public event EventHandler GameWon;
        public event EventHandler FirstStep;

        [SafeForDependencyAnalysis]
        public int NumberOfOpendCells
        {
            get
            {
                if (Depends.Guard)
                {
                    Depends.On(Cells);
                }

                return Cells.Cast<Cell>().Count(c => c.IsToggled);
            }
        }
        public int NumberOfMarkedCells { get; set; }
        public int NumberOfMines { get; set; }

        public GameController()
        {
            // Check number of mines < cells.count
            NumberOfMines = 0;
            isFirstStep = true;
            isGameOver = false;
        }

        public Cell[,] Cells { get; set; }

        public void OpenCell(Cell cell)
        {
            ToggleCore(cell);
            CheckGameWon();
        }

        public void OpenNeighbours(Cell cell)
        {
            if (!cell.IsToggled)
            {
                OpenCell(cell);
            }
            else
            {
                // Open cells and all depending cells
                var neighbours = GetNeighbours(cell);
                var markedNeighbours = neighbours.Count(n => n.IsMarked);
                
                if (markedNeighbours == cell.Number)
                {
                    neighbours.ForEach(ToggleCore);
                    CheckGameWon();
                }
            }
        }

        public void MarkCell(Cell cell)
        {
            if (cell.IsToggled) { return; }

            cell.IsMarked = !cell.IsMarked;

            if (cell.IsMarked)
            {
                NumberOfMarkedCells++;
            }
            else
            {
                NumberOfMarkedCells--;
            }
        }

        private List<Cell> GetNeighbours(Cell cell)
        {
            List<Cell> neighbours = new List<Cell>();
            int lowerRow = cell.Row - 1;
            int upperRow = cell.Row + 1;
            int lowerColumn = cell.Column - 1;
            int upperColumn = cell.Column + 1;

            for (int r = lowerRow; r <= upperRow; r++)
            {
                if (r >= Cells.GetLength(0)) { continue; }
                for (int c = lowerColumn; c <= upperColumn; c++)
                {
                    if (r < 0 || c < 0 || (r == cell.Row && c == cell.Column) || c >= Cells.GetLength(1)) { continue; }

                    neighbours.Add(Cells[r, c]);
                }
            }

            return neighbours;
        }

        private void CheckGameWon()
        {
            if (isGameOver) { return; }

            if (Cells.Length - NumberOfOpendCells <= NumberOfMines)
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

        private void ToggleAllCells()
        {
            foreach (var cell in Cells.Cast<Cell>())
            {
                cell.IsToggled = true;
            }
        }

        private void ToggleCore(Cell cell)
        {
            if (cell.IsToggled || cell.IsMarked) { return; }

            if (isFirstStep) { OnFirstStep(); }

            // Open cells and all depending cells
            cell.IsToggled = true;

            if (cell.Number == 0)
            {
                GetNeighbours(cell).ForEach(ToggleCore);
            }

            if (cell.Type == CellType.Mine)
            {
                isGameOver = true;
                ToggleAllCells();
                OnGameOver();
            }

            isFirstStep = false;
        }

        public void Reset()
        {
            isFirstStep = true;
            isGameOver = false;
        }
    }
}
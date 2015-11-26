using System;
using System.Linq;
using PostSharp.Patterns.Model;

namespace Minesweeper.Gamelogic
{
    [NotifyPropertyChanged]
    public class GameController
    {
        #region Fields
        private bool isFirstStep;
        private bool isGameOver;
        #endregion

        #region Events
        public event EventHandler GameOver;
        public event EventHandler GameWon;
        public event EventHandler FirstStep;
        #endregion

        #region Properties
        public Cell[,] Cells { get; set; }

        public int NumberOfMarkedCells { get; set; }
        public int NumberOfMines { get; set; }

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
        #endregion

        #region Constructor
        public GameController()
        {
            // Check number of mines < cells.count
            NumberOfMines = 0;
            isFirstStep = true;
            isGameOver = false;
        }
        #endregion

        #region Methods
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
                var neighbours = Cells.GetNeighbours(cell);
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
            if (GameOver != null && !isGameOver)
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
                Cells.GetNeighbours(cell).ForEach(ToggleCore);
            }

            if (cell.Type == CellType.Mine)
            {
                OnGameOver();
                isGameOver = true;

                ToggleAllCells();
            }

            isFirstStep = false;
        }

        public void Reset()
        {
            isFirstStep = true;
            isGameOver = false;

            NumberOfMarkedCells = 0;
        }
        #endregion
    }
}
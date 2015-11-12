using Minesweeper.Shared;
using System.Windows.Controls;
using System.Windows.Input;
using PostSharp.Patterns.Model;
using Minesweeper.Utils;
using Minesweeper.Gamelogic;

namespace Minesweeper.ViewModels
{
    [NotifyPropertyChanged]
    public class MainViewModel
    {
        public ICommand StartCommand { get; set; }
        public Grid GameGrid { get; set; }
        public GameController Controller { get; set; }

        public MainViewModel()
        {
            StartCommand = new DelegateCommand((obj) => StartGame());

            Controller = new GameController();
            CreateNewGame();
        }

        private void StartGame()
        {
            CreateNewGame();
        }

        private void CreateNewGame()
        {
            GameGrid = GameGridFactory.CreateGameGrid(Controller, 5);
            Controller.SetCells(GameGrid.FindCells());
        }
    }
}

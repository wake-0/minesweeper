using Minesweeper.Shared;
using System.Windows.Controls;
using System.Windows.Input;
using PostSharp.Patterns.Model;
using Minesweeper.Utils;
using Minesweeper.Gamelogic;
using System;
using System.Windows;

namespace Minesweeper.ViewModels
{
    [NotifyPropertyChanged]
    public class MainViewModel
    {
        public ICommand StartCommand { get; set; }
        public bool GameIsRunning { get; set; }

        public Grid GameGrid { get; set; }
        public GameController Controller { get; set; }

        public MainViewModel()
        {
            StartCommand = new DelegateCommand((obj) => StartGame());
            Controller = new GameController();
            Controller.GameOver += HandleGameOver;
            CreateNewGame();
        }

        private void HandleGameOver(object sender, EventArgs e)
        {
            GameIsRunning = false;
            MessageBox.Show("Game over");
        }

        private void StartGame()
        {
            CreateNewGame();
        }

        private void CreateNewGame()
        {
            GameIsRunning = true;
            GameGrid = GameGridFactory.CreateGameGrid(Controller, 5);
            Controller.SetCells(GameGrid.FindCells());
        }
    }
}

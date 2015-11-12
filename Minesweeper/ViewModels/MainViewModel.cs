using Minesweeper.Shared;
using System.Windows.Controls;
using System.Windows.Input;
using PostSharp.Patterns.Model;
using Minesweeper.Utils;
using Minesweeper.Gamelogic;
using System;
using System.Windows;
using Minesweeper.Services;

namespace Minesweeper.ViewModels
{
    [NotifyPropertyChanged]
    public class MainViewModel
    {
        public ICommand StartCommand { get; set; }
        public ICommand OpenSettingsCommand { get; set; }
        public bool GameIsRunning { get; set; }

        public Grid GameGrid { get; set; }
        public GameController Controller { get; set; }

        public GameSettings Settings { get; set; }
        public TimerService TimerService { get; set; }

        public MainViewModel()
        {
            StartCommand = new DelegateCommand((obj) => StartGame());
            OpenSettingsCommand = new DelegateCommand((obj) => OpenSettings());

            Settings = new GameSettings();
            TimerService = new TimerService();

            Controller = new GameController();
            Controller.FirstStep += HandleFirstStep;
            Controller.GameOver += HandleGameOver;
            Controller.GameWon += HandleGameWon;
            CreateNewGame();
        }

        private void HandleFirstStep(object sender, EventArgs e)
        {
            TimerService.Start();
        }

        private void HandleGameWon(object sender, EventArgs e)
        {
            GameIsRunning = false;
            TimerService.Stop();
            MessageBox.Show("Game won");
        }

        private void HandleGameOver(object sender, EventArgs e)
        {
            GameIsRunning = false;
            TimerService.Stop();
            MessageBox.Show("Game over");
        }

        private void StartGame()
        {
            CreateNewGame();
        }

        private void OpenSettings()
        {
            var settingsWindow = new SettingsWindow(Settings);
            settingsWindow.ApplyClicked += HandleApply;
            settingsWindow.ShowDialog();
        }

        private void HandleApply(object sender, EventArgs e)
        {
            CreateNewGame();
        }

        private void CreateNewGame()
        {
            GameIsRunning = true;
            GameGrid = GameGridFactory.CreateGameGrid(Controller, Settings.Size);
            Controller.Reset();
            Controller.SetCells(GameGrid.FindCells(), Settings.NumberOfMines);
        }
    }
}

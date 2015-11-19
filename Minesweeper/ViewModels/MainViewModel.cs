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
        public ICommand OpenStatisticsCommand { get; set; }
        public bool GameIsRunning { get; set; }

        public Grid GameGrid { get; set; }
        public GameController Controller { get; set; }

        public GameSettings Settings { get; set; }
        public GameStatistics Statistics { get; private set; }
        public TimerService TimerService { get; set; }

        public MainViewModel()
        {
            StartCommand = new DelegateCommand((obj) => StartGame());
            OpenSettingsCommand = new DelegateCommand((obj) => OpenSettings());
            OpenStatisticsCommand = new DelegateCommand((obj) => OpenStatistics());

            Settings = new GameSettings();
            Statistics = new GameStatistics();
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

            CreateStatistic();

            TimerService.Stop();
            MessageBox.Show("Game won");
        }

        private void CreateStatistic()
        {
            var statistic = new GameStatistic()
            {
                Time = TimerService.Time,
                Mines = Settings.NumberOfMines,
                Rows = Settings.Rows,
                Columns = Settings.Columns
            };
            Statistics.Statistics.Add(statistic);
        }

        private void HandleGameOver(object sender, EventArgs e)
        {
            GameIsRunning = false;
            TimerService.Stop();
            MessageBox.Show("Game over");
        }

        private void StartGame()
        {
            TimerService.Reset();
            CreateNewGame();
        }

        private void OpenSettings()
        {
            var settingsWindow = new SettingsWindow(Settings);
            settingsWindow.ApplyClicked += HandleApply;
            settingsWindow.ShowDialog();
        }

        private void OpenStatistics()
        {
            var statisticsWindow = new StatisticsWindow(Statistics);
            statisticsWindow.ShowDialog();
        }

        private void HandleApply(object sender, EventArgs e)
        {
            CreateNewGame();
        }

        private void CreateNewGame()
        {
            GameIsRunning = true;
            Cell[,] game = GameGridFactory.CreateGame(Settings.Rows, Settings.Columns, Settings.NumberOfMines);
            GameGrid = GameGridFactory.CreateGameGrid(Controller,game);

            Controller.Cells = game;
            Controller.NumberOfMines = Settings.NumberOfMines;

            Controller.Reset();
        }
    }
}

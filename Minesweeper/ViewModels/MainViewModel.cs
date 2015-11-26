using Minesweeper.Shared;
using System.Windows.Controls;
using System.Windows.Input;
using PostSharp.Patterns.Model;
using Minesweeper.Utils;
using Minesweeper.Gamelogic;
using System;
using System.Windows;
using Minesweeper.Services;
using System.Xml.Serialization;
using System.IO;
using System.Linq;

namespace Minesweeper.ViewModels
{
    [NotifyPropertyChanged]
    public class MainViewModel
    {
        #region Fields
        private static string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MinesweeperStatistics.xml");

        private SettingsWindow settingsWindow;
        private StatisticsWindow statisticsWindow;
        #endregion

        #region Properties
        public ICommand StartCommand { get; set; }
        public DelegateCommand OpenSettingsCommand { get; set; }
        public DelegateCommand OpenStatisticsCommand { get; set; }
        public bool GameIsRunning { get; set; }

        public Grid GameGrid { get; set; }
        public GameController Controller { get; set; }

        public GameSettings Settings { get; set; }
        public GameStatistics Statistics { get; private set; }
        public TimerService TimerService { get; set; }
        #endregion

        #region Constructor
        public MainViewModel()
        {
            StartCommand = new DelegateCommand((obj) => StartGame());
            OpenSettingsCommand = new DelegateCommand((obj) => OpenSettings(), (obj) => settingsWindow == null);
            OpenStatisticsCommand = new DelegateCommand((obj) => OpenStatistics(), (obj) => statisticsWindow == null);

            LoadStatistics();

            Settings = new GameSettings();
            TimerService = new TimerService();

            Controller = new GameController();
            Controller.FirstStep += HandleFirstStep;
            Controller.GameOver += HandleGameOver;
            Controller.GameWon += HandleGameWon;
            CreateNewGame();
        }
        #endregion

        #region Methods
        private void SaveStatistics()
        {
            if (!Statistics.Statistics.Any()) { return; }

            XmlSerializer serializer = new XmlSerializer(typeof(GameStatistics));
            using (TextWriter writer = new StreamWriter(fileName))
            {
                serializer.Serialize(writer, Statistics);
            }
        }

        private void LoadStatistics()
        {
            if (!File.Exists(fileName))
            {
                Statistics = new GameStatistics();
                return;
            }

            XmlSerializer deserializer = new XmlSerializer(typeof(GameStatistics));
            using (TextReader reader = new StreamReader(fileName))
            {
                Statistics = deserializer.Deserialize(reader) as GameStatistics;
            }
        }

        private void HandleFirstStep(object sender, EventArgs e)
        {
            TimerService.Start();
        }

        private void HandleGameWon(object sender, EventArgs e)
        {
            GameIsRunning = false;
            TimerService.Stop();
            CreateStatistic(true);

            MessageBox.Show("Game won");
        }

        private void CreateStatistic(bool won)
        {
            var statistic = new GameStatistic()
            {
                Time = TimerService.Time,
                Mines = Settings.NumberOfMines,
                Rows = Settings.Rows,
                Columns = Settings.Columns,
                HasWon = won
            };

            Statistics.Statistics.Add(statistic);
        }

        private void HandleGameOver(object sender, EventArgs e)
        {
            GameIsRunning = false;
            TimerService.Stop();
            CreateStatistic(false);

            MessageBox.Show("Game over");
        }

        private void StartGame()
        {
            TimerService.Reset();
            CreateNewGame();
        }

        private void OpenSettings()
        {
            settingsWindow = new SettingsWindow(Settings);
            settingsWindow.Closed += CloseSettingsWindow;
            settingsWindow.ApplyClicked += HandleApply;

            OpenSettingsCommand.RaiseCanExecuteChanged();

            settingsWindow.Show();
        }

        private void CloseSettingsWindow(object sender, EventArgs e)
        {
            var window = (SettingsWindow)sender;
            window.Closed -= CloseSettingsWindow;
            window.ApplyClicked -= HandleApply;
            settingsWindow = null;

            OpenSettingsCommand.RaiseCanExecuteChanged();
        }

        private void OpenStatistics()
        {
            statisticsWindow = new StatisticsWindow(Statistics);
            statisticsWindow.Closed += CloseStatisticsWindow;

            OpenStatisticsCommand.RaiseCanExecuteChanged();

            statisticsWindow.Show();
        }

        private void CloseStatisticsWindow(object sender, EventArgs e)
        {
            var window = (StatisticsWindow)sender;
            window.Closed -= CloseStatisticsWindow;
            statisticsWindow = null;

            OpenStatisticsCommand.RaiseCanExecuteChanged();
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

        internal void Shutdown()
        {
            if (settingsWindow != null) { settingsWindow.Close(); }
            if (statisticsWindow != null) { statisticsWindow.Close(); }

            SaveStatistics();
        }
        #endregion
    }
}

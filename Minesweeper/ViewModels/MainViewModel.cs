using Minesweeper.Shared;
using System.Windows.Controls;
using System.Windows.Input;
using PostSharp.Patterns.Model;
using System.Windows.Media;
using Minesweeper.Utils;

namespace Minesweeper.ViewModels
{
    [NotifyPropertyChanged]
    public class MainViewModel
    {
        public ICommand StartCommand { get; set; }
        public Grid GameGrid { get; set; }

        public MainViewModel()
        {
            StartCommand = new DelegateCommand(Start);
            //GameGrid = new Grid();
            //GameGrid.Background = Brushes.Black;

            GameGrid = GameGridFactory.CreateGameGrid(5);
        }

        private void Start(object obj)
        {
            GameGrid.Background = Brushes.Blue;
        }

    }
}

using PostSharp.Patterns.Model;
using System.Collections.ObjectModel;

namespace Minesweeper.Utils
{
    [NotifyPropertyChanged]
    public class GameStatistics
    {
        public ObservableCollection<GameStatistic> Statistics { get; private set; }

        public GameStatistics()
        {
            Statistics = new ObservableCollection<GameStatistic>();
        }
    }
}

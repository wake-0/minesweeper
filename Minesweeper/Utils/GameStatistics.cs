using PostSharp.Patterns.Model;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Xml.Serialization;

namespace Minesweeper.Utils
{
    [Serializable]
    [NotifyPropertyChanged]
    public class GameStatistics
    {
        public ObservableCollection<GameStatistic> Statistics { get; private set; }
        [XmlIgnore]
        public double WinRate { get; private set; }
        [XmlIgnore]
        public double LoseRate { get; private set; }

        public GameStatistics()
        {
            WinRate = 0;
            LoseRate = 0;

            Statistics = new ObservableCollection<GameStatistic>();
            Statistics.CollectionChanged += CollectionChanged;
        }

        private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            WinRate = (100 * Statistics.Count(s => s.HasWon)) / Statistics.Count;
            LoseRate = 100 - WinRate;
        }
    }
}

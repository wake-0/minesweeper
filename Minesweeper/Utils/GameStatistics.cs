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
        #region Fields
        private readonly int percentageFactor = 100;
        #endregion

        #region Properties
        public ObservableCollection<GameStatistic> Statistics { get; private set; }
        [XmlIgnore]
        public double WinRate { get; private set; }
        [XmlIgnore]
        public double LoseRate { get; private set; }
        #endregion

        #region Constructor
        public GameStatistics()
        {
            WinRate = 0;
            LoseRate = 0;

            Statistics = new ObservableCollection<GameStatistic>();
            Statistics.CollectionChanged += CollectionChanged;
        }
        #endregion

        #region Methods
        private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            WinRate = (percentageFactor * Statistics.Count(s => s.HasWon)) / Statistics.Count;
            LoseRate = percentageFactor - WinRate;
        }
        #endregion
    }
}

using System.Windows;
using Minesweeper.Utils;

namespace Minesweeper
{
    public partial class StatisticsWindow : Window
    {
        #region Fields
        private GameStatistics statistics;
        #endregion

        #region Constructor
        public StatisticsWindow(GameStatistics statistics)
        {
            InitializeComponent();

            this.statistics = statistics;
            DataContext = statistics;
        }
        #endregion
    }
}

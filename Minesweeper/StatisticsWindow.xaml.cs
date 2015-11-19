using System.Windows;
using Minesweeper.Utils;

namespace Minesweeper
{
    /// <summary>
    /// Interaktionslogik für StatisticsWindow.xaml
    /// </summary>
    public partial class StatisticsWindow : Window
    {
        private GameStatistics statistics;

        public StatisticsWindow(GameStatistics statistics)
        {
            InitializeComponent();

            this.statistics = statistics;
            DataContext = statistics;
        }
    }
}

using Minesweeper.Gamelogic;
using System.Windows.Controls;
using PostSharp.Patterns.Model;
using System.Windows;
using System.Windows.Input;

namespace Minesweeper.Presentation
{
    [NotifyPropertyChanged]
    public partial class CellControl : UserControl
    {
        #region Properties
        public Cell Cell { get; private set; }
        public GameController Controller { get; private set; }
        #endregion

        #region Constructor
        public CellControl(GameController controller, Cell cell)
        {
            InitializeComponent();

            Cell = cell;
            Controller = controller;
            DataContext = this;
        }
        #endregion

        #region Methods
        private void ToggleCell(object sender, RoutedEventArgs e)
        {
            Controller.OpenCell(Cell);
            e.Handled = true;
        }

        private void MarkCell(object sender, MouseButtonEventArgs e)
        {
            Controller.MarkCell(Cell);
            e.Handled = true;
        }

        private void ToggleNeighbours(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton != MouseButton.Left) { return; }

            Controller.OpenNeighbours(Cell);
            e.Handled = true;
        }
        #endregion
    }
}

using Minesweeper.Gamelogic;
using System.Windows.Controls;
using PostSharp.Patterns.Model;

namespace Minesweeper.Presentation
{
    /// <summary>
    /// Interaktionslogik für CellControl.xaml
    /// </summary>
    [NotifyPropertyChanged]
    public partial class CellControl : UserControl
    {
        public Cell Cell { get; private set; }

        public CellControl(Cell cell)
        {
            InitializeComponent();

            Cell = cell;
            DataContext = Cell;
        }
    }
}

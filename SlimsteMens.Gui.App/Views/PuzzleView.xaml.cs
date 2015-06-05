using System.Windows;
using System.Windows.Controls;

namespace SlimsteMens.Gui.App.Views
{
    /// <summary>
    /// Interaction logic for PuzzleView.xaml
    /// </summary>
    public partial class PuzzleView : UserControl
    {
        public PuzzleView()
        {
            InitializeComponent();
        }

        private void PuzzleView_OnLoaded(object sender, RoutedEventArgs e)
        {
            AudioElement.Play();
        }
    }
}

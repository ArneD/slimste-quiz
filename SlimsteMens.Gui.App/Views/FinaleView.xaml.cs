using System.Windows;
using System.Windows.Controls;

namespace SlimsteMens.Gui.App.Views
{
    /// <summary>
    /// Interaction logic for FinaleView.xaml
    /// </summary>
    public partial class FinaleView : UserControl
    {
        public FinaleView()
        {
            InitializeComponent();
        }

        private void FinaleView_OnLoaded(object sender, RoutedEventArgs e)
        {
            AudioElement.Play();
        }
    }
}

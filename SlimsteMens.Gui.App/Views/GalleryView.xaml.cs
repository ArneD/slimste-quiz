using System.Windows;
using System.Windows.Controls;

namespace SlimsteMens.Gui.App.Views
{
    /// <summary>
    /// Interaction logic for GalleryView.xaml
    /// </summary>
    public partial class GalleryView : UserControl
    {
        public GalleryView()
        {
            InitializeComponent();
        }

        private void GalleryView_OnLoaded(object sender, RoutedEventArgs e)
        {
            AudioElement.Play();
        }
    }
}

using System.Windows;
using System.Windows.Controls;
using SlimsteMens.Gui.App.ViewModels;

namespace SlimsteMens.Gui.App.Views
{
    /// <summary>
    /// Interaction logic for ThreeSixNineView.xaml
    /// </summary>
    public partial class ThreeSixNineView : UserControl
    {
        public ThreeSixNineView()
        {
            InitializeComponent();
        }

        private void ThreeSixNineView_OnLoaded(object sender, RoutedEventArgs e)
        {
            var threeSixNineViewModel = DataContext as ThreeSixNineViewModel;
            if (threeSixNineViewModel != null && !threeSixNineViewModel.IntroPlayed)
            {
                AudioElement.Play();
                threeSixNineViewModel.IntroPlayed = true;
            }
        }
    }
}

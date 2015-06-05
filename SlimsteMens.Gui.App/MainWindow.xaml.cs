using System.Windows;
using SlimsteMens.Gui.App.ViewModels;

namespace SlimsteMens.Gui.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _dataContext;

        public MainWindow(MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();
            _dataContext = mainWindowViewModel;
            DataContext = _dataContext;
        }
    }
}

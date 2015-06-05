using System.Windows;
using SlimsteMens.Gui.Admin.ViewModels;
using SlimsteMens.Gui.Admin.Views;
using SlimsteMens.Gui.Controls.ViewModels;

namespace SlimsteMens.Gui.Admin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _dataContext;
        private readonly ConfigTimersViewModel _configTimersViewModel;
        private readonly ScoreControlViewModel _scoreControlViewModel;

        public MainWindow(MainWindowViewModel mainWindowViewModel, ConfigTimersViewModel configTimersViewModel, ScoreControlViewModel scoreControlViewModel)
        {
            InitializeComponent();
            _dataContext = mainWindowViewModel;
            _configTimersViewModel = configTimersViewModel;
            _scoreControlViewModel = scoreControlViewModel;
            DataContext = _dataContext;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            new ConfigTimersWindow(_configTimersViewModel, _scoreControlViewModel).ShowDialog();
        }
    }
}

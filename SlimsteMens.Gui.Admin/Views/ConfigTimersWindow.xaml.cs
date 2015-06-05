using System.Windows;
using SlimsteMens.Gui.Admin.ViewModels;
using SlimsteMens.Gui.Controls.ViewModels;

namespace SlimsteMens.Gui.Admin.Views
{
    /// <summary>
    /// Interaction logic for ConfigTimersWindow.xaml
    /// </summary>
    public partial class ConfigTimersWindow : Window
    {
        private readonly ConfigTimersViewModel _dataContext;

        public ConfigTimersWindow(ConfigTimersViewModel configTimersViewModel, ScoreControlViewModel scoreControlViewModel)
        {
            InitializeComponent();
            _dataContext = configTimersViewModel;
            _dataContext.Team1 = scoreControlViewModel.Team1;
            _dataContext.Team2 = scoreControlViewModel.Team2;
            _dataContext.Team3 = scoreControlViewModel.Team3;

            _dataContext.Team1Points = scoreControlViewModel.Team1Seconds;
            _dataContext.Team2Points = scoreControlViewModel.Team2Seconds;
            _dataContext.Team3Points = scoreControlViewModel.Team3Seconds;

            if (scoreControlViewModel.IsTurnTeam1) _dataContext.IsTeam1Turn = true;
            else if (scoreControlViewModel.IsTurnTeam2) _dataContext.IsTeam2Turn = true;
            else _dataContext.IsTeam3Turn = true;
            DataContext = _dataContext;
        }
    }
}

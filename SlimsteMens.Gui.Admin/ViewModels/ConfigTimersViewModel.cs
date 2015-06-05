using SlimsteMens.Gui.Controls;
using SlimsteMens.Services.Info;
using SlimsteMens.Services.Interfaces;
using System.Windows.Input;

namespace SlimsteMens.Gui.Admin.ViewModels
{
    public class ConfigTimersViewModel : ViewModelBase
    {
        private readonly IAdminService _adminService;
        private string _team1;
        private string _team2;
        private string _team3;
        private int _team1Points;
        private int _team2Points;
        private int _team3Points;
        private bool _isTeam1Turn;
        private bool _isTeam2Turn;
        private bool _isTeam3Turn;

        public string Team1
        {
            get { return _team1; }
            set
            {
                _team1 = value;
                OnPropertyChanged("Team1");
            }
        }

        public string Team2
        {
            get { return _team2; }
            set
            {
                _team2 = value;
                OnPropertyChanged("Team2");
            }
        }

        public string Team3
        {
            get { return _team3; }
            set
            {
                _team3 = value;
                OnPropertyChanged("Team3");
            }
        }

        public int Team1Points
        {
            get { return _team1Points; }
            set
            {
                _team1Points = value;
                OnPropertyChanged("Team1Points");
            }
        }

        public int Team2Points
        {
            get { return _team2Points; }
            set
            {
                _team2Points = value;
                OnPropertyChanged("Team2Points");
            }
        }

        public int Team3Points
        {
            get { return _team3Points; }
            set
            {
                _team3Points = value;
                OnPropertyChanged("Team3Points");
            }
        }

        public ICommand SaveCommand { get; set; }
        public bool IsTeam1Turn
        {
            get { return _isTeam1Turn; }
            set {
                _isTeam1Turn = value; 
                if (value)
                {
                    IsTeam2Turn = false;
                    IsTeam3Turn = false;
                }
                OnPropertyChanged("IsTeam1Turn");
            }
        }

        public bool IsTeam2Turn
        {
            get { return _isTeam2Turn; }
            set
            {
                _isTeam2Turn = value;
                if (value)
                {
                    IsTeam1Turn = false;
                    IsTeam3Turn = false;
                }
                OnPropertyChanged("IsTeam2Turn");
            }
        }

        public bool IsTeam3Turn
        {
            get { return _isTeam3Turn; }
            set
            {
                _isTeam3Turn = value;
                if (value)
                {
                    IsTeam1Turn = false;
                    IsTeam2Turn = false;
                }
                OnPropertyChanged("IsTeam3Turn");
            }
        }

        public ConfigTimersViewModel(IAdminService adminService)
        {
            _adminService = adminService;
            SaveCommand = new RelayCommand(Save) {IsEnabled = true};
        }

        public void Save()
        {
            var turnType = TurnTypeInfo.Team1;
            if(IsTeam2Turn)
                turnType = TurnTypeInfo.Team2;
            if(IsTeam3Turn)
                turnType = TurnTypeInfo.Team3;

            _adminService.ResetGame(new ResetGameInfo
                                                                {
                                                                    Team1Seconds = Team1Points,
                                                                    Team2Seconds = Team2Points,
                                                                    Team3Seconds = Team3Points,
                                                                    Turn = turnType,
                                                                });
        }
    }
}

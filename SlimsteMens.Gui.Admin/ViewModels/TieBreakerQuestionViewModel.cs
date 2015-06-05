using SlimsteMens.Gui.Controls;
using SlimsteMens.Gui.Controls.ViewModels;
using SlimsteMens.Services.Info;
using System.Collections.Generic;
using System.Linq;
using SlimsteMens.Services.Interfaces;

namespace SlimsteMens.Gui.Admin.ViewModels
{
    public class TieBreakerQuestionViewModel : ViewModelBase
    {
        private readonly IAdminService _adminService;
        private ScoreControlViewModel _scoreView;
        private string _team1Text;
        private string _team2Text;
        private string _team3Text;
        private bool _team1Checked;
        private bool _team2Checked;
        private bool _team3Checked;
        private bool _isTeam1Visible;
        private bool _isTeam2Visible;
        private bool _isTeam3Visible;

        /// <summary>
        /// Gets the score view.
        /// </summary>
        /// <value>
        /// The score view.
        /// </value>
        public ScoreControlViewModel ScoreView
        {
            get { return _scoreView; }
            private set
            {
                _scoreView = value;
                OnPropertyChanged("ScoreView");
            }
        }

        public string Team1Text
        {
            get { return _team1Text; }
            set
            {
                _team1Text = value;
                OnPropertyChanged("Team1Text");
            }
        }

        public string Team2Text
        {
            get { return _team2Text; }
            set
            {
                _team2Text = value;
                OnPropertyChanged("Team2Text");
            }
        }

        public string Team3Text
        {
            get { return _team3Text; }
            set
            {
                _team3Text = value;
                OnPropertyChanged("Team3Text");
            }
        }

        public bool Team1Checked
        {
            get { return _team1Checked; }
            set
            {
                _team1Checked = value;
                if (value)
                    _selected++;
                else
                    _selected--;
                CheckStartFinaleAvailable();
                OnPropertyChanged("Team1Checked");
            }
        }

        public bool Team2Checked
        {
            get { return _team2Checked; }
            set
            {
                _team2Checked = value;
                if (value)
                    _selected++;
                else
                    _selected--;
                CheckStartFinaleAvailable();
                OnPropertyChanged("Team2Checked");
            }
        }

        public bool Team3Checked
        {
            get { return _team3Checked; }
            set
            {
                _team3Checked = value;
                if (value)
                    _selected++;
                else
                    _selected--;
                CheckStartFinaleAvailable();
                OnPropertyChanged("Team3Checked");
            }
        }

        public bool IsTeam1Visible
        {
            get { return _isTeam1Visible; }
            set
            {
                _isTeam1Visible = value;
                OnPropertyChanged("IsTeam1Visible");
            }
        }

        public bool IsTeam2Visible
        {
            get { return _isTeam2Visible; }
            set
            {
                _isTeam2Visible = value;
                OnPropertyChanged("IsTeam2Visible");
            }
        }

        public bool IsTeam3Visible
        {
            get { return _isTeam3Visible; }
            set
            {
                _isTeam3Visible = value;
                OnPropertyChanged("IsTeam3Visible");
            }
        }

        public RelayCommand StartFinaleCommand { get; private set; }

        public IList<TurnTypeInfo> Turns { get; set; }

        private int _selected;

        public TieBreakerQuestionViewModel(IAdminService adminService, ScoreControlViewModel scoreControlView, IList<TurnTypeInfo> turns)
        {
            _adminService = adminService;
            ScoreView = scoreControlView;
            Turns = turns;
            StartFinaleCommand = new RelayCommand(StartFinale) { IsEnabled = false };

            IsTeam1Visible = turns.Any(t => t == TurnTypeInfo.Team1);
            IsTeam2Visible = turns.Any(t => t == TurnTypeInfo.Team2);
            IsTeam3Visible = turns.Any(t => t == TurnTypeInfo.Team3);

            Team1Checked = !IsTeam1Visible;
            Team2Checked = !IsTeam2Visible;
            Team3Checked = !IsTeam3Visible;

            _selected = 3 - turns.Count;
            CheckStartFinaleAvailable();

            Team1Text = scoreControlView.Team1;
            Team2Text = scoreControlView.Team2;
            Team3Text = scoreControlView.Team3;
        }

        private void CheckStartFinaleAvailable()
        {
            StartFinaleCommand.IsEnabled = _selected == 2;
        }

        public void StartFinale()
        {
            var winners = new List<TurnTypeInfo>();
            if (Team1Checked) winners.Add(TurnTypeInfo.Team1);
            if (Team2Checked) winners.Add(TurnTypeInfo.Team2);
            if (Team3Checked) winners.Add(TurnTypeInfo.Team3);
            _adminService.SetTieBreakerWinner(winners);
        }
    }
}

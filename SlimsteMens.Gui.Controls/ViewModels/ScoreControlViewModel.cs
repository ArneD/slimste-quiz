using System;
using System.ComponentModel;
using SlimsteMens.Services.Info;

namespace SlimsteMens.Gui.Controls.ViewModels
{
    public class ScoreControlViewModel : INotifyPropertyChanged
    {
        private string _team1;
        private string _team2;
        private string _team3;
        private int _team1Seconds;
        private int _team2Seconds;
        private int _team3Seconds;
        private TurnTypeInfo _turn = TurnTypeInfo.Unknown;

        public TurnTypeInfo Turn
        {
            get { return _turn; }
            set
            {
                _turn = value;
                OnPropertyChanged("IsTurnTeam1");
                OnPropertyChanged("IsTurnTeam2");
                OnPropertyChanged("IsTurnTeam3");
            }
        }

        public bool IsTurnTeam1
        {
            get { return Turn == TurnTypeInfo.Team1; }
        }

        public bool IsTurnTeam2
        {
            get { return Turn == TurnTypeInfo.Team2; }
        }

        public bool IsTurnTeam3
        {
            get { return Turn == TurnTypeInfo.Team3; }
        }

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

        public int Team1Seconds
        {
            get { return _team1Seconds; }
            set
            {
                _team1Seconds = value;
                OnPropertyChanged("Team1Seconds");
            }
        }

        public int Team2Seconds
        {
            get { return _team2Seconds; }
            set
            {
                _team2Seconds = value;
                OnPropertyChanged("Team2Seconds");
            }
        }

        public int Team3Seconds
        {
            get { return _team3Seconds; }
            set
            {
                _team3Seconds = value;
                OnPropertyChanged("Team3Seconds");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public void SetSecondsByTurn(TurnTypeInfo turn, int seconds)
        {
            switch (turn)
            {
                case TurnTypeInfo.Team1:
                    Team1Seconds = seconds;
                    break;
                case TurnTypeInfo.Team2:
                    Team2Seconds = seconds;
                    break;
                case TurnTypeInfo.Team3:
                    Team3Seconds = seconds;
                    break;
                default:
                    throw new ArgumentNullException();
            }
        }
    }
}

using System;
using System.ComponentModel;
using SlimsteMens.Services.Info;

namespace SlimsteMens.Gui.Controls.ViewModels
{
    public class FinalScoreControlViewModel : INotifyPropertyChanged
    {
        private string _team1;
        private string _team2;
        private int _team1Seconds;
        private int _team2Seconds;
        private FinaleTurnTypeInfo _turn = FinaleTurnTypeInfo.Unknown;

        public FinaleTurnTypeInfo Turn
        {
            get { return _turn; }
            set
            {
                _turn = value;
                OnPropertyChanged("IsTurnTeam1");
                OnPropertyChanged("IsTurnTeam2");
            }
        }

        public bool IsTurnTeam1
        {
            get { return Turn == FinaleTurnTypeInfo.Team1; }
        }

        public bool IsTurnTeam2
        {
            get { return Turn == FinaleTurnTypeInfo.Team2; }
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

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public void SetSecondsByTurn(FinaleTurnTypeInfo turn, int seconds)
        {
            switch (turn)
            {
                case FinaleTurnTypeInfo.Team1:
                    Team1Seconds = seconds;
                    break;
                case FinaleTurnTypeInfo.Team2:
                    Team2Seconds = seconds;
                    break;
                default:
                    throw new ArgumentNullException();
            }
        }
    }
}

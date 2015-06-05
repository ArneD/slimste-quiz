using System;
using System.Collections.Generic;

namespace SlimsteMens.Model.Entities
{
    public class Finale
    {
        private int _timeTeam2;
        private int _timeTeam1;
        public string Team1 { get; set; }
        public string Team2 { get; set; }

        public int TimeTeam1
        {
            get { return _timeTeam1; }
            set
            {
                _timeTeam1 = value;
                if (TimeTeam1 < 0) _timeTeam1 = 0;
            }
        }

        public int TimeTeam2
        {
            get { return _timeTeam2; }
            set
            {
                _timeTeam2 = value;
                if (TimeTeam2 < 0) _timeTeam2 = 0;
            }
        }

        public FinaleTurnType Turn { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Finale" /> class.
        /// </summary>
        /// <param name="team1">The team1.</param>
        /// <param name="team2">The team2.</param>
        /// <param name="team1Seconds">The team1 seconds.</param>
        /// <param name="team2Seconds">The team2 seconds.</param>
        internal Finale(string team1, string team2, int team1Seconds, int team2Seconds)
        {
            Team1 = team1;
            Team2 = team2;

            TimeTeam1 = team1Seconds;
            TimeTeam2 = team2Seconds;

            Turn = team1Seconds < team2Seconds ? FinaleTurnType.Team1 : FinaleTurnType.Team2;
        }

        public void NextTurn()
        {
            switch (Turn)
            {
                case FinaleTurnType.Team1:
                    Turn = FinaleTurnType.Team2;
                    break;
                default:
                    Turn = FinaleTurnType.Team1;
                    break;
            }
        }

        public int TimeByTurn
        {
            get
            {
                switch (Turn)
                {
                    case FinaleTurnType.Team1:
                        return TimeTeam1;
                    case FinaleTurnType.Team2:
                        return TimeTeam2;
                    default:
                        throw new NotImplementedException();
                }
            }
        }

        public void RemoveTime(int time)
        {
            switch (Turn)
            {
                case FinaleTurnType.Team1:
                    TimeTeam2 -= time;
                    
                    break;
                case FinaleTurnType.Team2:
                    TimeTeam1 -= time;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        public FinaleTurnType NextAndSetTurnByLowestTime(IList<FinaleTurnType> excludeTurns)
        {
            var turn = FinaleTurnType.Unknown;

            if (excludeTurns == null)
                excludeTurns = new List<FinaleTurnType>();

            if ((!excludeTurns.Contains(FinaleTurnType.Team1) && (TimeTeam1 <= TimeTeam2)) || excludeTurns.Contains(FinaleTurnType.Team2))
            {
                turn = FinaleTurnType.Team1;
            }
            else if ((!excludeTurns.Contains(FinaleTurnType.Team2) && (TimeTeam2 <= TimeTeam1)) || excludeTurns.Contains(FinaleTurnType.Team1))
            {
                turn = FinaleTurnType.Team2;
            }


            if (turn == FinaleTurnType.Unknown)
                throw new NotImplementedException();
            else
            {
                Turn = turn;
                return turn;
            }
        }
    }
}

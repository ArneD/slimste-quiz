using System;
using System.Collections.Generic;

namespace SlimsteMens.Model.Entities
{
    public class Game
    {
        public string Team1 { get; set; }
        public string Team2 { get; set; }
        public string Team3 { get; set; }

        public int TimeTeam1 { get; set; }
        public int TimeTeam2 { get; set; }
        public int TimeTeam3 { get; set; }

        public TurnType Turn { get; set; }
        public IList<RoundType> Rounds { get; private set; }
        private int _roundIndex = -1;

        public Game(string team1, string team2, string team3, int startSeconds, IList<RoundType> rounds)
        {
            if (rounds == null) throw new ArgumentNullException("rounds");

            Team1 = team1;
            Team2 = team2;
            Team3 = team3;
            TimeTeam1 = startSeconds;
            TimeTeam2 = startSeconds;
            TimeTeam3 = startSeconds;
            Rounds = rounds;

            Turn = TurnType.Team1;
        }

        public void NextTurn()
        {
            switch (Turn)
            {
                case TurnType.Team1:
                    Turn = TurnType.Team2;
                    break;
                case TurnType.Team2:
                    Turn = TurnType.Team3;
                    break;
                default:
                    Turn = TurnType.Team1;
                    break;
            }
        }

        public RoundType NextRound()
        {
            _roundIndex++;
            if(Rounds.Count > _roundIndex)
                return Rounds[_roundIndex];
            return RoundType.Finale;
        }

        public int TimeByTurn
        {
            get
            {
                switch (Turn)
                {
                    case TurnType.Team1:
                        return TimeTeam1;
                    case TurnType.Team2:
                        return TimeTeam2;
                    case TurnType.Team3:
                        return TimeTeam3;
                    default:
                        throw new NotImplementedException();
                }
            }
        }

        public void AddTime(int time)
        {
            switch (Turn)
            {
                case TurnType.Team1:
                    TimeTeam1 += time;
                    break;
                case TurnType.Team2:
                    TimeTeam2 += time;
                    break;
                case TurnType.Team3:
                    TimeTeam3 += time;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        public TurnType NextAndSetTurnByLowestTime(IList<TurnType> excludeTurns)
        {
            var turn = TurnType.Unknown;

            if(excludeTurns == null)
                excludeTurns = new List<TurnType>();

            if (!excludeTurns.Contains(TurnType.Team1) && (TimeTeam1 <= TimeTeam2 && TimeTeam1 <= TimeTeam3
                || (excludeTurns.Contains(TurnType.Team2) && excludeTurns.Contains(TurnType.Team3))
                || (excludeTurns.Contains(TurnType.Team2) && TimeTeam1 <= TimeTeam3)
                || (excludeTurns.Contains(TurnType.Team3) && TimeTeam1 <= TimeTeam2)))
            {
                turn = TurnType.Team1;
            }
            else if (!excludeTurns.Contains(TurnType.Team2) && (TimeTeam2 <= TimeTeam1 && TimeTeam2 <= TimeTeam3
                || (excludeTurns.Contains(TurnType.Team1) && excludeTurns.Contains(TurnType.Team3))
                || (excludeTurns.Contains(TurnType.Team1) && TimeTeam2 <= TimeTeam3)
                || (excludeTurns.Contains(TurnType.Team3) && TimeTeam2 <= TimeTeam1)))
            {
                turn = TurnType.Team2;
            }
            else if (!excludeTurns.Contains(TurnType.Team3) && (TimeTeam3 <= TimeTeam1 && TimeTeam3 <= TimeTeam2
                || (excludeTurns.Contains(TurnType.Team2) && excludeTurns.Contains(TurnType.Team1))
                || (excludeTurns.Contains(TurnType.Team1) && TimeTeam3 <= TimeTeam2)
                || (excludeTurns.Contains(TurnType.Team2) && TimeTeam3 <= TimeTeam1)))
            {
                turn = TurnType.Team3;
            }


            
            if(turn == TurnType.Unknown)
                throw new NotImplementedException();
            else
            {
                Turn = turn;
                return turn;
            }
        }

        public IList<TurnType> TeamsTiebreakQuestionNeeded()
        {
            if(TimeTeam1 == TimeTeam2 && TimeTeam1 == TimeTeam3)
            {
                return new List<TurnType>{TurnType.Team1, TurnType.Team2, TurnType.Team3};
            }
            else if((TimeTeam1 == TimeTeam2 && TimeTeam1 < TimeTeam3))
            {
                return new List<TurnType>{TurnType.Team1, TurnType.Team2};
            }
            else if((TimeTeam1 == TimeTeam3 && TimeTeam1 < TimeTeam2))
            {
                return new List<TurnType>{TurnType.Team1, TurnType.Team3};
            }
            else if ((TimeTeam2 == TimeTeam3 && TimeTeam2 < TimeTeam1))
            {
                return new List<TurnType> { TurnType.Team2, TurnType.Team3 };
            }
            else
                return new List<TurnType>();
        }

        public Finale RetrieveFinale()
        {
            if (TeamsTiebreakQuestionNeeded().Count > 0)
                return null;

            if(TimeTeam1 < TimeTeam2 && TimeTeam1 < TimeTeam3)
                return new Finale(Team2, Team3, TimeTeam2, TimeTeam3);
            else if(TimeTeam2 < TimeTeam1 && TimeTeam2 < TimeTeam3)
                return new Finale(Team1, Team3, TimeTeam1, TimeTeam3);
            else //if (TimeTeam3 < TimeTeam1 && TimeTeam3 < TimeTeam2)
                return new Finale(Team1, Team2, TimeTeam1, TimeTeam2);
        }

        public Finale RetrieveFinale(IList<TurnType> turns)
        {
            if (turns.Count != 2)
                return null;

            if(turns.Contains(TurnType.Team1) && turns.Contains(TurnType.Team2))
                return new Finale(Team1, Team2, TimeTeam1, TimeTeam2);
            else if (turns.Contains(TurnType.Team1) && turns.Contains(TurnType.Team3))
                return new Finale(Team1, Team3, TimeTeam1, TimeTeam3);
            else //if (turns.Contains(TurnType.Team2) && turns.Contains(TurnType.Team3))
                return new Finale(Team2, Team3, TimeTeam2, TimeTeam3);
        }
    }
}

using System.Collections.Generic;
using SlimsteMens.Services.SlimsteAdminService;

namespace SlimsteMens.Services.Info
{
    public class GameInfo
    {
        public string Team1 { get; set; }
        public string Team2 { get; set; }
        public string Team3 { get; set; }

        public int StartSeconds { get; set; }

        public GameInfo(GameDto dto) : this()
        {
            Team1 = dto.Team1;
            Team2 = dto.Team2;
            Team3 = dto.Team3;

            StartSeconds = dto.StartingSeconds;
        }

        public GameInfo()
        {}
        
        public GameDto CreateDto()
        {
            return new GameDto
                {
                    Team1 = Team1,
                    Team2 = Team2,
                    Team3 = Team3,
                    StartingSeconds = StartSeconds,
                    Rounds = new List<RoundTypeDto>{RoundTypeDto.ThreeSixNine, RoundTypeDto.Puzzle, RoundTypeDto.Gallery, RoundTypeDto.Video},
                };
        }
    }
}

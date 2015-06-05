using SlimsteMens.Services.SlimsteAdminService;

namespace SlimsteMens.Services.Info
{
    public class FinaleInfo
    {
        public string Team1 { get; set; }
        public string Team2 { get; set; }
        
        public int Team1Seconds { get; set; }
        public int Team2Seconds { get; set; }

        public FinaleInfo(){}

        public FinaleInfo(FinaleDto finaleDto)
        {
            Team1 = finaleDto.Team1;
            Team1Seconds = finaleDto.Team1Seconds;
            Team2 = finaleDto.Team2;
            Team2Seconds = finaleDto.Team2Seconds;
        }

        public FinaleDto CreateDto()
        {
            return new FinaleDto
                       {
                           Team1 = Team1,
                           Team2 = Team2,
                           Team1Seconds = Team1Seconds,
                           Team2Seconds = Team2Seconds,
                       };
        }
    }
}

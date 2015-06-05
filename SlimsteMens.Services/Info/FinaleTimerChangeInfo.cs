using SlimsteMens.Services.SlimsteAdminService;

namespace SlimsteMens.Services.Info
{
    public class FinaleTimerChangeInfo
    {
        public FinaleTurnTypeInfo Turn { get; private set; }
        public int Seconds { get; private set; }

        public FinaleTimerChangeInfo(FinaleTurnTypeDto turnType, int seconds)
        {
            Turn = turnType.ToInfo();
            Seconds = seconds;
        }
    }
}

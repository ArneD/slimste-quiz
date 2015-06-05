using SlimsteMens.Services.SlimsteAdminService;

namespace SlimsteMens.Services.Info
{
    public class TimerChangeInfo
    {
        public TurnTypeInfo Turn { get; private set; }
        public int Seconds { get; private set; }

        public TimerChangeInfo(TurnTypeDto turnType, int seconds)
        {
            Turn = turnType.ToInfo();
            Seconds = seconds;
        }
    }
}

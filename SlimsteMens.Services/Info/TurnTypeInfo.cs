using SlimsteMens.Services.SlimsteAdminService;

namespace SlimsteMens.Services.Info
{
    public enum TurnTypeInfo
    {
        Unknown = 0,
        Team1 = 1,
        Team2 = 2,
        Team3 = 3,
    }

    public enum FinaleTurnTypeInfo
    {
        Unknown = 0,
        Team1 = 1,
        Team2 = 2,
    }

    public static class TurnTypeExtensions
    {
        public static TurnTypeInfo ToInfo(this TurnTypeDto turnType)
        {
            switch (turnType)
            {
                case TurnTypeDto.Team1:
                    return TurnTypeInfo.Team1;
                case TurnTypeDto.Team2:
                    return TurnTypeInfo.Team2;
                case TurnTypeDto.Team3:
                    return TurnTypeInfo.Team3;
                default:
                    return TurnTypeInfo.Unknown;
            }
        }

        public static TurnTypeDto ToDto(this TurnTypeInfo turnType)
        {
            switch (turnType)
            {
                case TurnTypeInfo.Team1:
                    return TurnTypeDto.Team1;
                case TurnTypeInfo.Team2:
                    return TurnTypeDto.Team2;
                case TurnTypeInfo.Team3:
                    return TurnTypeDto.Team3;
                default:
                    return TurnTypeDto.Unknown;
            }
        }

        public static FinaleTurnTypeInfo ToInfo(this FinaleTurnTypeDto turnType)
        {
            switch (turnType)
            {
                case FinaleTurnTypeDto.Team1:
                    return FinaleTurnTypeInfo.Team1;
                case FinaleTurnTypeDto.Team2:
                    return FinaleTurnTypeInfo.Team2;
                default:
                    return FinaleTurnTypeInfo.Unknown;
            }
        }

        public static FinaleTurnTypeDto ToDto(this FinaleTurnTypeInfo turnType)
        {
            switch (turnType)
            {
                case FinaleTurnTypeInfo.Team1:
                    return FinaleTurnTypeDto.Team1;
                case FinaleTurnTypeInfo.Team2:
                    return FinaleTurnTypeDto.Team2;
                default:
                    return FinaleTurnTypeDto.Unknown;
            }
        }
    }
}

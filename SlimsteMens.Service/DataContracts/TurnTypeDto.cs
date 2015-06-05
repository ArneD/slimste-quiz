using System.Runtime.Serialization;
using SlimsteMens.Model.Entities;

namespace SlimsteMens.Service.DataContracts
{
    [DataContract]
    public enum TurnTypeDto
    {
        [EnumMember]
        Unknown = 0,
        [EnumMember]
        Team1 = 1,
        [EnumMember]
        Team2 = 2,
        [EnumMember]
        Team3 = 3,
    }

    [DataContract]
    public enum FinaleTurnTypeDto
    {
        [EnumMember]
        Unknown = 0,
        [EnumMember]
        Team1 = 1,
        [EnumMember]
        Team2 = 2,
    }

    public static class TurnTypeExtensions
    {
        public static TurnTypeDto ToDto(this TurnType turnType)
        {
            switch (turnType)
            {
                case TurnType.Team1:
                    return TurnTypeDto.Team1;
                case TurnType.Team2:
                    return TurnTypeDto.Team2;
                case TurnType.Team3:
                    return TurnTypeDto.Team3;
                default:
                    return TurnTypeDto.Unknown;
            }
        }

        public static TurnType ToModel(this TurnTypeDto turnTypeDto)
        {
            switch (turnTypeDto)
            {
                case TurnTypeDto.Team1:
                    return TurnType.Team1;
                case TurnTypeDto.Team2:
                    return TurnType.Team2;
                case TurnTypeDto.Team3:
                    return TurnType.Team3;
                default:
                    return TurnType.Unknown;  
            }
        }

        public static FinaleTurnTypeDto ToDto(this FinaleTurnType turnType)
        {
            switch (turnType)
            {
                case FinaleTurnType.Team1:
                    return FinaleTurnTypeDto.Team1;
                case FinaleTurnType.Team2:
                    return FinaleTurnTypeDto.Team2;
                default:
                    return FinaleTurnTypeDto.Unknown;
            }
        }

        public static FinaleTurnType ToModel(this FinaleTurnTypeDto turnTypeDto)
        {
            switch (turnTypeDto)
            {
                case FinaleTurnTypeDto.Team1:
                    return FinaleTurnType.Team1;
                case FinaleTurnTypeDto.Team2:
                    return FinaleTurnType.Team2;
                default:
                    return FinaleTurnType.Unknown;
            }
        }
    }
}

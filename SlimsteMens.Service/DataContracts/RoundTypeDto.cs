using System.Runtime.Serialization;
using SlimsteMens.Model.Entities;

namespace SlimsteMens.Service.DataContracts
{
    [DataContract]
    public enum RoundTypeDto
    {
        [EnumMember]
        Unknown = 0,
        [EnumMember]
        ThreeSixNine = 1,
        [EnumMember]
        Puzzle = 2,
        [EnumMember]
        Lists = 3,
        [EnumMember]
        Gallery = 4,
        [EnumMember]
        Video = 5,
        [EnumMember]
        Finale = 6,
    }

    public static class RoundTypeExtensions
    {
        public static RoundType ToModel(this RoundTypeDto roundTypeDto)
        {
            switch (roundTypeDto)
            {
                case RoundTypeDto.Finale:
                    return RoundType.Finale;
                case RoundTypeDto.Gallery:
                    return RoundType.Gallery;
                case RoundTypeDto.Lists:
                    return RoundType.Lists;
                case RoundTypeDto.Puzzle:
                    return RoundType.Puzzle;
                case RoundTypeDto.ThreeSixNine:
                    return RoundType.ThreeSixNine;
                case RoundTypeDto.Video:
                    return RoundType.Video;
                default:
                    return RoundType.Unknown;
            }
        }

        public static RoundTypeDto ToDto(this RoundType roundType)
        {
            switch (roundType)
            {
                case RoundType.Finale:
                    return RoundTypeDto.Finale;
                case RoundType.Gallery:
                    return RoundTypeDto.Gallery;
                case RoundType.Lists:
                    return RoundTypeDto.Lists;
                case RoundType.Puzzle:
                    return RoundTypeDto.Puzzle;
                case RoundType.ThreeSixNine:
                    return RoundTypeDto.ThreeSixNine;
                case RoundType.Video:
                    return RoundTypeDto.Video;
                default:
                    return RoundTypeDto.Unknown;
            }
        }
    }
}

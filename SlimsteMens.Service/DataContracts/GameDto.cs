using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SlimsteMens.Service.DataContracts
{
    [DataContract]
    public class GameDto
    {
        [DataMember]
        public string Team1 { get; set; }
        [DataMember]
        public string Team2 { get; set; }
        [DataMember]
        public string Team3 { get; set; }
        [DataMember]
        public int StartingSeconds { get; set; }

        [DataMember]
        public List<RoundTypeDto> Rounds { get; set; }
    }
}

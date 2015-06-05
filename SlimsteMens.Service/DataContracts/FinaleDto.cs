using System.Runtime.Serialization;
using SlimsteMens.Model.Entities;

namespace SlimsteMens.Service.DataContracts
{
    [DataContract]
    public class FinaleDto
    {
        [DataMember]
        public string Team1 { get; set; }
        [DataMember]
        public string Team2 { get; set; }
        [DataMember]
        public int Team1Seconds { get; set; }
        [DataMember]
        public int Team2Seconds { get; set; }

        public FinaleDto(){}

        public FinaleDto(Finale finale)
        {
            Team1 = finale.Team1;
            Team1Seconds = finale.TimeTeam1;
            Team2 = finale.Team2;
            Team2Seconds = finale.TimeTeam2;
        }
    }
}

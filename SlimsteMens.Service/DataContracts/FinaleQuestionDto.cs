using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using SlimsteMens.Model.Entities;
using SlimsteMens.Model.Factories;

namespace SlimsteMens.Service.DataContracts
{
    [DataContract]
    public class FinaleQuestionDto
    {
        [DataMember]
        public long Id { get; set; }

        [DataMember]
        public string Question { get; set; }

        [DataMember]
        public string Answer1 { get; set; }

        [DataMember]
        public string Answer2 { get; set; }

        [DataMember]
        public string Answer3 { get; set; }

        [DataMember]
        public string Answer4 { get; set; }

        [DataMember]
        public string Answer5 { get; set; }

        [DataMember]
        public bool Played { get; set; }

        public FinaleQuestionDto(){}

        public FinaleQuestionDto(FinaleQuestion finaleQuestion)
        {
            Id = finaleQuestion.Id;
            Question = finaleQuestion.Question;
            Answer1 = finaleQuestion.Answer1;
            Answer2 = finaleQuestion.Answer2;
            Answer3 = finaleQuestion.Answer3;
            Answer4 = finaleQuestion.Answer4;
            Answer5 = finaleQuestion.Answer5;
        }

        public FinaleQuestion ToModel()
        {
            return FinaleQuestionFactory.CreateFinaleQuestion(Question, Answer1, Answer2, Answer3, Answer4, Answer5);
        }
    }
}

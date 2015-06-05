using System.Runtime.Serialization;
using SlimsteMens.Model.Entities;
using SlimsteMens.Model.Factories;

namespace SlimsteMens.Service.DataContracts
{
    [DataContract]
    public class ThreeSixNineQuestionDto
    {
        public static ThreeSixNineQuestionDto Create(ThreeSixNineQuestion threeSixNineQuestion)
        {
            return new ThreeSixNineQuestionDto
                {
                    Question = threeSixNineQuestion.Question,
                    Answer = threeSixNineQuestion.Answer,
                    Photo = threeSixNineQuestion.Photo,
                };
        }

        public ThreeSixNineQuestion ToModel()
        {
            return ThreeSixNineQuestionFactory.CreateThreeSixNineQuestion(Question, Answer, Photo);
        }

        [DataMember]
        public string Question { get; set; }
        [DataMember]
        public string Answer { get; set; }
        [DataMember]
        public byte[] Photo { get; set; }
    }
}

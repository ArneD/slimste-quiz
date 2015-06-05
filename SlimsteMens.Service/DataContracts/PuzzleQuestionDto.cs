using System.Runtime.Serialization;
using SlimsteMens.Model.Entities;
using SlimsteMens.Model.Factories;

namespace SlimsteMens.Service.DataContracts
{
    [DataContract]
    public class PuzzleQuestionDto
    {
        public static PuzzleQuestionDto Create(PuzzleQuestion puzzleQuestion)
        {
            return new PuzzleQuestionDto
                {
                    Hint1 = puzzleQuestion.Hint1,
                    Hint2 = puzzleQuestion.Hint2,
                    Hint3 = puzzleQuestion.Hint3,
                    Hint4 = puzzleQuestion.Hint4,
                    Answer = puzzleQuestion.Answer,
                    Id = puzzleQuestion.Id,
                };
        }

        public PuzzleQuestion ToModel()
        {
            return PuzzleQuestionFactory.CreatePuzzleQuestion(Hint1, Hint2, Hint3, Hint4, Answer);
        }

        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public string Hint1 { get; set; }
        [DataMember]
        public string Hint2 { get; set; }
        [DataMember]
        public string Hint3 { get; set; }
        [DataMember]
        public string Hint4 { get; set; }
        [DataMember]
        public string Answer { get; set; }
    }
}

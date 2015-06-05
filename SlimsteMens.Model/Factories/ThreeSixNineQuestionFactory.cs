using SlimsteMens.Model.Entities;

namespace SlimsteMens.Model.Factories
{
    public static class ThreeSixNineQuestionFactory
    {
        public static ThreeSixNineQuestion CreateThreeSixNineQuestion(string question, string answer, byte[] photo)
        {
            var question369 = CreateThreeSixNineQuestion(question, answer);
            question369.Photo = photo;
            return question369;
        }

        public static ThreeSixNineQuestion CreateThreeSixNineQuestion(string question, string answer)
        {
            return new ThreeSixNineQuestion(question, answer);
        }
    }
}

using SlimsteMens.Model.Entities;

namespace SlimsteMens.Model.Factories
{
    public static class FinaleQuestionFactory
    {
        public static FinaleQuestion CreateFinaleQuestion(string question, string answer1, string answer2,
            string answer3, string answer4, string answer5)
        {
            return new FinaleQuestion(question, answer1, answer2, answer3, answer4, answer5);
        }
    }
}

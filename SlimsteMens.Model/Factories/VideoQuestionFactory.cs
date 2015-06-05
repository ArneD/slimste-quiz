using SlimsteMens.Model.Entities;

namespace SlimsteMens.Model.Factories
{
    public static class VideoQuestionFactory
    {
        public static VideoQuestion CreateVideoQuestion(string question, string video, string answer1,
            string answer2, string answer3, string answer4, string answer5)
        {
            return new VideoQuestion(question, video, answer1, answer2, answer3, answer4, answer5);
        }
    }
}

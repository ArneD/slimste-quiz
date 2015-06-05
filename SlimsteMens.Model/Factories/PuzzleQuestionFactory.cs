using SlimsteMens.Model.Entities;

namespace SlimsteMens.Model.Factories
{
    public static class PuzzleQuestionFactory
    {
        public static PuzzleQuestion CreatePuzzleQuestion(string hint1, string hint2, string hint3, string hint4, string answer)
        {
            return new PuzzleQuestion(hint1, hint2, hint3, hint4, answer);
        }
    }
}

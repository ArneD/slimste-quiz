using System.Collections.Generic;
using SlimsteMens.Model.Entities;

namespace SlimsteMens.Model.Factories
{
    public static class PuzzleFactory
    {
        public static Puzzle CreatePuzzle(IList<PuzzleQuestion> puzzleQuestions)
        {
            return new Puzzle(puzzleQuestions);
        }
    }
}

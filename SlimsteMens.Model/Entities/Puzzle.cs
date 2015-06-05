using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SlimsteMens.Model.Entities
{
    public class Puzzle : EntityBase
    {
        public virtual ICollection<PuzzleQuestion> PuzzleQuestions { get; protected set; }

        internal protected Puzzle(IList<PuzzleQuestion> puzzleQuestions) : this()
        {
            if (puzzleQuestions == null) 
                throw new ArgumentNullException("puzzleQuestions");

            PuzzleQuestions =new Collection<PuzzleQuestion>(puzzleQuestions);
        }

        protected Puzzle(){}
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlimsteMens.Services.SlimsteAdminService;

namespace SlimsteMens.Services.Info
{
    /// <summary>
    /// Info puzzle question
    /// </summary>
    public class PuzzleQuestionInfo
    {
        public string Hint1 { get; set; }
        public string Hint2 { get; set; }
        public string Hint3 { get; set; }
        public string Hint4 { get; set; }
        public string Answer { get; set; }
        public long Id { get; set; }

        public PuzzleQuestionInfo() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PuzzleQuestionInfo" /> class.
        /// </summary>
        /// <param name="puzzleDto">The puzzle dto.</param>
        public PuzzleQuestionInfo(PuzzleQuestionDto puzzleDto)
        {
            Hint1 = puzzleDto.Hint1;
            Hint2 = puzzleDto.Hint2;
            Hint3 = puzzleDto.Hint3;
            Hint4 = puzzleDto.Hint4;
            Answer = puzzleDto.Answer;
            Id = puzzleDto.Id;
        }

        /// <summary>
        /// Creates the dto.
        /// </summary>
        /// <returns></returns>
        public PuzzleQuestionDto CreateDto()
        {
            return new PuzzleQuestionDto 
            { 
                Hint1 = Hint1,
                Hint2 = Hint2,
                Hint3 = Hint3,
                Hint4 = Hint4,
                Answer = Answer,
                Id = Id,
            };
        }
    }
}

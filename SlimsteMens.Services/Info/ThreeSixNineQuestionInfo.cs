using SlimsteMens.Services.SlimsteAdminService;

namespace SlimsteMens.Services.Info
{
    public class ThreeSixNineQuestionInfo
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public byte[] Photo { get; set; }

        public ThreeSixNineQuestionInfo(){}

        public ThreeSixNineQuestionInfo(ThreeSixNineQuestionDto threeSixNineQuestionDto)
        {
            Question = threeSixNineQuestionDto.Question;
            Answer = threeSixNineQuestionDto.Answer;
            Photo = threeSixNineQuestionDto.Photo;
        }

        public ThreeSixNineQuestionDto CreateDto()
        {
            return new ThreeSixNineQuestionDto
                {
                    Answer = Answer,
                    Question = Question,
                    Photo = Photo,
                };
        }
    }
}

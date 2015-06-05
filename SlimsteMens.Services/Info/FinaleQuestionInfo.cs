using SlimsteMens.Services.SlimsteAdminService;

namespace SlimsteMens.Services.Info
{
    public class FinaleQuestionInfo
    {
        public long Id { get; set; }
        public string Question { get; set; }
        public string Answer1 { get; set; }
        public string Answer2 { get; set; }
        public string Answer3 { get; set; }
        public string Answer4 { get; set; }
        public string Answer5 { get; set; }

        public FinaleQuestionInfo(){}

        public FinaleQuestionInfo(FinaleQuestionDto finaleQuestionDto)
        {
            Id = finaleQuestionDto.Id;
            Question = finaleQuestionDto.Question;
            Answer1 = finaleQuestionDto.Answer1;
            Answer2 = finaleQuestionDto.Answer2;
            Answer3 = finaleQuestionDto.Answer3;
            Answer4 = finaleQuestionDto.Answer4;
            Answer5 = finaleQuestionDto.Answer5;
        }

        public FinaleQuestionDto CreateDto()
        {
            return new FinaleQuestionDto
                       {
                           Id = Id, 
                           Answer1 = Answer1,
                           Answer2 = Answer2,
                           Answer3 = Answer3,
                           Answer4 = Answer4,
                           Answer5 = Answer5,
                           Question = Question,
                       };
        }
    }
}

using SlimsteMens.Services.SlimsteAdminService;

namespace SlimsteMens.Services.Info
{
    public class VideoQuestionInfo
    {
        public long Id { get; set; }
        public string Question { get; set; }
        public string VideoPath { get; set; }
        public string Answer1 { get; set; }
        public string Answer2 { get; set; }
        public string Answer3 { get; set; }
        public string Answer4 { get; set; }
        public string Answer5 { get; set; }

        public VideoQuestionInfo()
        {}

        public VideoQuestionInfo(VideoDto videoDto)
        {
            Id = videoDto.Id;
            Question = videoDto.Question;
            VideoPath = videoDto.VideoPath;
            Answer1 = videoDto.Answer1;
            Answer2 = videoDto.Answer2;
            Answer3 = videoDto.Answer3;
            Answer4 = videoDto.Answer4;
            Answer5 = videoDto.Answer5;
        }

        public VideoDto CreateDto()
        {
            return new VideoDto
                {
                    Answer1 = Answer1,
                    Answer2 = Answer2,
                    Answer3 = Answer3,
                    Answer4 = Answer4,
                    Answer5 = Answer5,
                    Question = Question,
                    VideoPath = VideoPath,
                    Id = Id,
                };
        }
    }
}

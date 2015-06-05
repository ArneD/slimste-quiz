using System.Runtime.Serialization;
using SlimsteMens.Model.Entities;
using SlimsteMens.Model.Factories;

namespace SlimsteMens.Service.DataContracts
{
    [DataContract]
    public class VideoDto
    {
        [DataMember]
        public string VideoPath { get; set; }

        [DataMember]
        public long Id { get; set; }

        [DataMember]
        public string Answer1 { get; set; }

        [DataMember]
        public string Answer2 { get; set; }

        [DataMember]
        public string Answer3 { get; set; }

        [DataMember]
        public string Answer4 { get; set; }

        [DataMember]
        public string Answer5 { get; set; }

        [DataMember]
        public string Question { get; set; }

        /// <summary>
        /// Creates the dto.
        /// </summary>
        /// <param name="video">The video.</param>
        /// <returns></returns>
        public static VideoDto CreateDto(VideoQuestion video)
        {
            return new VideoDto
                {
                    Answer1 = video.Answer1,
                    Answer2 = video.Answer2,
                    Answer3 = video.Answer3,
                    Answer4 = video.Answer4,
                    Answer5 = video.Answer5,
                    Id = video.Id,
                    VideoPath = video.Video,
                    Question = video.Question,
                };
        }

        /// <summary>
        /// To the model.
        /// </summary>
        /// <returns></returns>
        public VideoQuestion ToModel()
        {
            return VideoQuestionFactory.CreateVideoQuestion(Question, VideoPath, Answer1, Answer2, Answer3, Answer4, Answer5);
        }
    }
}

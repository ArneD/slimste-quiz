using System.Runtime.Serialization;
using SlimsteMens.Model.Entities;
using SlimsteMens.Model.Factories;

namespace SlimsteMens.Service.DataContracts
{
    [DataContract]
    public class GalleryQuestionDto
    {
        [DataMember]
        public string Answer { get; set; }

        [DataMember]
        public byte[] Photo { get; set; }

        [DataMember]
        public byte[] PhotoAnswer { get; set; }

        public static GalleryQuestionDto CreateDto(GalleryQuestion galleryQuestion)
        {
            return new GalleryQuestionDto
                {
                    Answer = galleryQuestion.Answer,
                    Photo = galleryQuestion.Photo,
                    PhotoAnswer = galleryQuestion.PhotoAnswer,
                };
        }

        public GalleryQuestion ToModel()
        {
            return GalleryQuestionFactory.CreateGalleryQuestion(Photo, Answer, PhotoAnswer);
        }
    }
}

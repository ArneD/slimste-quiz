using System;
using SlimsteMens.Services.SlimsteAdminService;

namespace SlimsteMens.Services.Info
{
    public class GalleryQuestionInfo
    {
        public byte[] Photo { get; set; }
        public byte[] PhotoAnswer { get; set; }
        public string Answer { get; set; }

        public GalleryQuestionInfo(){}

        /// <summary>
        /// Initializes a new instance of the <see cref="GalleryQuestionInfo" /> class.
        /// </summary>
        /// <param name="galleryQuestionDto">The gallery question dto.</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        public GalleryQuestionInfo(GalleryQuestionDto galleryQuestionDto)
        {
            if (galleryQuestionDto == null) throw new ArgumentNullException("galleryQuestionDto");
            Photo = galleryQuestionDto.Photo;
            Answer = galleryQuestionDto.Answer;
            PhotoAnswer = galleryQuestionDto.PhotoAnswer;
        }

        /// <summary>
        /// Creates the dto.
        /// </summary>
        /// <returns></returns>
        public GalleryQuestionDto CreateDto()
        {
            return new GalleryQuestionDto
                {
                    Answer = Answer,
                    Photo = Photo,
                    PhotoAnswer = PhotoAnswer,
                };
        }
    }
}

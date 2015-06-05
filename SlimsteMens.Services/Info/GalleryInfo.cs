using System.Collections.Generic;
using System.Linq;
using SlimsteMens.Services.SlimsteAdminService;

namespace SlimsteMens.Services.Info
{
    public class GalleryInfo
    {
        public string Name { get; set; }
        public List<GalleryQuestionInfo> Questions { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GalleryInfo" /> class.
        /// </summary>
        public GalleryInfo()
        {
            Questions = new List<GalleryQuestionInfo>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GalleryInfo" /> class.
        /// </summary>
        /// <param name="galleryDto">The gallery dto.</param>
        public GalleryInfo(GalleryDto galleryDto)
        {
            Name = galleryDto.Name;
            Questions = galleryDto.Questions.Select(s => new GalleryQuestionInfo(s)).ToList();
        }

        public GalleryDto CreateDto()
        {
            return new GalleryDto
                {
                    Name = Name,
                    Questions = Questions.Select(s => s.CreateDto()).ToList(),
                };
        }
    }
}

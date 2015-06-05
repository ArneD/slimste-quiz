using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using SlimsteMens.Model.Entities;
using SlimsteMens.Model.Factories;

namespace SlimsteMens.Service.DataContracts
{
    [DataContract]
    public class GalleryDto
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public List<GalleryQuestionDto> Questions { get; set; }

        public static GalleryDto CreateDto(Gallery gallery)
        {
            return new GalleryDto
                {
                    Name = gallery.Name,
                    Questions = gallery.GalleryQuestions.Select(GalleryQuestionDto.CreateDto).ToList()
                };
        }

        public Gallery ToModel()
        {
            return GalleryFactory.CreateGallery(Name, new Collection<GalleryQuestion>(Questions.Select(s => s.ToModel()).ToList()));
        }
    }
}

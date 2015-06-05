using System;
using System.Collections.Generic;
using SlimsteMens.Model.Entities;

namespace SlimsteMens.Model.Factories
{
    public static class GalleryFactory
    {
        /// <summary>
        /// Creates the gallery.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="galleryQuestions">The gallery questions.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException"></exception>
        public static Gallery CreateGallery(string name, ICollection<GalleryQuestion> galleryQuestions)
        {
            if(galleryQuestions.Count != 10)
                throw new ArgumentException("Galery must have 10 questions", "galleryQuestions");

            var gallery = new Gallery(name);
            foreach (var galleryQuestion in galleryQuestions)
            {
                gallery.GalleryQuestions.Add(galleryQuestion);
            }
            return gallery;
        }
    }
}

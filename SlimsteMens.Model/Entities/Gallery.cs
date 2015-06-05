using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SlimsteMens.Model.Entities
{
    public class Gallery : EntityBase
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if(string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("value");
                _name = value;
            }
        }

        public virtual ICollection<GalleryQuestion> GalleryQuestions { get; protected set; }

        internal protected Gallery(string name) : this()
        {
            Name = name;
        }

        protected Gallery()
        {
            GalleryQuestions = new Collection<GalleryQuestion>();
        }
    }
}

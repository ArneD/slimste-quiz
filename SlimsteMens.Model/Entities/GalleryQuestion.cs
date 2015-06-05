using System;

namespace SlimsteMens.Model.Entities
{
    public class GalleryQuestion : EntityBase
    {
        private string _answer;
        private byte[] _photo;

        public string Answer
        {
            get { return _answer; }
            set
            {
                if(string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("value");
                _answer = value;
            }
        }

        public byte[] Photo
        {
            get { return _photo; }
            set
            {
                if(value == null || value.Length == 0)
                    throw new ArgumentNullException("value");
                _photo = value;
            }
        }

        public byte[] PhotoAnswer { get; set; }

        public bool Played { get; set; }

        internal protected GalleryQuestion(byte[] photo, string answer) : this()
        {
            Photo = photo;
            Answer = answer;
        }

        protected GalleryQuestion()
        {
            Played = false;
        }
    }
}

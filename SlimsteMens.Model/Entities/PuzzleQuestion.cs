using System;

namespace SlimsteMens.Model.Entities
{
    public class PuzzleQuestion : EntityBase
    {
        private string _answer;
        private string _hint1;
        private string _hint2;
        private string _hint3;
        private string _hint4;

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

        public string Hint1
        {
            get { return _hint1; }
            set
            {
                if(string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("value");
                _hint1 = value;
            }
        }

        public string Hint2
        {
            get { return _hint2; }
            set
            {
                if(string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("value");
                _hint2 = value;
            }
        }

        public string Hint3
        {
            get { return _hint3; }
            set
            {
                if(string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("value");
                _hint3 = value;
            }
        }

        public string Hint4
        {
            get { return _hint4; }
            set
            {
                if(string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("value");
                _hint4 = value;
            }
        }

        public bool Played { get; set; }

        internal protected PuzzleQuestion(string hint1, string hint2, string hint3, string hint4, string answer) : this()
        {
            Hint1 = hint1;
            Hint2 = hint2;
            Hint3 = hint3;
            Hint4 = hint4;
            Answer = answer;
        }

        protected PuzzleQuestion(){}
    }
}

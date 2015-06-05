using System;

namespace SlimsteMens.Model.Entities
{
    public class ThreeSixNineQuestion : EntityBase
    {
        private string _question;
        private string _answer;

        public string Question
        {
            get { return _question; }
            set
            {
                if(string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("value");
                _question = value;
            }
        }

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

        public byte[] Photo { get; set; }

        public bool Played { get; set; }

        protected internal ThreeSixNineQuestion(string question, string answer)
        {
            Question = question;
            Answer = answer;
            Played = false;
        }

        protected internal ThreeSixNineQuestion()
        {}
    }
}

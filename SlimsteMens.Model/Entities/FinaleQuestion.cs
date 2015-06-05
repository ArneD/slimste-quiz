using System;

namespace SlimsteMens.Model.Entities
{
    public class FinaleQuestion : EntityBase
    {
        private string _question;
        private string _answer1;
        private string _answer3;
        private string _answer2;
        private string _answer4;
        private string _answer5;

        public string Question
        {
            get { return _question; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("value");
                _question = value;
            }
        }

        public string Answer1
        {
            get { return _answer1; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("value");
                _answer1 = value;
            }
        }

        public string Answer2
        {
            get { return _answer2; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("value");
                _answer2 = value;
            }
        }

        public string Answer3
        {
            get { return _answer3; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("value");
                _answer3 = value;
            }
        }

        public string Answer4
        {
            get { return _answer4; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("value");
                _answer4 = value;
            }
        }

        public string Answer5
        {
            get { return _answer5; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("value");
                _answer5 = value;
            }
        }

        public bool Played { get; set; }

        internal protected FinaleQuestion(string question, string answer1, string answer2,
            string answer3, string answer4, string answer5) : this()
        {
            Question = question;
            Answer1 = answer1;
            Answer2 = answer2;
            Answer3 = answer3;
            Answer4 = answer4;
            Answer5 = answer5;
        }

        protected FinaleQuestion()
        {
            Played = false;
        }
    }
}

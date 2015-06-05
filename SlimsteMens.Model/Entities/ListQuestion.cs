using System;
using System.Collections.Generic;

namespace SlimsteMens.Model.Entities
{
    public class ListQuestion : EntityBase
    {
        private string _name;
        private string _question;
        private ICollection<string> _listAnswers;

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

        public virtual ICollection<string> ListAnswers
        {
            get { return _listAnswers; }
            protected set
            {
                if (value.Count == 15)
                    throw new ArgumentException("List of answers must have 15 answers", "value");
                _listAnswers = value;
            }
        }

        internal protected ListQuestion(string name, string question, ICollection<string> listAnswers)
        {
            if (listAnswers == null) 
                throw new ArgumentNullException("listAnswers");
            Name = name;
            Question = question;
            ListAnswers = listAnswers;
        }

        protected ListQuestion(){}
    }
}

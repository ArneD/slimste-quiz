using System;
using System.Collections.Generic;
using System.Linq;
using SlimsteMens.Model.Entities;
using SlimsteMens.Model.Repositories;

namespace SlimsteMens.Model.Services
{
    public class ThreeSixNineRoundService
    {
        private readonly IRepository<ThreeSixNineQuestion> _repository;
        public IList<ThreeSixNineQuestion> Questions { get; set; }
        public int QuestionNumber { get; set; }
        public int Try { get; set; }
        public int SecondsOnCorrect { get; set; }

        public ThreeSixNineRoundService(int secondsOnCorrect, IRepository<ThreeSixNineQuestion> repository)
        {
            if (repository == null) 
                throw new ArgumentNullException("repository");

            _repository = repository;
            SecondsOnCorrect = secondsOnCorrect;
            
            InitializeQuestions();

            QuestionNumber = 1;
            Try = 1;
        }

        public void Incorrect()
        {
            Try += 1;
            if (Try == 4)
            {
                Try = 1;
                QuestionNumber += 1;
            }
        }

        public int Correct()
        {
            Try = 1;
            QuestionNumber += 1;
            return (QuestionNumber - 1) % 3 == 0 ? SecondsOnCorrect : 0;
        }

        public ThreeSixNineQuestion NextQuestion()
        {
            return Questions[QuestionNumber - 1];
        }

        private void InitializeQuestions()
        {
            IList<long> ids;
            if (_repository.AsQueryable().Count(c => !c.Played) >= 15)
            {
                ids = _repository.AsQueryable().Where(c => !c.Played).Select(s => s.Id).Shuffle().Take(15).ToList();
            }
            else
            {
                ids = _repository.AsQueryable().Select(s => s.Id).Shuffle().Take(15).ToList();
            }

            if (ids.Count < 15)
                throw new InvalidOperationException("Need 15 3-6-9 questions to continue");

            Questions = _repository.AsQueryable()
                .Where(c => ids.Any(s => s == c.Id))
                .Shuffle()
                .ToList();

            foreach (var question in Questions)
            {
                question.Played = true;
            }
        }
    }
}

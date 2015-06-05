using System;
using System.Collections.Generic;
using System.Linq;
using SlimsteMens.Model.Entities;
using SlimsteMens.Model.Repositories;

namespace SlimsteMens.Model.Services
{
    public class FinaleRoundService
    {
        private readonly Finale _finale;
        public int Correct { get; set; }
        private readonly IList<long> _ids = new List<long>();
        public int SecondsOnCorrect { get; set; }
        public IList<FinaleTurnType> PlayedCurrentQuestion { get; set; }

        public FinaleRoundService(Finale finale)
        {
            if (finale == null) throw new ArgumentNullException("finale");

            _finale = finale;
            SecondsOnCorrect = 20;
        }

        public FinaleQuestion NextQuestion(IRepository<FinaleQuestion> finaleRepository)
        {
            Correct = 0;
            PlayedCurrentQuestion = new List<FinaleTurnType>();
            _finale.NextAndSetTurnByLowestTime(null);
            PlayedCurrentQuestion.Add(_finale.Turn);
            var question = finaleRepository.Query(p => _ids.All(i => i != p.Id)).Shuffle().FirstOrDefault();
            
            if (question == null)
            {
                throw new InvalidOperationException("No more finale questions");
            }

            question.Played = true;
            _ids.Add(question.Id);
            return question;
        }
    }
}

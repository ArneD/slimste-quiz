using System;
using System.Collections.Generic;
using System.Linq;
using SlimsteMens.Model.Entities;
using SlimsteMens.Model.Factories;
using SlimsteMens.Model.Repositories;

namespace SlimsteMens.Model.Services
{
    public class PuzzleRoundService
    {
        private readonly Game _game;
        private readonly IRepository<Puzzle> _puzzleRepository;
        private readonly IRepository<PuzzleQuestion> _puzzleQuestionRepository;
        protected int PuzzleNr;
        public int Correct { get; set; }
        public IList<Puzzle> Puzzles { get; set; }
        public int SecondsOnCorrect { get; set; }
        public IList<TurnType> PlayedAPuzzle { get; set; }
        public IList<TurnType> PlayedCurrentPuzzle { get; set; }

        public PuzzleRoundService(int secondsOnCorrect, Game game, IRepository<Puzzle> puzzleRepository, IRepository<PuzzleQuestion> puzzleQuestionRepository)
        {
            if (game == null) throw new ArgumentNullException("game");
            if (puzzleRepository == null) throw new ArgumentNullException("puzzleRepository");
            if (puzzleQuestionRepository == null) throw new ArgumentNullException("puzzleQuestionRepository");

            _game = game;
            _puzzleRepository = puzzleRepository;
            _puzzleQuestionRepository = puzzleQuestionRepository;
            SecondsOnCorrect = secondsOnCorrect;
            InitializePuzzles();
            PlayedAPuzzle = new List<TurnType>();
            PlayedCurrentPuzzle = new List<TurnType>();
            PuzzleNr = 0;
        }

        public Puzzle NextPuzzle()
        {
            if (PuzzleNr >= Puzzles.Count)
                return null;

            TurnType turn = _game.NextAndSetTurnByLowestTime(PlayedAPuzzle);
            Correct = 0;
            if (Puzzles.Count == 0)
                return null;

            PlayedCurrentPuzzle = new List<TurnType>();
            PlayedAPuzzle.Add(turn);
            PuzzleNr++;

            return Puzzles[PuzzleNr - 1];
        }

        private void InitializePuzzles()
        {
            //Predefined puzzles: TODO: implement in admin UI
            if (_puzzleRepository.AsQueryable().Count(c => c.PuzzleQuestions.Count >= 3 && c.PuzzleQuestions.All(p => !p.Played)) >= 3)
            {
                Puzzles = _puzzleRepository.AsQueryable().Where(puz => puz.PuzzleQuestions.All(question => !question.Played)).Shuffle().Take(3).ToList();
            }
            else if (_puzzleQuestionRepository.AsQueryable().Count(p => !p.Played) >= 9)
            {
                var questions = _puzzleQuestionRepository.AsQueryable().Where(p => !p.Played).Shuffle().Take(9).ToList();
                questions.ToList().ForEach(p => p.Played = true);

                CreatePuzzles(questions);
            }
            else
            {
                var questions = _puzzleQuestionRepository.AsQueryable().Shuffle().Take(9).ToList();
                questions.ToList().ForEach(p => p.Played = true);

                CreatePuzzles(questions);
            }

            if (Puzzles.Count < 3)
                throw new InvalidOperationException("Need 3 puzzles to continue");

            foreach (var puzzle in Puzzles)
                puzzle.PuzzleQuestions.ToList().ForEach(s => s.Played = true);
        }

        private void CreatePuzzles(IList<PuzzleQuestion> questions)
        {
            if (questions.Count < 9)
                throw new InvalidOperationException("Need 9 puzzle questions to continue");

            Puzzles = new List<Puzzle>
                            {
                                PuzzleFactory.CreatePuzzle(questions.Take(3).ToList()),
                                PuzzleFactory.CreatePuzzle(questions.Skip(3).Take(3).ToList()),
                                PuzzleFactory.CreatePuzzle(questions.Skip(6).Take(3).ToList())
                            };
        }
    }
}

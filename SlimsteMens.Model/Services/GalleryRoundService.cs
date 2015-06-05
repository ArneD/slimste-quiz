using System;
using System.Collections.Generic;
using System.Linq;
using SlimsteMens.Model.Entities;
using SlimsteMens.Model.Repositories;

namespace SlimsteMens.Model.Services
{
    public class GalleryRoundService
    {
        private readonly IRepository<Gallery> _galleryRepository;
        private readonly Game _game;
        protected int GalleryNr;
        public int Correct { get; set; }
        public IList<Gallery> Galleries { get; set; }
        public int SecondsOnCorrect { get; set; }
        public IList<TurnType> PlayedAGallery { get; set; }
        public IList<TurnType> PlayedCurrentGallery { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GalleryRoundService" /> class.
        /// </summary>
        /// <param name="secondsOnCorrect">The seconds on correct.</param>
        /// <param name="galleryRepository">The gallery repository.</param>
        /// <param name="game">The game.</param>
        /// <exception cref="System.ArgumentNullException">
        /// galleryRepository
        /// or
        /// game
        /// </exception>
        /// <exception cref="System.ArgumentException"></exception>
        public GalleryRoundService(int secondsOnCorrect, IRepository<Gallery> galleryRepository, Game game)
        {
            if (galleryRepository == null) throw new ArgumentNullException("galleryRepository");
            if (game == null) throw new ArgumentNullException("game");

            SecondsOnCorrect = secondsOnCorrect;
            _galleryRepository = galleryRepository;
            _game = game;
            PlayedAGallery = new List<TurnType>();
            PlayedCurrentGallery = new List<TurnType>();
            GalleryNr = 0;
            InitializeGalleries();
        }

        /// <summary>
        /// Retrieves the Next gallery.
        /// </summary>
        /// <returns></returns>
        public Gallery NextGallery()
        {
            if (GalleryNr >= Galleries.Count)
                return null;

            TurnType turn = _game.NextAndSetTurnByLowestTime(PlayedAGallery);
            Correct = 0;
            if (Galleries.Count == 0)
                return null;

            PlayedCurrentGallery = new List<TurnType>();
            PlayedAGallery.Add(turn);
            GalleryNr++;

            return Galleries[GalleryNr - 1];
        }

        private void InitializeGalleries()
        {
            List<long> ids;

            if (_galleryRepository.AsQueryable().Count(c => c.GalleryQuestions.All(g => !g.Played)) >= 3)
            {
                ids = _galleryRepository.AsQueryable().Where(c => c.GalleryQuestions.All(g => !g.Played)).Select(s => s.Id).Shuffle().Take(3).ToList();
            }
            else
            {
                ids = _galleryRepository.AsQueryable().Select(s => s.Id).Shuffle().Take(3).ToList();
            }

            Galleries = _galleryRepository.Query(g => ids.Any(id => id == g.Id)).Shuffle().ToList();

            if (Galleries.Count < 3)
                throw new InvalidOperationException("Need 3 galleries to continue");

            Galleries.ToList().ForEach(g => g.GalleryQuestions.ToList().ForEach(gq => gq.Played = true));
        }
    }
}

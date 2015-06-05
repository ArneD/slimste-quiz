using System;
using System.Collections.Generic;
using System.Linq;
using SlimsteMens.Model.Entities;
using SlimsteMens.Model.Repositories;

namespace SlimsteMens.Model.Services
{
    public class VideoRoundService
    {
        private readonly IRepository<VideoQuestion> _videoRepository;
        private readonly Game _game;
        protected int VideoNr;
        private int _correct;

        public int Correct
        {
            get { return _correct; }
            set
            {
                _correct = value;
                SecondsOnCorrect += 10;
            }
        }

        public List<VideoQuestion> Videos { get; set; }
        public int SecondsOnCorrect { get; set; }
        public IList<TurnType> PlayedAVideo { get; set; }
        public IList<TurnType> PlayedCurrentVideo { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="VideoRoundService" /> class.
        /// </summary>
        /// <param name="videoRepository">The video repository.</param>
        /// <param name="game">The game.</param>
        /// <exception cref="System.ArgumentNullException">
        /// videoRepository
        /// or
        /// game
        /// </exception>
        /// <exception cref="System.ArgumentException"></exception>
        public VideoRoundService(IRepository<VideoQuestion> videoRepository, Game game)
        {
            if (videoRepository == null) throw new ArgumentNullException("videoRepository");
            if (game == null) throw new ArgumentNullException("game");

            _videoRepository = videoRepository;
            _game = game;
            PlayedAVideo = new List<TurnType>();
            PlayedCurrentVideo = new List<TurnType>();
            VideoNr = 0;

            InitializeVideos();
        }

        /// <summary>
        /// Retrieves the Next gallery.
        /// </summary>
        /// <returns></returns>
        public VideoQuestion NextVideo()
        {
            if (VideoNr >= Videos.Count)
                return null;

            TurnType turn = _game.NextAndSetTurnByLowestTime(PlayedAVideo);
            Correct = 0;
            if (Videos.Count == 0)
                return null;

            PlayedCurrentVideo = new List<TurnType>();
            PlayedAVideo.Add(turn);
            VideoNr++;

            SecondsOnCorrect = 10;

            return Videos[VideoNr - 1];
        }

        private void InitializeVideos()
        {
            Videos = _videoRepository.AsQueryable().Count(c => !c.Played) >= 3
                ? _videoRepository.Query(f => !f.Played).Shuffle().Take(3).ToList()
                : _videoRepository.AsQueryable().Shuffle().Take(3).ToList();

            if(Videos.Count < 3)
                throw new InvalidOperationException("Need 3 video questions to continue");

            Videos.ForEach(q => q.Played = true);
        }
    }
}

using Autofac;
using SlimsteMens.Model.Entities;
using SlimsteMens.Model.Repositories;
using SlimsteMens.Model.Services;
using SlimsteMens.Service.Admin;
using SlimsteMens.Service.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;

namespace SlimsteMens.Service
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Reentrant)]
    public class SlimsteService : ISlimsteService
    {
        private static readonly List<ISlimsteCallback> Subscribers = new List<ISlimsteCallback>();
        private System.Timers.Timer _timer;
        private Game _game;
        private Finale _finale;
        private ThreeSixNineRoundService _threeSixNineRoundService;
        private PuzzleRoundService _puzzleRoundService;
        private GalleryRoundService _galleryRoundService;
        private VideoRoundService _videoRoundService;
        private FinaleRoundService _finaleRoundService;
        private readonly AdminQuestionsService _adminQuestionsService;
        private readonly IContainer _container;

        public SlimsteService()
        {
            _container = IoCContainer.Build();
            _adminQuestionsService = new AdminQuestionsService(_container);
        }

        #region General
        public void Start(GameDto game)
        {
            _game = new Game(game.Team1, game.Team2, game.Team3, game.StartingSeconds,
                game.Rounds.Select(s => s.ToModel()).ToList());

            using (var scope = new UnitOfWorkScope(_container.BeginLifetimeScope()))
            {
                if (_game.Rounds.Contains(RoundType.ThreeSixNine))
                {
                    _threeSixNineRoundService = new ThreeSixNineRoundService(10, scope.Resolve<IRepository<ThreeSixNineQuestion>>());
                }

                if (_game.Rounds.Contains(RoundType.Puzzle))
                {
                    var puzzleRepository = scope.Resolve<IRepository<Puzzle>>();
                    var puzzleQuestionRepository = scope.Resolve<IRepository<PuzzleQuestion>>();
                    _puzzleRoundService = new PuzzleRoundService(30, _game, puzzleRepository, puzzleQuestionRepository);
                }

                if (_game.Rounds.Contains(RoundType.Gallery))
                {
                    _galleryRoundService = new GalleryRoundService(20, scope.Resolve<IRepository<Gallery>>(), _game);
                }

                if (_game.Rounds.Contains(RoundType.Video))
                {
                    var videoRepository = scope.Resolve<IRepository<VideoQuestion>>();
                    _videoRoundService = new VideoRoundService(videoRepository, _game);
                }
            }

            SendToSubscribers(x => x.OnGameStarted(game));

            _timer = new System.Timers.Timer(1000);
            _timer.Elapsed += TimerElapsed;

            NextRound();
        }

        void TimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (_finale == null)
            {
                switch (_game.Turn)
                {
                    case TurnType.Team1:
                        _game.TimeTeam1 -= 1;
                        SendToSubscribers(x => x.OnSetTimer(TurnType.Team1.ToDto(), _game.TimeTeam1));
                        break;
                    case TurnType.Team2:
                        _game.TimeTeam2 -= 1;
                        SendToSubscribers(x => x.OnSetTimer(TurnType.Team2.ToDto(), _game.TimeTeam2));
                        break;
                    case TurnType.Team3:
                        _game.TimeTeam3 -= 1;
                        SendToSubscribers(x => x.OnSetTimer(TurnType.Team3.ToDto(), _game.TimeTeam3));
                        break;
                }
            }
            else
            {
                switch (_finale.Turn)
                {
                    case FinaleTurnType.Team1:
                        _finale.TimeTeam1 -= 1;
                        SendToSubscribers(x => x.OnSetTimerFinale(FinaleTurnType.Team1.ToDto(), _finale.TimeTeam1));
                        break;
                    case FinaleTurnType.Team2:
                        _finale.TimeTeam2 -= 1;
                        SendToSubscribers(x => x.OnSetTimerFinale(FinaleTurnType.Team2.ToDto(), _finale.TimeTeam2));
                        break;
                }
                CheckFinaleEnd();
            }
        }

        public bool Subscribe()
        {
            try
            {
                var callback = OperationContext.Current.GetCallbackChannel<ISlimsteCallback>();
                if (!Subscribers.Contains(callback))
                    Subscribers.Add(callback);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Unsubscribe()
        {
            try
            {
                var callback = OperationContext.Current.GetCallbackChannel<ISlimsteCallback>();
                if (Subscribers.Contains(callback))
                    Subscribers.Remove(callback);
                //return true;
            }
            catch
            {
                //return false;
            }
        }

        public void StartTimer()
        {
            if (_timer != null)
                _timer.Start();
        }

        public void StopTimer()
        {
            if (_timer != null)
                _timer.Stop();
        }

        public void ResetGame(ResetGameDto resetGameDto)
        {
            _game.TimeTeam1 = resetGameDto.Team1Seconds;
            _game.TimeTeam2 = resetGameDto.Team2Seconds;
            _game.TimeTeam3 = resetGameDto.Team3Seconds;
            _game.Turn = resetGameDto.Turn.ToModel();

            SendToSubscribers(x => x.OnSetTimer(TurnTypeDto.Team1, _game.TimeTeam1));
            SendToSubscribers(x => x.OnSetTimer(TurnTypeDto.Team2, _game.TimeTeam2));
            SendToSubscribers(x => x.OnSetTimer(TurnTypeDto.Team3, _game.TimeTeam3));
            SendToSubscribers(x => x.OnTurnChange(_game.Turn.ToDto()));
        }

        public void NextRound()
        {
            switch (_game.NextRound())
            {
                case RoundType.ThreeSixNine:
                    SendToSubscribers(x => x.OnStartRound369());
                    break;
                case RoundType.Puzzle:
                    SendToSubscribers(x => x.OnStartRoundPuzzle());
                    break;
                case RoundType.Gallery:
                    SendToSubscribers(x => x.OnStartRoundGallery());
                    break;
                case RoundType.Video:
                    SendToSubscribers(x => x.OnStartRoundVideo());
                    break;
                default:
                    StartFinale();
                    break;
            }
        }

        private void RefreshTimeForPlayersTurn()
        {
            if (_finale == null)
            {
                SendToSubscribers(x => x.OnSetTimer(_game.Turn.ToDto(), _game.TimeByTurn));
            }
            else
            {
                if (_finale.Turn == FinaleTurnType.Team1)
                {
                    SendToSubscribers(x => x.OnSetTimerFinale(FinaleTurnTypeDto.Team2, _finale.TimeTeam2));
                }
                else
                {
                    SendToSubscribers(x => x.OnSetTimerFinale(FinaleTurnTypeDto.Team1, _finale.TimeTeam1));
                }
            }
        }

        private void RefreshTurn()
        {
            if (_finale == null)
            {
                SendToSubscribers(x => x.OnTurnChange(_game.Turn.ToDto()));
            }
            else
            {
                SendToSubscribers(x => x.OnFinaleTurnChange(_finale.Turn.ToDto()));
            }
        }

        public void SetTime(TurnTypeDto turn, int time)
        {
            switch (turn.ToModel())
            {
                case TurnType.Team1:
                    _game.TimeTeam1 = time;
                    SendToSubscribers(x => x.OnSetTimer(turn, _game.TimeTeam1));
                    break;
                case TurnType.Team2:
                    _game.TimeTeam2 = time;
                    SendToSubscribers(x => x.OnSetTimer(turn, _game.TimeTeam2));
                    break;
                case TurnType.Team3:
                    _game.TimeTeam3 = time;
                    SendToSubscribers(x => x.OnSetTimer(turn, _game.TimeTeam3));
                    break;
            }
        }

        public void SetFinaleTime(FinaleTurnTypeDto turn, int time)
        {
            switch (turn.ToModel())
            {
                case FinaleTurnType.Team1:
                    _game.TimeTeam1 = time;
                    SendToSubscribers(x => x.OnSetTimerFinale(turn, _finale.TimeTeam1));
                    break;
                case FinaleTurnType.Team2:
                    _game.TimeTeam2 = time;
                    SendToSubscribers(x => x.OnSetTimerFinale(turn, _finale.TimeTeam2));
                    break;
            }
        }

        #endregion General

        #region 3-6-9
        public void StartRound369()
        {
            Send369Question();
        }

        private void Send369Question()
        {
            if (_threeSixNineRoundService.QuestionNumber != 16)
            {
                SendToSubscribers(x => x.OnQuestion369(ThreeSixNineQuestionDto.Create(_threeSixNineRoundService.NextQuestion())));
            }
            else
            {
                SendToSubscribers(x => x.OnEndRound369());
            }
        }

        public void Question369Incorrect()
        {
            _threeSixNineRoundService.Incorrect();
            _game.NextTurn();
            RefreshTurn();
            if (_threeSixNineRoundService.Try == 1)
            {
                Send369Question();
            }
        }

        public void Question369Correct()
        {
            int i = _threeSixNineRoundService.Correct();
            if (i != 0)
            {
                _game.AddTime(i);
                RefreshTimeForPlayersTurn();
            }
            Send369Question();
        }

        public bool AddQuestion369(ThreeSixNineQuestionDto questionDto)
        {
            return _adminQuestionsService.AddQuestion369(questionDto);
        }

        #endregion 3-6-9

        #region Puzzle
        public void StartRoundPuzzle()
        {
            SendPuzzle();
            StartTimer();
        }

        public void NextTurnForPuzzle()
        {
            _puzzleRoundService.PlayedCurrentPuzzle.Add(_game.Turn);
            _game.NextAndSetTurnByLowestTime(_puzzleRoundService.PlayedCurrentPuzzle);
            RefreshTurn();
            StartTimer();
        }

        public void ShowPuzzleAnswers()
        {
            SendToSubscribers(x => x.OnShowPuzzleAnswers());
        }

        public void NextPuzzle()
        {
            SendPuzzle();
            StartTimer();
        }

        private void SendPuzzle()
        {
            var puzzle = _puzzleRoundService.NextPuzzle();
            RefreshTurn();
            if (puzzle != null)
            {
                SendToSubscribers(x => x.OnQuestionPuzzle(puzzle.PuzzleQuestions.Select(PuzzleQuestionDto.Create)));
            }
            else
            {
                SendToSubscribers(x => x.OnEndRoundPuzzle());
            }
        }

        public void QuestionPuzzleCorrect(long puzzleId)
        {
            _game.AddTime(_puzzleRoundService.SecondsOnCorrect);
            _puzzleRoundService.Correct++;
            SendToSubscribers(x => x.OnPuzzleCorrect(puzzleId));

            if (_puzzleRoundService.Correct == 3)
            {
                StopTimer();
                SendToSubscribers(x => x.OnQuestionPuzzleCompleted());
            }

            RefreshTimeForPlayersTurn();
        }

        public bool AddPuzzleQuestion(PuzzleQuestionDto puzzleQuestionDto)
        {
            return _adminQuestionsService.AddPuzzleQuestion(puzzleQuestionDto);
        }
        #endregion Puzzle

        #region Gallery

        public void StartRoundGallery()
        {
            NextGallery();
        }

        public void NextGallery()
        {
            var gallery = _galleryRoundService.NextGallery();
            RefreshTurn();
            if (gallery != null)
            {
                SendToSubscribers(x => x.OnQuestionGallery(GalleryDto.CreateDto(gallery)));
                StartTimer();
            }
            else
            {
                SendToSubscribers(x => x.OnEndRoundGallery());
            }
        }

        public void NextTurnForGallery()
        {
            _galleryRoundService.PlayedCurrentGallery.Add(_game.Turn);
            if (_galleryRoundService.PlayedCurrentGallery.Count < 3)
            {
                _game.NextAndSetTurnByLowestTime(_galleryRoundService.PlayedCurrentGallery);
                RefreshTurn();
                StartTimer();
            }
            else
            {
                StopTimer();
                //if (_galleryRoundService.PlayedAGallery.Count < 3)
                //{
                SendToSubscribers(x => x.OnGalleryCompleted());
                //}
                //else
                //{
                //    SendToSubscribers(x => x.OnEndRoundGallery());
                //}
            }
        }

        public void GalleryQuestionCorrect()
        {
            _game.AddTime(_galleryRoundService.SecondsOnCorrect);
            _galleryRoundService.Correct++;
            if (_galleryRoundService.PlayedCurrentGallery.Count < 2)
            {
                SendToSubscribers(x => x.OnNextGalleryQuestion());
            }

            if (_galleryRoundService.Correct == 10)
            {
                StopTimer();
                if (_galleryRoundService.PlayedAGallery.Count < 3)
                {
                    SendToSubscribers(x => x.OnGalleryCompleted());
                }
                else
                {
                    SendToSubscribers(x => x.OnEndRoundGallery());
                }
            }

            RefreshTimeForPlayersTurn();
        }

        public void GalleryQuestionIncorrect()
        {
            SendToSubscribers(x => x.OnNextGalleryQuestion());
        }

        public void ShowNextGalleryQuestionAnswer()
        {
            SendToSubscribers(x => x.OnShowGalleryAnswer());
        }

        public bool AddGallery(GalleryDto gallery)
        {
            return _adminQuestionsService.AddGallery(gallery);
        }

        #endregion

        #region Video
        public void StartRoundVideo()
        {
            throw new NotImplementedException();
        }

        public void NextVideo()
        {
            var nextVideo = _videoRoundService.NextVideo();
            RefreshTurn();
            if (nextVideo != null)
            {
                SendToSubscribers(x => x.OnQuestionVideo(VideoDto.CreateDto(nextVideo)));
            }
            else
            {
                SendToSubscribers(x => x.OnEndRoundVideo());
            }
        }

        public void PlayVideo()
        {
            SendToSubscribers(x => x.OnPlayVideo());
        }

        public void PlayVideoCompleted()
        {
            SendToSubscribers(x => x.OnPlayVideoCompleted());
        }

        public void NextTurnForVideo()
        {
            _videoRoundService.PlayedCurrentVideo.Add(_game.Turn);
            if (_videoRoundService.PlayedCurrentVideo.Count < 3)
            {
                _game.NextAndSetTurnByLowestTime(_videoRoundService.PlayedCurrentVideo);
                RefreshTurn();
                StartTimer();
            }
            else
            {
                StopTimer();
                SendToSubscribers(x => x.OnVideoCompleted());
            }
        }

        public void QuestionVideoCorrect(string answer)
        {
            _game.AddTime(_videoRoundService.SecondsOnCorrect);
            _videoRoundService.Correct++;
            SendToSubscribers(x => x.OnVideoQuestionCorrect(answer));

            if (_videoRoundService.Correct == 5)
            {
                StopTimer();
                if (_videoRoundService.PlayedAVideo.Count < 3)
                {
                    SendToSubscribers(x => x.OnVideoCompleted());
                }
                else
                {
                    SendToSubscribers(x => x.OnEndRoundVideo());
                }
            }

            RefreshTimeForPlayersTurn();
        }

        public void ShowVideoAnswers()
        {
            SendToSubscribers(x => x.OnShowVideoAnswers());
        }

        public bool AddVideo(VideoDto video)
        {
            return _adminQuestionsService.AddVideo(video);
        }
        #endregion Video

        #region Finale

        public void StartFinale()
        {
            var turns = _game.TeamsTiebreakQuestionNeeded();
            if (turns.Count == 0)
            {
                _finale = _game.RetrieveFinale();
                _finaleRoundService = new FinaleRoundService(_finale);
                SendToSubscribers(x => x.OnFinaleStarted(new FinaleDto(_finale)));
                RefreshTurn();
            }
            else
            {
                SendToSubscribers(x => x.OnTiebreakerQuestion(turns.Select(t => t.ToDto()).ToList()));
            }
        }

        public void FinaleQuestionCorrect(string answer)
        {
            _finale.RemoveTime(_finaleRoundService.SecondsOnCorrect);
            _finaleRoundService.Correct++;
            SendToSubscribers(x => x.OnFinaleAnswerCorrect(answer));

            if (_finaleRoundService.Correct == 5)
            {
                StopTimer();
                SendToSubscribers(x => x.OnFinaleQuestionCompleted());
            }
            CheckFinaleEnd();
            RefreshTimeForPlayersTurn();
        }

        public void NextTurnForFinaleQuestion()
        {
            if (_finaleRoundService.PlayedCurrentQuestion.Count < 2)
            {
                _finaleRoundService.PlayedCurrentQuestion.Add(_finale.Turn);
                _finale.NextAndSetTurnByLowestTime(_finaleRoundService.PlayedCurrentQuestion);
                RefreshTurn();
                StartTimer();
            }
            else
            {
                StopTimer();
                SendToSubscribers(x => x.OnFinaleQuestionCompleted());
            }
        }

        public void ShowFinaleQuestionAnswers()
        {
            SendToSubscribers(x => x.OnShowFinaleQuestionAnswers());
        }

        public void NextFinaleQuestion()
        {
            using (var scope = new UnitOfWorkScope(_container.BeginLifetimeScope()))
            {
                var question = _finaleRoundService.NextQuestion(scope.Resolve<IRepository<FinaleQuestion>>());
                RefreshTurn();
                if (question != null)
                {
                    SendToSubscribers(x => x.OnFinaleQuestion(new FinaleQuestionDto(question)));
                }
            }
        }

        public void SetTieBreakerWinner(IList<TurnTypeDto> winners)
        {
            _finale = _game.RetrieveFinale(winners.Select(s => s.ToModel()).ToList());
            _finaleRoundService = new FinaleRoundService(_finale);
            SendToSubscribers(x => x.OnFinaleStarted(new FinaleDto(_finale)));

            RefreshTurn();
        }

        public bool AddFinaleQuestion(FinaleQuestionDto finaleDto)
        {
            return _adminQuestionsService.AddFinaleQuestion(finaleDto);
        }

        private void CheckFinaleEnd()
        {
            if (_finale.TimeTeam1 <= 0 || _finale.TimeTeam2 <= 0)
            {
                StopTimer();
                if (_finale.TimeTeam1 == 0)
                {
                    SendToSubscribers(x => x.OnGameWon(_finale.Team2));
                }
                else
                {
                    SendToSubscribers(x => x.OnGameWon(_finale.Team1));
                }
            }
        }

        #endregion

        private static void SendToSubscribers(Action<ISlimsteCallback> action)
        {
            Task.Factory.StartNew(() =>
            {
                foreach (var callback in Subscribers)
                    action(callback);
            });
        }
    }
}

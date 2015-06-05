using System;
using System.Linq;
using System.Collections.Generic;
using System.ServiceModel;
using SlimsteMens.Services.Info;
using SlimsteMens.Services.Interfaces;
using SlimsteMens.Services.SlimsteAdminService;

namespace SlimsteMens.Services.Services
{
    public class AdminService : IAdminService
    {
        private readonly SlimsteServiceClient _slimsteAdminService;
        public IAdminServiceCallback AdminServiceCallback { get; private set; }

        public AdminService(IAdminServiceCallback adminServiceCallback)
        {
            AdminServiceCallback = adminServiceCallback;
            _slimsteAdminService = new SlimsteServiceClient(new InstanceContext(AdminServiceCallback));
            _slimsteAdminService.Subscribe();
        }

        public void StartGame(GameInfo game)
        {
            _slimsteAdminService.StartAsync(game.CreateDto());
        }

        public void ResetGame(ResetGameInfo resetGame)
        {
            _slimsteAdminService.ResetGameAsync(resetGame.CreateDto());
        }

        public void NextRound()
        {
            _slimsteAdminService.NextRoundAsync();
        }

        public void StartTimer()
        {
            _slimsteAdminService.StartTimerAsync();
        }

        public void StopTimer()
        {
            _slimsteAdminService.StopTimerAsync();
        }

        #region 3-6-9
        public void StartRound369()
        {
            _slimsteAdminService.StartRound369Async();
        }

        public bool Add369Question(ThreeSixNineQuestionInfo threeSixNineQuestionInfo)
        {
            return _slimsteAdminService.AddQuestion369(threeSixNineQuestionInfo.CreateDto());
        }

        public void Question369Correct()
        {
            _slimsteAdminService.Question369CorrectAsync();
        }

        public void Question369Incorrect()
        {
            _slimsteAdminService.Question369IncorrectAsync();
        }
        #endregion 3-6-9

        #region Puzzle
        public void StartRoundPuzzle()
        {
            _slimsteAdminService.StartRoundPuzzleAsync();
        }

        public void NextTurnForPuzzle()
        {
            _slimsteAdminService.NextTurnForPuzzleAsync();
        }

        public void NextPuzzle()
        {
            _slimsteAdminService.NextPuzzleAsync();
        }

        public void ShowPuzzleAnswers()
        {
            _slimsteAdminService.ShowPuzzleAnswersAsync();
        }

        public void PuzzleCorrect(long puzzleId)
        {
            _slimsteAdminService.QuestionPuzzleCorrect(puzzleId);
        }

        public bool AddPuzzleQuestion(PuzzleQuestionInfo puzzleQuestionInfo)
        {
            return _slimsteAdminService.AddPuzzleQuestion(puzzleQuestionInfo.CreateDto());
        }
        #endregion

        #region Gallery

        public bool AddGallery(GalleryInfo galleryInfo)
        {
            return _slimsteAdminService.AddGallery(galleryInfo.CreateDto());
        }

        public void StartRoundGallery()
        {
            _slimsteAdminService.StartRoundGalleryAsync();
        }

        public void NextTurnForGallery()
        {
            _slimsteAdminService.NextTurnForGalleryAsync();
        }

        public void ShowNextGalleryQuestionAnswer()
        {
            _slimsteAdminService.ShowNextGalleryQuestionAnswerAsync();
        }

        public void NextGallery()
        {
            _slimsteAdminService.NextGalleryAsync();
        }

        public void GalleryQuestionCorrect()
        {
            _slimsteAdminService.GalleryQuestionCorrectAsync();
        }

        public void GalleryQuestionIncorrect()
        {
            _slimsteAdminService.GalleryQuestionIncorrectAsync();
        }

        #endregion

        #region Video
        public bool AddVideoQuestion(VideoQuestionInfo videoQuestionInfo)
        {
            return _slimsteAdminService.AddVideo(videoQuestionInfo.CreateDto());
        }

        public void PlayVideo()
        {
            _slimsteAdminService.PlayVideoAsync();
        }

        public void VideoPlayed()
        {
            _slimsteAdminService.PlayVideoCompletedAsync();
        }

        public void NextVideo()
        {
            _slimsteAdminService.NextVideoAsync();
        }

        public void NextTurnForVideo()
        {
            _slimsteAdminService.NextTurnForVideoAsync();
        }

        public void ShowVideoAnswers()
        {
            _slimsteAdminService.ShowVideoAnswersAsync();
        }

        public void VideoAnswerCorrect(string answer)
        {
            _slimsteAdminService.QuestionVideoCorrect(answer);
        }
        #endregion Video

        #region Finale

        public bool AddFinaleQuestion(FinaleQuestionInfo finaleQuestionInfo)
        {
            return _slimsteAdminService.AddFinaleQuestion(finaleQuestionInfo.CreateDto());
        }

        public void StartFinale()
        {
            _slimsteAdminService.StartFinaleAsync();
        }

        public void SetTieBreakerWinner(IList<TurnTypeInfo> winners)
        {
            _slimsteAdminService.SetTieBreakerWinnerAsync(winners.Select(s => s.ToDto()).ToList());
        }

        public void NextFinaleQuestion()
        {
            _slimsteAdminService.NextFinaleQuestionAsync();
        }

        public void ShowFinaleQuestionAnswers()
        {
            _slimsteAdminService.ShowFinaleQuestionAnswersAsync();
        }

        public void NextTurnForFinaleQuestion()
        {
            _slimsteAdminService.NextTurnForFinaleQuestionAsync();
        }

        public void FinaleQuestionCorrect(string answer)
        {
            _slimsteAdminService.FinaleQuestionCorrectAsync(answer);
        }
        #endregion

        #region Disposing + Destructor
        public void Dispose()
        {
            Disposing(true);
            GC.SuppressFinalize(this);
        }

        protected void Disposing(bool disposing)
        {
            _slimsteAdminService.Unsubscribe();
            _slimsteAdminService.Dispose();
        }

        ~AdminService()
        {
            Disposing(false);
        }

        #endregion Disposing + Destructor
    }
}

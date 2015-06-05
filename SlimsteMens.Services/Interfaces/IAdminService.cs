using System;
using System.Collections.Generic;
using SlimsteMens.Services.Info;

namespace SlimsteMens.Services.Interfaces
{
    public interface IAdminService : IDisposable
    {
        IAdminServiceCallback AdminServiceCallback { get; }
        void StartGame(GameInfo game);
        void ResetGame(ResetGameInfo resetGame);
        void NextRound();
        void StartRound369();
        bool Add369Question(ThreeSixNineQuestionInfo threeSixNineQuestionInfo);
        void Question369Correct();
        void Question369Incorrect();
        void StartRoundPuzzle();
        void NextTurnForPuzzle();
        void ShowPuzzleAnswers();
        void NextPuzzle();
        bool AddPuzzleQuestion(PuzzleQuestionInfo puzzleQuestionInfo);
        void PuzzleCorrect(long puzzleId);

        void StartRoundGallery();
        bool AddGallery(GalleryInfo galleryInfo);
        void NextTurnForGallery();
        void NextGallery();
        void ShowNextGalleryQuestionAnswer();
        void GalleryQuestionCorrect();
        void GalleryQuestionIncorrect();

        bool AddVideoQuestion(VideoQuestionInfo videoQuestionInfo);
        void PlayVideo();
        void NextVideo();
        void NextTurnForVideo();
        void VideoPlayed();
        void VideoAnswerCorrect(string answer);
        void ShowVideoAnswers();

        bool AddFinaleQuestion(FinaleQuestionInfo finaleQuestionInfo);
        void SetTieBreakerWinner(IList<TurnTypeInfo> winners);
        void StartFinale();
        void NextFinaleQuestion();
        void ShowFinaleQuestionAnswers();
        void NextTurnForFinaleQuestion();
        void FinaleQuestionCorrect(string answer);

        void StartTimer();
        void StopTimer();
    }
}
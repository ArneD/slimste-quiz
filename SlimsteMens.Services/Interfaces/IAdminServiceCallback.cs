using System;
using System.Collections.Generic;
using SlimsteMens.Services.Info;
using SlimsteMens.Services.SlimsteAdminService;

namespace SlimsteMens.Services.Interfaces
{
    public interface IAdminServiceCallback : ISlimsteServiceCallback
    {
        event EventHandler<EventArgs<GameInfo>> GameStarted;
        event EventHandler Round369Started;
        event EventHandler<EventArgs<ThreeSixNineQuestionInfo>> Question369Received;
        event EventHandler Round369Ended;
        event EventHandler<EventArgs<TurnTypeInfo>> TurnChanged;
        event EventHandler<EventArgs<TimerChangeInfo>> TimerChanged;
        event EventHandler<EventArgs<FinaleTimerChangeInfo>> FinaleTimerChanged;

        event EventHandler RoundPuzzleStarted;
        event EventHandler<EventArgs<IList<PuzzleQuestionInfo>>> PuzzleReceived;
        event EventHandler<EventArgs<long>> PuzzleCorrect;
        event EventHandler PuzzleAnswersShowed; 
        event EventHandler PuzzleCompleted;
        event EventHandler RoundPuzzleEnded;

        event EventHandler RoundGalleryStarted;
        event EventHandler GalleryCompleted;
        event EventHandler<EventArgs<GalleryInfo>> QuestionGalleryReceived;
        event EventHandler NextGalleryAnswerShowed;
        event EventHandler NextGalleryQuestionShowed; 
        event EventHandler RoundGalleryEnded;

        event EventHandler RoundVideoStarted;
        event EventHandler VideoCompleted;
        event EventHandler PlayVideoStarted;
        event EventHandler PlayVideoCompleted;
        event EventHandler VideoAnswersShowed;
        event EventHandler<EventArgs<string>> VideoCorrect;
        event EventHandler<EventArgs<VideoQuestionInfo>> QuestionVideoReceived;
        event EventHandler RoundVideoEnded;

        event EventHandler<EventArgs<FinaleInfo>> FinaleStarted;
        event EventHandler<EventArgs<IList<TurnTypeInfo>>> TieBreakerQuestionReceived;
        event EventHandler<EventArgs<FinaleTurnTypeInfo>> FinaleTurnChanged;
        event EventHandler<EventArgs<FinaleQuestionInfo>> FinaleQuestionReceived;
        event EventHandler FinaleQuestionCompleted;
        event EventHandler FinaleQuestionAnswersShowed;
        event EventHandler<EventArgs<string>> FinaleQuestionAnswerCorrect;
        event EventHandler<EventArgs<string>> GameWon;
    }
}
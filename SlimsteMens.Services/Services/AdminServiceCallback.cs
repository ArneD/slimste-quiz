using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using SlimsteMens.Services.Info;
using SlimsteMens.Services.Interfaces;
using SlimsteMens.Services.SlimsteAdminService;

namespace SlimsteMens.Services.Services
{
    [CallbackBehavior(UseSynchronizationContext = false) ]
    public class AdminServiceCallback : IAdminServiceCallback
    {
        public event EventHandler<EventArgs<GameInfo>> GameStarted;
        public event EventHandler<EventArgs<TurnTypeInfo>> TurnChanged;
        public event EventHandler<EventArgs<TimerChangeInfo>> TimerChanged;
        public event EventHandler<EventArgs<FinaleTimerChangeInfo>> FinaleTimerChanged;

        public event EventHandler Round369Started;
        public event EventHandler<EventArgs<ThreeSixNineQuestionInfo>> Question369Received;
        public event EventHandler Round369Ended;

        public event EventHandler RoundPuzzleStarted;
        public event EventHandler<EventArgs<IList<PuzzleQuestionInfo>>> PuzzleReceived;
        public event EventHandler<EventArgs<long>> PuzzleCorrect;
        public event EventHandler PuzzleAnswersShowed;
        public event EventHandler PuzzleCompleted; 
        public event EventHandler RoundPuzzleEnded;

        public event EventHandler RoundGalleryStarted;
        public event EventHandler GalleryCompleted;
        public event EventHandler<EventArgs<GalleryInfo>> QuestionGalleryReceived;
        public event EventHandler NextGalleryQuestionShowed;
        public event EventHandler NextGalleryAnswerShowed;
        public event EventHandler RoundGalleryEnded;

        public event EventHandler RoundVideoStarted;
        public event EventHandler VideoCompleted;
        public event EventHandler PlayVideoStarted;
        public event EventHandler PlayVideoCompleted;
        public event EventHandler VideoAnswersShowed;
        public event EventHandler<EventArgs<string>> VideoCorrect;
        public event EventHandler<EventArgs<VideoQuestionInfo>> QuestionVideoReceived;
        public event EventHandler RoundVideoEnded;

        public event EventHandler<EventArgs<FinaleInfo>> FinaleStarted;
        public event EventHandler<EventArgs<IList<TurnTypeInfo>>> TieBreakerQuestionReceived;
        public event EventHandler<EventArgs<FinaleTurnTypeInfo>> FinaleTurnChanged;
        public event EventHandler<EventArgs<FinaleQuestionInfo>> FinaleQuestionReceived;
        public event EventHandler FinaleQuestionCompleted;
        public event EventHandler FinaleQuestionAnswersShowed;
        public event EventHandler<EventArgs<string>> FinaleQuestionAnswerCorrect;
        public event EventHandler<EventArgs<string>> GameWon;

        public void OnGameStarted(GameDto game)
        {
            if(GameStarted != null)
                GameStarted(this, new EventArgs<GameInfo>(new GameInfo(game)));
        }

        public void OnTurnChange(TurnTypeDto turn)
        {
            if(TurnChanged != null)
                TurnChanged(this, new EventArgs<TurnTypeInfo>(turn.ToInfo()));
        }

        public void OnSetTimer(TurnTypeDto turn, int seconds)
        {
            if(TimerChanged != null)
                TimerChanged(this, new EventArgs<TimerChangeInfo>(new TimerChangeInfo(turn, seconds)));
        }

        #region 3-6-9

        public void OnStartRound369()
        {
            if(Round369Started != null)
                Round369Started(this, new EventArgs());
        }

        public void OnQuestion369(ThreeSixNineQuestionDto question)
        {
            if(Question369Received != null)
                Question369Received(this, new EventArgs<ThreeSixNineQuestionInfo>(new ThreeSixNineQuestionInfo(question)));
        }

        public void OnEndRound369()
        {
            if(Round369Ended != null)
                Round369Ended(this, new EventArgs());
        }
        #endregion 3-6-9

        #region Puzzle
        public void OnStartRoundPuzzle()
        {
            if(RoundPuzzleStarted != null)
                RoundPuzzleStarted(this, new EventArgs());
        }

        public void OnQuestionPuzzle(List<PuzzleQuestionDto> puzzle)
        {
            if (PuzzleReceived != null)
                PuzzleReceived(this, new EventArgs<IList<PuzzleQuestionInfo>>(puzzle.Select(s => new PuzzleQuestionInfo(s)).ToList()));
        }

        public void OnPuzzleCorrect(long puzzleId)
        {
            if (PuzzleCorrect != null)
                PuzzleCorrect(this, new EventArgs<long>(puzzleId));
        }

        public void OnQuestionPuzzleCompleted()
        {
            if(PuzzleCompleted != null)
                PuzzleCompleted(this, new EventArgs());
        }

        public void OnShowPuzzleAnswers()
        {
            if(PuzzleAnswersShowed != null)
                PuzzleAnswersShowed(this, new EventArgs());
        }

        public void OnEndRoundPuzzle()
        {
            if(RoundPuzzleEnded != null)
                RoundPuzzleEnded(this, new EventArgs());
        }
        #endregion

        #region Gallery
        /// <summary>
        /// Called when [start round gallery].
        /// </summary>
        public void OnStartRoundGallery()
        {
            if(RoundGalleryStarted != null)
                RoundGalleryStarted(this, new EventArgs());
        }

        public void OnNextGalleryQuestion()
        {
            if(NextGalleryQuestionShowed != null)
                NextGalleryQuestionShowed(this, new EventArgs());
        }

        public void OnGalleryCompleted()
        {
            if(GalleryCompleted != null)
                GalleryCompleted(this, new EventArgs());
        }

        public void OnQuestionGallery(GalleryDto galleryDto)
        {
            if(QuestionGalleryReceived != null)
                QuestionGalleryReceived(this, new EventArgs<GalleryInfo>(new GalleryInfo(galleryDto)));
        }

        public void OnShowGalleryAnswer()
        {
            if(NextGalleryAnswerShowed != null)
                NextGalleryAnswerShowed(this, new EventArgs());
        }

        public void OnEndRoundGallery()
        {
            if(RoundGalleryEnded != null)
                RoundGalleryEnded(this, new EventArgs());
        }
        #endregion

        #region Video
        public void OnStartRoundVideo()
        {
            if (RoundVideoStarted != null)
                RoundVideoStarted(this, new EventArgs());
        }

        public void OnQuestionVideo(VideoDto videoDto)
        {
            if (QuestionVideoReceived != null)
                QuestionVideoReceived(this, new EventArgs<VideoQuestionInfo>(new VideoQuestionInfo(videoDto)));
        }

        public void OnPlayVideo()
        {
            if (PlayVideoStarted != null)
                PlayVideoStarted(this, new EventArgs());
        }

        public void OnPlayVideoCompleted()
        {
            if (PlayVideoCompleted != null)
                PlayVideoCompleted(this, new EventArgs());
        }

        public void OnVideoQuestionCorrect(string answer)
        {
            if (VideoCorrect != null)
                VideoCorrect(this, new EventArgs<string>(answer));
        }

        public void OnVideoCompleted()
        {
            if (VideoCompleted != null)
                VideoCompleted(this, new EventArgs());
        }

        public void OnShowVideoAnswers()
        {
            if (VideoAnswersShowed != null)
                VideoAnswersShowed(this, new EventArgs());
        }

        public void OnEndRoundVideo()
        {
            if(RoundVideoEnded != null)
                RoundVideoEnded(this, new EventArgs());
        }
        #endregion

        #region Finale

        public void OnFinaleStarted(FinaleDto finale)
        {
            if(FinaleStarted != null)
                FinaleStarted(this, new EventArgs<FinaleInfo>(new FinaleInfo(finale)));
        }

        public void OnTiebreakerQuestion(List<TurnTypeDto> turns)
        {
            if(TieBreakerQuestionReceived != null)
                TieBreakerQuestionReceived(this, new EventArgs<IList<TurnTypeInfo>>(turns.Select(s => s.ToInfo()).ToList()));
        }

        public void OnSetTimerFinale(FinaleTurnTypeDto turn, int seconds)
        {
            if(FinaleTimerChanged != null)
                FinaleTimerChanged(this, new EventArgs<FinaleTimerChangeInfo>(new FinaleTimerChangeInfo(turn, seconds)));
        }

        public void OnFinaleQuestion(FinaleQuestionDto finaleQuestionDto)
        {
            if(FinaleQuestionReceived != null)
                FinaleQuestionReceived(this, new EventArgs<FinaleQuestionInfo>(new FinaleQuestionInfo(finaleQuestionDto)));
        }

        public void OnFinaleQuestionCompleted()
        {
            if(FinaleQuestionCompleted != null)
                FinaleQuestionCompleted(this, new EventArgs());
        }

        public void OnShowFinaleQuestionAnswers()
        {
            if(FinaleQuestionAnswersShowed != null)
                FinaleQuestionAnswersShowed(this, new EventArgs());
        }

        public void OnFinaleAnswerCorrect(string answer)
        {
            if(FinaleQuestionAnswerCorrect != null)
                FinaleQuestionAnswerCorrect(this, new EventArgs<string>(answer));
        }

        public void OnFinaleTurnChange(FinaleTurnTypeDto turn)
        {
            if(FinaleTurnChanged != null)
                FinaleTurnChanged(this, new EventArgs<FinaleTurnTypeInfo>(turn.ToInfo()));
        }

        public void OnGameWon(string winner)
        {
            if(GameWon != null)
                GameWon(this, new EventArgs<string>(winner));
        }
        #endregion Finale
    }
}

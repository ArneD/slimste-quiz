using SlimsteMens.Service.DataContracts;
using System.Collections.Generic;
using System.ServiceModel;

namespace SlimsteMens.Service
{
    interface ISlimsteCallback
    {
        [OperationContract(IsOneWay = true)]
        void OnGameStarted(GameDto game);

        [OperationContract(IsOneWay = true)]
        void OnFinaleStarted(FinaleDto finale);

        [OperationContract(IsOneWay = true)]
        void OnSetTimer(TurnTypeDto turn, int seconds);

        [OperationContract(IsOneWay = true)]
        void OnSetTimerFinale(FinaleTurnTypeDto turn, int seconds);

        [OperationContract(IsOneWay = true)]
        void OnTurnChange(TurnTypeDto turn);

        [OperationContract(IsOneWay = true)]
        void OnFinaleTurnChange(FinaleTurnTypeDto turn);

        [OperationContract(IsOneWay = true)]
        void OnStartRound369();

        [OperationContract(IsOneWay = true)]
        void OnQuestion369(ThreeSixNineQuestionDto question);

        [OperationContract(IsOneWay = true)]
        void OnEndRound369();

        [OperationContract(IsOneWay = true)]
        void OnStartRoundPuzzle();

        [OperationContract(IsOneWay = true)]
        void OnQuestionPuzzle(IEnumerable<PuzzleQuestionDto> puzzle);

        [OperationContract(IsOneWay = true)]
        void OnPuzzleCorrect(long puzzleId);

        [OperationContract(IsOneWay = true)]
        void OnQuestionPuzzleCompleted();

        [OperationContract(IsOneWay = true)]
        void OnShowPuzzleAnswers();

        [OperationContract(IsOneWay = true)]
        void OnEndRoundPuzzle();

        [OperationContract(IsOneWay = true)]
        void OnStartRoundGallery();

        [OperationContract(IsOneWay = true)]
        void OnNextGalleryQuestion();

        [OperationContract(IsOneWay = true)]
        void OnGalleryCompleted();
        
        [OperationContract(IsOneWay = true)]
        void OnQuestionGallery(GalleryDto galleryDto);

        [OperationContract(IsOneWay = true)]
        void OnShowGalleryAnswer();

        [OperationContract(IsOneWay = true)]
        void OnEndRoundGallery();

        [OperationContract(IsOneWay = true)]
        void OnStartRoundVideo();

        [OperationContract(IsOneWay = true)]
        void OnQuestionVideo(VideoDto videoDto);

        [OperationContract(IsOneWay = true)]
        void OnPlayVideo();

        [OperationContract(IsOneWay = true)]
        void OnPlayVideoCompleted();

        [OperationContract(IsOneWay = true)]
        void OnVideoQuestionCorrect(string answer);

        [OperationContract(IsOneWay = true)]
        void OnVideoCompleted();

        [OperationContract(IsOneWay = true)]
        void OnShowVideoAnswers();

        [OperationContract(IsOneWay = true)]
        void OnEndRoundVideo();

        [OperationContract(IsOneWay = true)]
        void OnTiebreakerQuestion(IList<TurnTypeDto> players);

        [OperationContract(IsOneWay = true)]
        void OnFinaleQuestion(FinaleQuestionDto finaleQuestionDto);

        [OperationContract(IsOneWay = true)]
        void OnFinaleQuestionCompleted();

        [OperationContract(IsOneWay = true)]
        void OnShowFinaleQuestionAnswers();

        [OperationContract(IsOneWay = true)]
        void OnFinaleAnswerCorrect(string answer);

        [OperationContract(IsOneWay = true)]
        void OnGameWon(string winner);
    }
}

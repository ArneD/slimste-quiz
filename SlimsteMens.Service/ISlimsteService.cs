using System.Collections.Generic;
using System.ServiceModel;
using SlimsteMens.Service.DataContracts;

namespace SlimsteMens.Service
{
    [ServiceContract(CallbackContract = typeof(ISlimsteCallback))]
    public interface ISlimsteService
    {
        #region General
        [OperationContract(IsOneWay = true)]
        void Start(GameDto game);

        [OperationContract(IsOneWay = true)]
        void ResetGame(ResetGameDto resetGameDto);

        [OperationContract]
        bool Subscribe();

        [OperationContract(IsOneWay = true)]
        void Unsubscribe();

        [OperationContract(IsOneWay = true)]
        void StartTimer();

        [OperationContract(IsOneWay = true)]
        void StopTimer();

        [OperationContract(IsOneWay = true)]
        void NextRound();

        [OperationContract(IsOneWay = true)]
        void SetTime(TurnTypeDto turn, int time);
        #endregion General

        #region 3-6-9
        [OperationContract(IsOneWay = true)]
        void StartRound369();

        [OperationContract(IsOneWay = true)]
        void Question369Incorrect();

        [OperationContract(IsOneWay = true)]
        void Question369Correct();

        [OperationContract]
        bool AddQuestion369(ThreeSixNineQuestionDto questionDto);
        #endregion 3-6-9

        #region Puzzle
        [OperationContract(IsOneWay = true)]
        void StartRoundPuzzle();

        [OperationContract(IsOneWay = true)]
        void NextPuzzle();

        [OperationContract(IsOneWay = true)]
        void ShowPuzzleAnswers();

        [OperationContract(IsOneWay = true)]
        void NextTurnForPuzzle();

        [OperationContract(IsOneWay = true)]
        void QuestionPuzzleCorrect(long puzzleId);

        [OperationContract]
        bool AddPuzzleQuestion(PuzzleQuestionDto puzzleQuestionDto);
        #endregion

        #region Gallery

        [OperationContract(IsOneWay = true)]
        void StartRoundGallery();

        [OperationContract(IsOneWay = true)]
        void NextGallery();

        [OperationContract(IsOneWay = true)]
        void NextTurnForGallery();

        [OperationContract(IsOneWay = true)]
        void GalleryQuestionCorrect();

        [OperationContract(IsOneWay = true)]
        void GalleryQuestionIncorrect();

        [OperationContract(IsOneWay = true)]
        void ShowNextGalleryQuestionAnswer();

        [OperationContract]
        bool AddGallery(GalleryDto gallery);

        #endregion

        #region Video

        [OperationContract(IsOneWay = true)]
        void StartRoundVideo();

        [OperationContract(IsOneWay = true)]
        void NextVideo();

        [OperationContract(IsOneWay = true)]
        void PlayVideo();

        [OperationContract(IsOneWay = true)]
        void PlayVideoCompleted();

        [OperationContract(IsOneWay = true)]
        void NextTurnForVideo();

        [OperationContract(IsOneWay = true)]
        void QuestionVideoCorrect(string answer);

        [OperationContract(IsOneWay = true)]
        void ShowVideoAnswers();

        [OperationContract]
        bool AddVideo(VideoDto video);

        #endregion

        #region Finale

        [OperationContract(IsOneWay = true)]
        void StartFinale();

        [OperationContract(IsOneWay = true)]
        void NextFinaleQuestion();

        [OperationContract(IsOneWay = true)]
        void ShowFinaleQuestionAnswers();

        [OperationContract(IsOneWay = true)]
        void NextTurnForFinaleQuestion();

        [OperationContract(IsOneWay = true)]
        void FinaleQuestionCorrect(string answer);

        [OperationContract(IsOneWay = true)]
        void SetTieBreakerWinner(IList<TurnTypeDto> winners);

        [OperationContract(IsOneWay = true)]
        void SetFinaleTime(FinaleTurnTypeDto turn, int time);

        [OperationContract]
        bool AddFinaleQuestion(FinaleQuestionDto finaleDto);

        #endregion
    }
}

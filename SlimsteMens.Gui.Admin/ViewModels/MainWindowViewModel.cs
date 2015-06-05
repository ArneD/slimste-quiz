using System;
using System.Collections.Generic;
using System.Windows.Threading;
using SlimsteMens.Gui.Controls.ViewModels;
using SlimsteMens.Services;
using SlimsteMens.Services.Info;
using SlimsteMens.Services.Interfaces;

namespace SlimsteMens.Gui.Admin.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly Stack<ViewModelBase> _viewHistory = new Stack<ViewModelBase>();
        private readonly IAdminService _adminService;
        private readonly StartViewModel _startViewModel;
        private readonly ScoreControlViewModel _score;
        private readonly FinaleRoundViewModel _finaleRoundViewModel;

        /// <summary>
        /// Gets or sets the employee view model.
        /// </summary>
        /// <value>
        /// The employee view model.
        /// </value>
        public ViewModelBase CurrentView
        {
            get { return _viewHistory.Peek(); }
        }
       
        /// <summary>
        /// Show the view model
        /// </summary>
        /// <param name="viewModel"></param>
        public void Show(ViewModelBase viewModel)
        {
            _viewHistory.Push(viewModel);
            OnPropertyChanged("CurrentView");
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
        /// </summary>
        public MainWindowViewModel(IAdminService adminService, ScoreControlViewModel scoreControlViewModel, FinaleRoundViewModel finaleRoundViewModel)
        {
            _adminService = adminService;
            _startViewModel = new StartViewModel(_adminService, this);
            _score = scoreControlViewModel;
            _finaleRoundViewModel = finaleRoundViewModel;
            _finaleRoundViewModel.ScoreView = new FinalScoreControlViewModel();

            _adminService.AdminServiceCallback.GameStarted += AdminServiceCallbackGameStarted;
            _adminService.AdminServiceCallback.TurnChanged += AdminServiceCallbackOnTurnChanged;
            _adminService.AdminServiceCallback.TimerChanged += AdminServiceCallbackOnTimerChanged;
            _adminService.AdminServiceCallback.Round369Started += AdminServiceCallbackOnRound369Started;
            _adminService.AdminServiceCallback.RoundPuzzleStarted += AdminServiceCallbackOnRoundPuzzleStarted;
            _adminService.AdminServiceCallback.RoundGalleryStarted += AdminServiceCallbackOnRoundGalleryStarted;
            _adminService.AdminServiceCallback.RoundVideoStarted += AdminServiceCallbackOnRoundVideoStarted;
            _adminService.AdminServiceCallback.TieBreakerQuestionReceived += AdminServiceCallbackOnTieBreakerQuestionReceived;
            _adminService.AdminServiceCallback.FinaleStarted += AdminServiceCallbackOnFinaleStarted;
            Show(_startViewModel);
        }

        #region Events
        void AdminServiceCallbackGameStarted(object sender, EventArgs<GameInfo> e)
        {
            Dispatch(() =>
            {
                _score.Team1 = e.Value.Team1;
                _score.Team2 = e.Value.Team2;
                _score.Team3 = e.Value.Team3;
                _score.Team1Seconds =
                    e.Value.StartSeconds;
                _score.Team2Seconds =
                    e.Value.StartSeconds;
                _score.Team3Seconds =
                    e.Value.StartSeconds;
                _score.Turn =
                    TurnTypeInfo.Team1;
            }, DispatcherPriority.DataBind);
        }

        private void AdminServiceCallbackOnRound369Started(object sender, EventArgs eventArgs)
        {
            Dispatch(()
                => Show(new ThreeSixNineRoundViewModel(_adminService) { ScoreView = _score }), DispatcherPriority.DataBind);
        }

        private void AdminServiceCallbackOnRoundPuzzleStarted(object sender, EventArgs eventArgs)
        {
            Dispatch(()
                => Show(new PuzzleRoundViewModel(_adminService, _score)), DispatcherPriority.DataBind);
        }

        private void AdminServiceCallbackOnTimerChanged(object sender, EventArgs<TimerChangeInfo> eventArgs)
        {
            Dispatch(() =>
                _score.SetSecondsByTurn(eventArgs.Value.Turn, eventArgs.Value.Seconds), DispatcherPriority.DataBind);
        }

        private void AdminServiceCallbackOnTurnChanged(object sender, EventArgs<TurnTypeInfo> eventArgs)
        {
            Dispatch(() =>
            {
                _score.Turn = eventArgs.Value;
            }, DispatcherPriority.DataBind);
        }

        private void AdminServiceCallbackOnRoundGalleryStarted(object sender, EventArgs eventArgs)
        {
            Dispatch(()
                => Show(new GalleryRoundViewModel(_adminService, _score)), DispatcherPriority.DataBind);
        }

        private void AdminServiceCallbackOnRoundVideoStarted(object sender, EventArgs eventArgs)
        {
            Dispatch(()
                => Show(new VideoRoundViewModel(_adminService, _score)), DispatcherPriority.DataBind);
        }

        private void AdminServiceCallbackOnTieBreakerQuestionReceived(object sender, EventArgs<IList<TurnTypeInfo>> eventArgs)
        {
            Dispatch(()
                => Show(new TieBreakerQuestionViewModel(_adminService, _score, eventArgs.Value)), DispatcherPriority.DataBind);
        }

        private void AdminServiceCallbackOnFinaleStarted(object sender, EventArgs<FinaleInfo> eventArgs)
        {
            Dispatch(() =>
            {
                _finaleRoundViewModel.ScoreView.Team1 = eventArgs.Value.Team1;
                _finaleRoundViewModel.ScoreView.Team2 = eventArgs.Value.Team2;
                _finaleRoundViewModel.ScoreView.Team1Seconds = eventArgs.Value.Team1Seconds;
                _finaleRoundViewModel.ScoreView.Team2Seconds = eventArgs.Value.Team2Seconds;
                Show(_finaleRoundViewModel);
            }, DispatcherPriority.DataBind);
        }
        #endregion

        public void BackToStart()
        {
            Show(_startViewModel);
        }
    }
}

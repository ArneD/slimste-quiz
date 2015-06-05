using System;
using System.Collections.Generic;
using System.Windows.Threading;
using SlimsteMens.Gui.Controls.ViewModels;
using SlimsteMens.Services;
using SlimsteMens.Services.Info;
using SlimsteMens.Services.Interfaces;

namespace SlimsteMens.Gui.App.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ThreeSixNineViewModel ThreeSixNineViewModel { get; private set; }
        public PuzzleViewModel PuzzleViewModel { get; private set; }
        public GalleryViewModel GalleryViewModel { get; private set; }
        public VideoViewModel VideoViewModel { get; private set; }
        public FinaleViewModel FinaleViewModel { get; private set; }
        public ImageViewModel ImageViewModel { get; private set; }
        private readonly Stack<ViewModelBase> _viewHistory = new Stack<ViewModelBase>();
        private ScoreControlViewModel _score;

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
        public MainWindowViewModel(IAdminService adminService, StartViewModel startViewModel, ThreeSixNineViewModel threeSixNineViewModel, PuzzleViewModel puzzleViewModel, GalleryViewModel galleryViewModel, VideoViewModel videoViewModel, FinaleViewModel finaleViewModel, ImageViewModel imageViewModel)
        {
            ThreeSixNineViewModel = threeSixNineViewModel;
            PuzzleViewModel = puzzleViewModel;
            GalleryViewModel = galleryViewModel;
            VideoViewModel = videoViewModel;
            FinaleViewModel = finaleViewModel;
            ImageViewModel = imageViewModel;
            Show(startViewModel);
            adminService.AdminServiceCallback.GameStarted += AdminServiceCallbackGameStarted;
            adminService.AdminServiceCallback.TurnChanged += AdminServiceCallbackOnTurnChanged;
            adminService.AdminServiceCallback.TimerChanged += AdminServiceCallbackOnTimerChanged;
            adminService.AdminServiceCallback.Round369Started += AdminServiceCallbackOnRound369Started;
            adminService.AdminServiceCallback.RoundPuzzleStarted += AdminServiceCallbackOnRoundPuzzleStarted;
            adminService.AdminServiceCallback.RoundGalleryStarted += AdminServiceCallbackOnRoundGalleryStarted;
            adminService.AdminServiceCallback.RoundVideoStarted += AdminServiceCallbackOnRoundVideoStarted;
            adminService.AdminServiceCallback.FinaleStarted += AdminServiceCallbackOnFinaleStarted;

            threeSixNineViewModel.OnSetImage += threeSixNineViewModel_OnSetImage;
            imageViewModel.OnImageShown += imageViewModel_OnImageShown;
        }
        
        #region Events
        private void threeSixNineViewModel_OnSetImage(object sender, byte[] image)
        {
            ImageViewModel.SetImage(image);
            Show(ImageViewModel);
        }
        private void imageViewModel_OnImageShown(object sender, EventArgs e)
        {
            Show(ThreeSixNineViewModel);
        }

        private void AdminServiceCallbackGameStarted(object sender, EventArgs<GameInfo> e)
        {
            Dispatch(() =>
            {
                _score = new ScoreControlViewModel
                {
                    Team1 = e.Value.Team1,
                    Team2 = e.Value.Team2,
                    Team3 = e.Value.Team3,
                    Team1Seconds = e.Value.StartSeconds,
                    Team2Seconds = e.Value.StartSeconds,
                    Team3Seconds = e.Value.StartSeconds,
                    Turn = TurnTypeInfo.Team1,
                };
            }, DispatcherPriority.DataBind);
        }

        private void AdminServiceCallbackOnRound369Started(object sender, EventArgs eventArgs)
        {
            Dispatch(() =>
            {
                Show(ThreeSixNineViewModel);
                ThreeSixNineViewModel.ScoreView = _score;
            }, DispatcherPriority.DataBind);
        }

        private void AdminServiceCallbackOnRoundPuzzleStarted(object sender, EventArgs eventArgs)
        {
            Dispatch(() =>
            {
                Show(PuzzleViewModel);
                PuzzleViewModel.ScoreView = _score;
            }, DispatcherPriority.DataBind);
        }

        private void AdminServiceCallbackOnRoundGalleryStarted(object sender, EventArgs eventArgs)
        {
            Dispatch(() =>
            {
                Show(GalleryViewModel);
                GalleryViewModel.ScoreView = _score;
            }, DispatcherPriority.DataBind);
        }

        private void AdminServiceCallbackOnRoundVideoStarted(object sender, EventArgs eventArgs)
        {
            Dispatch(() =>
            {
                Show(VideoViewModel);
                VideoViewModel.ScoreView = _score;
            }, DispatcherPriority.DataBind);
        }

        private void AdminServiceCallbackOnTimerChanged(object sender, EventArgs<TimerChangeInfo> eventArgs)
        {
            Dispatch(() =>
                _score.SetSecondsByTurn(
                    eventArgs.Value.Turn,
                    eventArgs.Value.Seconds), DispatcherPriority.DataBind);
        }

        private void AdminServiceCallbackOnTurnChanged(object sender, EventArgs<TurnTypeInfo> eventArgs)
        {
            Dispatch(() =>
            {
                _score.Turn = eventArgs.Value;
            }, DispatcherPriority.DataBind);
        }

        private void AdminServiceCallbackOnFinaleStarted(object sender, EventArgs<FinaleInfo> eventArgs)
        {
            Dispatch(() =>
            {
                FinaleViewModel.ScoreView.Team1 = eventArgs.Value.Team1;
                FinaleViewModel.ScoreView.Team2 = eventArgs.Value.Team2;
                FinaleViewModel.ScoreView.Team1Seconds = eventArgs.Value.Team1Seconds;
                FinaleViewModel.ScoreView.Team2Seconds = eventArgs.Value.Team2Seconds;
                Show(FinaleViewModel);
            }, DispatcherPriority.DataBind);
        }

        #endregion
    }
}

using System;
using System.Windows.Threading;
using SlimsteMens.Gui.Controls;
using SlimsteMens.Gui.Controls.ViewModels;
using SlimsteMens.Services;
using SlimsteMens.Services.Info;
using SlimsteMens.Services.Interfaces;

namespace SlimsteMens.Gui.Admin.ViewModels
{
    public class VideoRoundViewModel : ViewModelBase
    {
        private readonly IAdminService _adminService;
        private ScoreControlViewModel _scoreView;
        private VideoQuestionInfo _videoQuestion;
        private bool _isAnswer1Checked;
        private bool _isAnswer2Checked;
        private bool _isAnswer3Checked;
        private bool _isAnswer4Checked;
        private bool _isAnswer5Checked;
        private int _playersPlayedQuestion;

        public ScoreControlViewModel ScoreView
        {
            get { return _scoreView; }
            private set
            {
                _scoreView = value;
                OnPropertyChanged("ScoreView");
            }
        }

        public VideoQuestionInfo VideoQuestion
        {
            get { return _videoQuestion; }
            set
            {
                _videoQuestion = value;
                OnPropertyChanged("VideoQuestion");
            }
        }

        public bool IsAnswer1Checked
        {
            get { return _isAnswer1Checked; }
            set
            {
                _isAnswer1Checked = value;
                OnPropertyChanged("IsAnswer1Checked");
            }
        }

        public bool IsAnswer2Checked
        {
            get { return _isAnswer2Checked; }
            set
            {
                _isAnswer2Checked = value;
                OnPropertyChanged("IsAnswer2Checked");
            }
        }

        public bool IsAnswer3Checked
        {
            get { return _isAnswer3Checked; }
            set
            {
                _isAnswer3Checked = value;
                OnPropertyChanged("IsAnswer3Checked");
            }
        }

        public bool IsAnswer4Checked
        {
            get { return _isAnswer4Checked; }
            set
            {
                _isAnswer4Checked = value;
                OnPropertyChanged("IsAnswer4Checked");
            }
        }

        public bool IsAnswer5Checked
        {
            get { return _isAnswer5Checked; }
            set
            {
                _isAnswer5Checked = value;
                OnPropertyChanged("IsAnswer5Checked");
            }
        }

        public RelayCommand PlayVideoCommand { get; private set; }
        public RelayCommand NextVideoCommand { get; private set; }
        public RelayCommand SkipVideoCommand { get; private set; }
        public RelayCommand NextRoundCommand { get; private set; }
        public RelayCommand StartTimerCommand { get; private set; }
        public RelayCommand StopTimerCommand { get; private set; }
        public RelayCommand NextTurnCommand { get; private set; }
        public RelayCommand ShowAnswersCommand { get; private set; }
        public RelayCommand<string> AnswerCorrectCommand { get; private set; }

        private void AdminServiceCallbackOnRoundVideoEnded(object sender, EventArgs eventArgs)
        {
            Dispatch(() =>
            {
                _adminService.AdminServiceCallback.PlayVideoCompleted -= AdminServiceCallbackOnPlayVideoCompleted;
                _adminService.AdminServiceCallback.QuestionVideoReceived -= AdminServiceCallbackOnQuestionVideoReceived;
                _adminService.AdminServiceCallback.VideoCompleted -= AdminServiceCallbackOnVideoCompleted;
                _adminService.AdminServiceCallback.RoundVideoEnded -= AdminServiceCallbackOnRoundVideoEnded;
                NextRoundCommand.IsEnabled = true;
            }, DispatcherPriority.DataBind);

        }

        private void AdminServiceCallbackOnVideoCompleted(object sender, EventArgs eventArgs)
        {
            Dispatch(() =>
            {
                StopTimerCommand.IsEnabled = false;
                NextVideoCommand.IsEnabled = true;
                AnswerCorrectCommand.IsEnabled = false;
            }, DispatcherPriority.DataBind);
        }

        private void AdminServiceCallbackOnQuestionVideoReceived(object sender, EventArgs<VideoQuestionInfo> eventArgs)
        {
            Dispatch(() =>
            {
                PlayVideoCommand.IsEnabled = true;
                VideoQuestion = eventArgs.Value;
                IsAnswer1Checked = false;
                IsAnswer2Checked = false;
                IsAnswer3Checked = false;
                IsAnswer4Checked = false;
                IsAnswer5Checked = false;
                _playersPlayedQuestion = 1;
            }, DispatcherPriority.DataBind);
        }

        private void AdminServiceCallbackOnPlayVideoCompleted(object sender, EventArgs eventArgs)
        {
            Dispatch(() =>
            {
                StartTimerCommand.IsEnabled = true;
                AnswerCorrectCommand.IsEnabled = true;
                SkipVideoCommand.IsEnabled = false;

            }, DispatcherPriority.DataBind);
        }

        public VideoRoundViewModel(IAdminService adminService, ScoreControlViewModel scoreControlViewModel) 
        {
            _adminService = adminService;
            _adminService.AdminServiceCallback.PlayVideoCompleted += AdminServiceCallbackOnPlayVideoCompleted;
            _adminService.AdminServiceCallback.QuestionVideoReceived += AdminServiceCallbackOnQuestionVideoReceived;
            _adminService.AdminServiceCallback.VideoCompleted += AdminServiceCallbackOnVideoCompleted;
            _adminService.AdminServiceCallback.RoundVideoEnded += AdminServiceCallbackOnRoundVideoEnded;
            PlayVideoCommand = new RelayCommand(PlayVideo);
            NextVideoCommand = new RelayCommand(NextVideo) { IsEnabled = true };
            SkipVideoCommand = new RelayCommand(SkipVideo);
            StartTimerCommand = new RelayCommand(StartTimer);
            StopTimerCommand = new RelayCommand(StopTimer);
            NextTurnCommand = new RelayCommand(NextTurn);
            ShowAnswersCommand = new RelayCommand(ShowAnswers);
            NextRoundCommand = new RelayCommand(NextRound);
            AnswerCorrectCommand = new RelayCommand<string>(AnswerCorrect);
            ScoreView = scoreControlViewModel;
        }

        public void PlayVideo()
        {
            _adminService.PlayVideo();
            PlayVideoCommand.IsEnabled = false;
            SkipVideoCommand.IsEnabled = true;
            ShowAnswersCommand.IsEnabled = false;
        }

        public void NextVideo()
        {
            _adminService.NextVideo();
            NextVideoCommand.IsEnabled = false;
            ShowAnswersCommand.IsEnabled = false;
        }

        public void SkipVideo()
        {
            _adminService.VideoPlayed();
        }

        public void StartTimer()
        {
            _adminService.StartTimer();
            StartTimerCommand.IsEnabled = false;
            StopTimerCommand.IsEnabled = true;
        }

        public void StopTimer()
        {
            _adminService.StopTimer();
            StopTimerCommand.IsEnabled = false;
            NextTurnCommand.IsEnabled = true;
        }

        public void NextTurn()
        {
            _adminService.NextTurnForVideo();
            NextTurnCommand.IsEnabled = false;
            StopTimerCommand.IsEnabled = true;
            _playersPlayedQuestion++;
            if (_playersPlayedQuestion > 3)
                ShowAnswersCommand.IsEnabled = true;
        }

        public void ShowAnswers()
        {
            _adminService.ShowVideoAnswers();
            ShowAnswersCommand.IsEnabled = false;
        }

        public void NextRound()
        {
            _adminService.NextRound();
        }

        public void AnswerCorrect(string answer)
        {
            _adminService.VideoAnswerCorrect(answer);
        }
    }
}

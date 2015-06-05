using System;
using System.Globalization;
using System.Windows.Threading;
using SlimsteMens.Gui.Controls.ViewModels;
using SlimsteMens.Services;
using SlimsteMens.Services.Info;
using SlimsteMens.Services.Interfaces;

namespace SlimsteMens.Gui.App.ViewModels
{
    /// <summary>
    /// Video View Model
    /// </summary>
    public class VideoViewModel : ViewModelBase
    {
        private readonly IAdminService _adminService;
        private ScoreControlViewModel _scoreView;
        private VideoQuestionInfo _videoQuestion;
        private bool _isVideoPlaying;
        private bool _isAnswer1Enabled;
        private bool _isAnswer2Enabled;
        private bool _isAnswer3Enabled;
        private bool _isAnswer4Enabled;
        private bool _isAnswer5Enabled;
        private string _score1Text;
        private string _score2Text;
        private string _score3Text;
        private string _score4Text;
        private string _score5Text;
        private int _score;
        public event EventHandler OnPlayVideo;
        public event EventHandler OnPlayVideoCompleted;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is video playing.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is video playing; otherwise, <c>false</c>.
        /// </value>
        public bool IsVideoPlaying
        {
            get { return _isVideoPlaying; }
            set
            {
                _isVideoPlaying = value;
                OnPropertyChanged("IsVideoPlaying");
            }
        }

        /// <summary>
        /// Gets the score view.
        /// </summary>
        /// <value>
        /// The score view.
        /// </value>
        public ScoreControlViewModel ScoreView
        {
            get { return _scoreView; }
            set
            {
                _scoreView = value;
                OnPropertyChanged("ScoreView");
            }
        }

        /// <summary>
        /// Gets or sets the video question.
        /// </summary>
        /// <value>
        /// The video question.
        /// </value>
        public VideoQuestionInfo VideoQuestion
        {
            get { return _videoQuestion; }
            set
            {
                _videoQuestion = value;
                OnPropertyChanged("VideoQuestion");
            }
        }

        public bool IsAnswer1Enabled
        {
            get { return _isAnswer1Enabled; }
            set
            {
                _isAnswer1Enabled = value;
                OnPropertyChanged("IsAnswer1Enabled");
            }
        }

        public bool IsAnswer2Enabled
        {
            get { return _isAnswer2Enabled; }
            set
            {
                _isAnswer2Enabled = value;
                OnPropertyChanged("IsAnswer2Enabled");
            }
        }

        public bool IsAnswer3Enabled
        {
            get { return _isAnswer3Enabled; }
            set
            {
                _isAnswer3Enabled = value;
                OnPropertyChanged("IsAnswer3Enabled");
            }
        }

        public bool IsAnswer4Enabled
        {
            get { return _isAnswer4Enabled; }
            set
            {
                _isAnswer4Enabled = value;
                OnPropertyChanged("IsAnswer4Enabled");
            }
        }

        public bool IsAnswer5Enabled
        {
            get { return _isAnswer5Enabled; }
            set
            {
                _isAnswer5Enabled = value;
                OnPropertyChanged("IsAnswer5Enabled");
            }
        }

        public string Score1Text
        {
            get { return _score1Text; }
            set
            {
                _score1Text = value;
                OnPropertyChanged("Score1Text");
            }
        }

        public string Score2Text
        {
            get { return _score2Text; }
            set
            {
                _score2Text = value;
                OnPropertyChanged("Score2Text");
            }
        }

        public string Score3Text
        {
            get { return _score3Text; }
            set
            {
                _score3Text = value;
                OnPropertyChanged("Score3Text");
            }
        }

        public string Score4Text
        {
            get { return _score4Text; }
            set
            {
                _score4Text = value;
                OnPropertyChanged("Score4Text");
            }
        }

        public string Score5Text
        {
            get { return _score5Text; }
            set
            {
                _score5Text = value;
                OnPropertyChanged("Score5Text");
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VideoViewModel" /> class.
        /// </summary>
        public VideoViewModel(IAdminService adminService)
        {
            _adminService = adminService;
            adminService.AdminServiceCallback.PlayVideoStarted += AdminServiceCallbackOnPlayVideoStarted;
            adminService.AdminServiceCallback.QuestionVideoReceived += AdminServiceCallbackOnQuestionVideoReceived;
            adminService.AdminServiceCallback.VideoCorrect += AdminServiceCallbackOnVideoCorrect;
            adminService.AdminServiceCallback.VideoAnswersShowed += AdminServiceCallbackOnVideoAnswersShowed;
            adminService.AdminServiceCallback.RoundVideoEnded += AdminServiceCallbackOnRoundVideoEnded;
            adminService.AdminServiceCallback.PlayVideoCompleted += AdminServiceCallbackOnPlayVideoCompleted;
        }

        /// <summary>
        /// Video ended
        /// </summary>
        public void VideoEnded()
        {
            _adminService.VideoPlayed();
            IsVideoPlaying = false;
        }

        #region Events
        private void AdminServiceCallbackOnVideoAnswersShowed(object sender, EventArgs eventArgs)
        {
            Dispatch(() =>
            {
                IsAnswer1Enabled = true;
                IsAnswer2Enabled = true;
                IsAnswer3Enabled = true;
                IsAnswer4Enabled = true;
                IsAnswer5Enabled = true;
            }, DispatcherPriority.DataBind);
        }

        private void AdminServiceCallbackOnVideoCorrect(object sender, EventArgs<string> eventArgs)
        {
            Dispatch(() =>
            {
                if (String.Equals(eventArgs.Value, VideoQuestion.Answer1, StringComparison.InvariantCultureIgnoreCase))
                {
                    Score1Text = _score.ToString(CultureInfo.InvariantCulture);
                    IsAnswer1Enabled = true;
                }
                else if (String.Equals(eventArgs.Value, VideoQuestion.Answer2, StringComparison.InvariantCultureIgnoreCase))
                {
                    Score2Text = _score.ToString(CultureInfo.InvariantCulture);
                    IsAnswer2Enabled = true;
                }
                else if (String.Equals(eventArgs.Value, VideoQuestion.Answer3, StringComparison.InvariantCultureIgnoreCase))
                {
                    Score3Text = _score.ToString(CultureInfo.InvariantCulture);
                    IsAnswer3Enabled = true;
                }
                else if (String.Equals(eventArgs.Value, VideoQuestion.Answer4, StringComparison.InvariantCultureIgnoreCase))
                {
                    Score4Text = _score.ToString(CultureInfo.InvariantCulture);
                    IsAnswer4Enabled = true;
                }
                else if (String.Equals(eventArgs.Value, VideoQuestion.Answer5, StringComparison.InvariantCultureIgnoreCase))
                {
                    Score5Text = _score.ToString(CultureInfo.InvariantCulture);
                    IsAnswer5Enabled = true;
                }
                _score += 10;
            }, DispatcherPriority.DataBind);
        }

        private void AdminServiceCallbackOnQuestionVideoReceived(object sender, EventArgs<VideoQuestionInfo> eventArgs)
        {
            Dispatch(() =>
            {
                VideoQuestion = eventArgs.Value;
                IsVideoPlaying = true;
                _score = 10;
                IsAnswer1Enabled = false;
                IsAnswer2Enabled = false;
                IsAnswer3Enabled = false;
                IsAnswer4Enabled = false;
                IsAnswer5Enabled = false;
                Score1Text = "";
                Score2Text = "";
                Score3Text = "";
                Score4Text = "";
                Score5Text = "";
            }, DispatcherPriority.DataBind);
        }

        private void AdminServiceCallbackOnPlayVideoStarted(object sender, EventArgs eventArgs)
        {
            Dispatch(() =>
            {
                if(OnPlayVideo != null)
                    OnPlayVideo(this, new EventArgs());
            }, DispatcherPriority.DataBind);
        }

        private void AdminServiceCallbackOnPlayVideoCompleted(object sender, EventArgs eventArgs)
        {
            Dispatch(() =>
            {
                IsVideoPlaying = false;
                if (OnPlayVideoCompleted != null)
                    OnPlayVideoCompleted(this, new EventArgs());
            }, DispatcherPriority.DataBind);
        }

        private void AdminServiceCallbackOnRoundVideoEnded(object sender, EventArgs eventArgs)
        {
            Dispatch(() =>
            {
                _adminService.AdminServiceCallback.PlayVideoStarted -= AdminServiceCallbackOnPlayVideoStarted;
                _adminService.AdminServiceCallback.QuestionVideoReceived -= AdminServiceCallbackOnQuestionVideoReceived;
                _adminService.AdminServiceCallback.VideoCorrect -= AdminServiceCallbackOnVideoCorrect;
                _adminService.AdminServiceCallback.VideoAnswersShowed -= AdminServiceCallbackOnVideoAnswersShowed;
                _adminService.AdminServiceCallback.RoundVideoEnded -= AdminServiceCallbackOnRoundVideoEnded;
                _adminService.AdminServiceCallback.VideoCompleted -= AdminServiceCallbackOnPlayVideoCompleted;
            }, DispatcherPriority.DataBind);
        }
        #endregion
    }
}

using System.Windows.Threading;
using SlimsteMens.Gui.Controls;
using SlimsteMens.Gui.Controls.ViewModels;
using SlimsteMens.Services;
using SlimsteMens.Services.Info;
using SlimsteMens.Services.Interfaces;

namespace SlimsteMens.Gui.Admin.ViewModels
{
    public class FinaleRoundViewModel : ViewModelBase
    {
        private readonly IAdminService _adminService;
        private FinalScoreControlViewModel _scoreView;
        public FinalScoreControlViewModel ScoreView
        {
            get { return _scoreView; }
            set
            {
                _scoreView = value;
                OnPropertyChanged("ScoreView");
            }
        }

        public RelayCommand NextQuestionCommand { get; set; }
        public RelayCommand StartTimerCommand { get; set; }
        public RelayCommand StopTimerCommand { get; set; }
        public RelayCommand NextPlayerCommand { get; set; }
        public RelayCommand ShowAnswersCommand { get; set; }

        public RelayCommand<string> CheckBoxCommand { get; set; }

        public bool IsCheckBox1Checked
        {
            get { return _isCheckBox1Checked; }
            set
            {
                _isCheckBox1Checked = value;
                OnPropertyChanged("IsCheckBox1Checked");
            }
        }

        public bool IsCheckBox2Checked
        {
            get { return _isCheckBox2Checked; }
            set
            {
                _isCheckBox2Checked = value;
                OnPropertyChanged("IsCheckBox2Checked");
            }
        }

        public bool IsCheckBox3Checked
        {
            get { return _isCheckBox3Checked; }
            set
            {
                _isCheckBox3Checked = value;
                OnPropertyChanged("IsCheckBox3Checked");
            }
        }

        public bool IsCheckBox4Checked
        {
            get { return _isCheckBox4Checked; }
            set
            {
                _isCheckBox4Checked = value;
                OnPropertyChanged("IsCheckBox4Checked");
            }
        }

        public bool IsCheckBox5Checked
        {
            get { return _isCheckBox5Checked; }
            set
            {
                _isCheckBox5Checked = value;
                OnPropertyChanged("IsCheckBox5Checked");
            }
        }

        public FinaleQuestionInfo Question
        {
            get { return _question; }
            set
            {
                _question = value;
                OnPropertyChanged("Question");
                IsCheckBox1Checked = false;
                IsCheckBox2Checked = false;
                IsCheckBox3Checked = false;
                IsCheckBox4Checked = false;
                IsCheckBox5Checked = false;
            }
        }

        private bool _firstPlayerPlayed;
        private FinaleQuestionInfo _question;
        private bool _isCheckBox1Checked;
        private bool _isCheckBox2Checked;
        private bool _isCheckBox3Checked;
        private bool _isCheckBox4Checked;
        private bool _isCheckBox5Checked;

        public FinaleRoundViewModel(IAdminService adminService)
        {
            _adminService = adminService;
            _adminService.AdminServiceCallback.FinaleTurnChanged += AdminServiceCallbackOnFinaleTurnChanged;
            _adminService.AdminServiceCallback.FinaleQuestionReceived += AdminServiceCallbackOnFinaleQuestionReceived;
            _adminService.AdminServiceCallback.FinaleTimerChanged += AdminServiceCallbackOnFinaleTimerChanged;
            _adminService.AdminServiceCallback.GameWon += AdminServiceCallbackOnGameWon;
            NextQuestionCommand = new RelayCommand(NextQuestion){IsEnabled = true};
            StartTimerCommand = new RelayCommand(StartTimer);
            StopTimerCommand = new RelayCommand(StopTimer);
            NextPlayerCommand = new RelayCommand(NextPlayer);
            ShowAnswersCommand = new RelayCommand(ShowAnswers);
            CheckBoxCommand = new RelayCommand<string>(CorrectAnswer) {IsEnabled = true};
        }

        public void NextQuestion()
        {
            NextQuestionCommand.IsEnabled = false;
            ShowAnswersCommand.IsEnabled = false;
            _firstPlayerPlayed = false;
            _adminService.NextFinaleQuestion();
        }

        public void StartTimer()
        {
            StopTimerCommand.IsEnabled = true;
            StartTimerCommand.IsEnabled = false;
            _adminService.StartTimer();
        }

        public void StopTimer()
        {
            StopTimerCommand.IsEnabled = false;
            _adminService.StopTimer();
            if(_firstPlayerPlayed)
            {
                NextQuestionCommand.IsEnabled = true;
                ShowAnswersCommand.IsEnabled = true;
            }
            else
            {
                _firstPlayerPlayed = true;
                NextPlayerCommand.IsEnabled = true;
            }
        }

        public void NextPlayer()
        {
            NextPlayerCommand.IsEnabled = false;
            StopTimerCommand.IsEnabled = true;
            _adminService.NextTurnForFinaleQuestion();
        }

        public void ShowAnswers()
        {
            ShowAnswersCommand.IsEnabled = false;
            _adminService.ShowFinaleQuestionAnswers();
        }

        public void CorrectAnswer(string answer)
        {
            _adminService.FinaleQuestionCorrect(answer);
        }

        #region Events

        private void AdminServiceCallbackOnFinaleTurnChanged(object sender, EventArgs<FinaleTurnTypeInfo> eventArgs)
        {
            Dispatch(() =>
            {
                ScoreView.Turn =
                    eventArgs.Value;
            }, DispatcherPriority.DataBind);
        }

        private void AdminServiceCallbackOnFinaleQuestionReceived(object sender, EventArgs<FinaleQuestionInfo> eventArgs)
        {
            Dispatch(() =>
            {
                StartTimerCommand.IsEnabled =
                    true;
                Question = eventArgs.Value;
            }, DispatcherPriority.DataBind);
        }

        private void AdminServiceCallbackOnFinaleTimerChanged(object sender, EventArgs<FinaleTimerChangeInfo> eventArgs)
        {
            Dispatch(() => ScoreView.SetSecondsByTurn(eventArgs.Value.Turn, eventArgs.Value.Seconds), DispatcherPriority.DataBind);
        }

        private void AdminServiceCallbackOnGameWon(object sender, EventArgs<string> eventArgs)
        {
            Dispatch(() =>
            {
                StopTimerCommand.IsEnabled = false;
                ShowAnswersCommand.IsEnabled = true;
            }, DispatcherPriority.DataBind);
        }
        #endregion
    }
}

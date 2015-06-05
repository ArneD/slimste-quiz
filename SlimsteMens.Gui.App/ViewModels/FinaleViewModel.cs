using System;
using System.Windows.Threading;
using SlimsteMens.Gui.Controls.ViewModels;
using SlimsteMens.Services;
using SlimsteMens.Services.Info;
using SlimsteMens.Services.Interfaces;

namespace SlimsteMens.Gui.App.ViewModels
{
    public class FinaleViewModel : ViewModelBase
    {
        private FinalScoreControlViewModel _scoreView;
        private bool _isAnswer1Enabled;
        private bool _isAnswer2Enabled;
        private bool _isAnswer3Enabled;
        private bool _isAnswer4Enabled;
        private bool _isAnswer5Enabled;
        private FinaleQuestionInfo _question;

        public FinalScoreControlViewModel ScoreView
        {
            get { return _scoreView; }
            set
            {
                _scoreView = value;
                OnPropertyChanged("ScoreView");
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

        public FinaleQuestionInfo Question
        {
            get { return _question; }
            set
            {
                _question = value;
                OnPropertyChanged("Question");
            }
        }

        public FinaleViewModel(IAdminService adminService)
        {
            adminService.AdminServiceCallback.FinaleQuestionReceived += AdminServiceCallbackOnFinaleQuestionReceived;
            adminService.AdminServiceCallback.FinaleTimerChanged += AdminServiceCallbackOnFinaleTimerChanged;
            adminService.AdminServiceCallback.FinaleTurnChanged += AdminServiceCallbackOnFinaleTurnChanged;
            adminService.AdminServiceCallback.FinaleQuestionAnswerCorrect += AdminServiceCallbackOnFinaleQuestionAnswerCorrect;
            adminService.AdminServiceCallback.FinaleQuestionAnswersShowed += AdminServiceCallbackOnFinaleQuestionAnswersShowed;
            ScoreView = new FinalScoreControlViewModel();
        }

        #region Events
        private void AdminServiceCallbackOnFinaleQuestionAnswersShowed(object sender, EventArgs eventArgs)
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

        private void AdminServiceCallbackOnFinaleQuestionAnswerCorrect(object sender, EventArgs<string> eventArgs)
        {
            Dispatch(() =>
            {
                if (Question.Answer1 == eventArgs.Value) IsAnswer1Enabled = true;
                if (Question.Answer2 == eventArgs.Value) IsAnswer2Enabled = true;
                if (Question.Answer3 == eventArgs.Value) IsAnswer3Enabled = true;
                if (Question.Answer4 == eventArgs.Value) IsAnswer4Enabled = true;
                if (Question.Answer5 == eventArgs.Value) IsAnswer5Enabled = true;
            }, DispatcherPriority.DataBind);
        }

        private void AdminServiceCallbackOnFinaleTurnChanged(object sender, EventArgs<FinaleTurnTypeInfo> eventArgs)
        {
            Dispatch(() =>
            {
                ScoreView.Turn = eventArgs.Value;
            }, DispatcherPriority.DataBind);
        }

        private void AdminServiceCallbackOnFinaleTimerChanged(object sender, EventArgs<FinaleTimerChangeInfo> eventArgs)
        {
            Dispatch(() => ScoreView.SetSecondsByTurn(eventArgs.Value.Turn, eventArgs.Value.Seconds), DispatcherPriority.DataBind);
        }

        private void AdminServiceCallbackOnFinaleQuestionReceived(object sender, EventArgs<FinaleQuestionInfo> eventArgs)
        {
            Dispatch(() =>
            {
                Question = eventArgs.Value;
                IsAnswer1Enabled = false;
                IsAnswer2Enabled = false;
                IsAnswer3Enabled = false;
                IsAnswer4Enabled = false;
                IsAnswer5Enabled = false;
            }, DispatcherPriority.DataBind);
        }
        #endregion Events
    }
}

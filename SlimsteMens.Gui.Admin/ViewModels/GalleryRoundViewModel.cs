using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using SlimsteMens.Gui.Controls;
using SlimsteMens.Gui.Controls.ViewModels;
using SlimsteMens.Services;
using SlimsteMens.Services.Info;
using SlimsteMens.Services.Interfaces;

namespace SlimsteMens.Gui.Admin.ViewModels
{
    public class GalleryRoundViewModel : ViewModelBase
    {
        private readonly IAdminService _adminService;
        private ScoreControlViewModel _scoreView;
        private BitmapImage _currentImage;
        private int _correct;
        private int _imageIndex;
        private int _answerIndex;
        private IList<GalleryQuestionInfo> _galleryQuestions;
        public ObservableCollection<GalleryQuestionInfo> GalleryQuestionsToGuess
        {
            get { return _galleryQuestionsToGuess; }
            private set
            {
                _galleryQuestionsToGuess = value;
                OnPropertyChanged("GalleryQuestionsToGuess");
            }
        }

        private string _galleryName;
        private bool _isFirstPlayerStarted;
        private bool _canOthersGuess;
        private bool _nextTurnAvailable;
        private bool _stopTimerAvaibable;
        private bool _nextGalleryAvailable;
        private ObservableCollection<GalleryQuestionInfo> _galleryQuestionsToGuess;
        private bool _showAnswersAvailable;
        private bool _isNextAnswerAvailable;
        private bool _nextRoundAvailable;
        private string _answer;

        public string GalleryName
        {
            get { return _galleryName; }
            private set
            {
                _galleryName = value;
                OnPropertyChanged("GalleryName");
            }
        }

        public ScoreControlViewModel ScoreView
        {
            get { return _scoreView; }
            private set
            {
                _scoreView = value;
                OnPropertyChanged("ScoreView");
            }
        }

        public BitmapImage CurrentImage
        {
            get
            {
                return _currentImage;
            }
            private set
            {
                _currentImage = value;
                OnPropertyChanged("CurrentImage");
            }
        }

        public ICommand CorrectCommand { get; private set; }
        public ICommand IncorrectCommand { get; private set; }
        public ICommand StartGalleryCommand { get; private set; }
        public ICommand NextTurnCommand { get; private set; }
        public ICommand StopTimerCommand { get; private set; }
        public ICommand ShowAnswersCommand { get; private set; }
        public ICommand NextAnswerCommand { get; private set; }
        public ICommand GuessCorrectCommand { get; set; }
        public ICommand NextRoundCommand { get; private set; }

        public bool IsFirstPlayerStarted
        {
            get { return _isFirstPlayerStarted; }
            private set
            {
                _isFirstPlayerStarted = value;
                OnPropertyChanged("IsFirstPlayerStarted");
            }
        }

        public bool NextGalleryAvailable
        {
            get { return _nextGalleryAvailable; }
            private set
            {
                _nextGalleryAvailable = value;
                OnPropertyChanged("NextGalleryAvailable");
            }
        }

        public bool CanOthersGuess
        {
            get { return _canOthersGuess; }
            private set
            {
                _canOthersGuess = value;
                OnPropertyChanged("CanOthersGuess");
            }
        }

        public bool NextTurnAvailable
        {
            get { return _nextTurnAvailable; }
            private set
            {
                _nextTurnAvailable = value;
                OnPropertyChanged("NextTurnAvailable");
            }
        }

        public bool StopTimerAvaibable
        {
            get { return _stopTimerAvaibable; }
            private set
            {
                _stopTimerAvaibable = value;
                OnPropertyChanged("StopTimerAvaibable");
            }
        }

        public bool ShowAnswersAvailable
        {
            get { return _showAnswersAvailable; }
            set
            {
                _showAnswersAvailable = value;
                OnPropertyChanged("ShowAnswersAvailable");
            }
        }

        public bool IsNextAnswerAvailable
        {
            get { return _isNextAnswerAvailable; }
            private set
            {
                _isNextAnswerAvailable = value;
                OnPropertyChanged("IsNextAnswerAvailable");
            }
        }

        public bool NextRoundAvailable
        {
            get { return _nextRoundAvailable; }
            private set
            {
                _nextRoundAvailable = value;
                OnPropertyChanged("NextRoundAvailable");
            }
        }

        public string Answer
        {
            get { return _answer; }
            set
            {
                _answer = value;
                OnPropertyChanged("Answer");
            }
        }

        public GalleryRoundViewModel(IAdminService adminService, ScoreControlViewModel scoreControlView) 
        {
            _adminService = adminService;
            _adminService.AdminServiceCallback.QuestionGalleryReceived += AdminServiceCallbackOnQuestionGalleryReceived;
            _adminService.AdminServiceCallback.GalleryCompleted += AdminServiceCallbackOnGalleryCompleted;
            _adminService.AdminServiceCallback.RoundGalleryEnded += AdminServiceCallbackOnRoundGalleryEnded;

            CorrectCommand = new RelayCommand(Correct) { IsEnabled = true };
            IncorrectCommand = new RelayCommand(Incorrect) { IsEnabled = true };
            StartGalleryCommand = new RelayCommand(StartGallery) { IsEnabled = true };
            NextTurnCommand = new RelayCommand(NextTurn) { IsEnabled = true };
            ShowAnswersCommand = new RelayCommand(ShowAnswers) { IsEnabled = true };
            NextAnswerCommand = new RelayCommand(NextAnswer) { IsEnabled = true };
            GuessCorrectCommand = new RelayCommand(CorrectGuess) { IsEnabled = true };
            StopTimerCommand = new RelayCommand(StopTimer) { IsEnabled = true };
            NextRoundCommand = new RelayCommand(NextRound) { IsEnabled = true };
            NextGalleryAvailable = true;
            IsFirstPlayerStarted = false;
            ScoreView = scoreControlView;
        }

        private void AdminServiceCallbackOnRoundGalleryEnded(object sender, EventArgs eventArgs)
        {
            Dispatch(() =>
            {
                _adminService.AdminServiceCallback.QuestionGalleryReceived -= AdminServiceCallbackOnQuestionGalleryReceived;
                _adminService.AdminServiceCallback.GalleryCompleted -= AdminServiceCallbackOnGalleryCompleted;
                _adminService.AdminServiceCallback.RoundGalleryEnded -= AdminServiceCallbackOnRoundGalleryEnded;
                IsNextAnswerAvailable = false;
                ShowAnswersAvailable = false;
                NextGalleryAvailable = false;
                CanOthersGuess = false;
                IsFirstPlayerStarted = false;
                NextRoundAvailable = true;
            }, DispatcherPriority.DataBind);
        }

        private void AdminServiceCallbackOnQuestionGalleryReceived(object sender, EventArgs<GalleryInfo> eventArgs)
        {
            Dispatch(() =>
            {
                _correct = 0;
                _imageIndex = -1;
                GalleryName = eventArgs.Value.Name;
                _galleryQuestions = eventArgs.Value.Questions;
                CanOthersGuess = false;
                ShowAnswersAvailable = false;
                NextGalleryAvailable = false;
                GalleryQuestionsToGuess = new ObservableCollection<GalleryQuestionInfo>();
                SetNextImage();
            }, DispatcherPriority.DataBind);
        }

        private void AdminServiceCallbackOnGalleryCompleted(object sender, EventArgs eventArgs)
        {
            Dispatch(() => {
                               CanOthersGuess = false;
                               IsFirstPlayerStarted = false;
                               CurrentImage = null;
                               NextTurnAvailable = false;
                               StopTimerAvaibable = false;
                               NextGalleryAvailable = true;
                               ShowAnswersAvailable = true;
            }, DispatcherPriority.DataBind);
        }


        public void StartGallery()
        {
            IsFirstPlayerStarted = true;
            _adminService.NextGallery();
            NextGalleryAvailable = false;
            IsNextAnswerAvailable = false;
        }

        public void StopTimer()
        {
            _adminService.StopTimer();
            NextTurnAvailable = true;
            StopTimerAvaibable = false;
        }

        public void Correct()
        {
            _correct++;
            _adminService.GalleryQuestionCorrect();
            SetNextImage();
        }

        public void Incorrect()
        {
            GalleryQuestionsToGuess.Add(_galleryQuestions[_imageIndex]);
            _adminService.GalleryQuestionIncorrect();
            SetNextImage();
        }

        public void NextTurn()
        {
            _adminService.NextTurnForGallery();
            StopTimerAvaibable = true;
            NextTurnAvailable = false;
        }

        public void ShowAnswers()
        {
            _answerIndex = -1;
            _adminService.ShowNextGalleryQuestionAnswer();
            ShowAnswersAvailable = false;
            IsNextAnswerAvailable = true;
            SetNextAnswerImage();
        }

        public void NextAnswer()
        {
            _adminService.ShowNextGalleryQuestionAnswer();
            SetNextAnswerImage();
        }

        public void CorrectGuess()
        {
            _correct++;
            _adminService.GalleryQuestionCorrect();
        }

        public void NextRound()
        {
            _adminService.NextRound();
        }

        private void SetNextImage()
        {
            _imageIndex++;
            if (_imageIndex >= _galleryQuestions.Count)
            {
                CurrentImage = null;
                _adminService.StopTimer();
                if (_correct < 10)
                {
                    CanOthersGuess = true;
                    NextTurnAvailable = true;
                }
                else
                {
                    CanOthersGuess = false;
                }
                IsFirstPlayerStarted = false;
                Answer = "";
                return;
            }
            BitmapImage img = new BitmapImage();
            MemoryStream ms = new MemoryStream(_galleryQuestions[_imageIndex].Photo) { Position = 0 };
            img.BeginInit();
            img.CacheOption = BitmapCacheOption.OnLoad;
            img.StreamSource = ms;
            img.EndInit();
            CurrentImage = img;
            Answer = _galleryQuestions[_imageIndex].Answer;
        }

        private void SetNextAnswerImage()
        {
            _answerIndex++;
            if(_answerIndex >= _galleryQuestions.Count)
            {
                CurrentImage = null;
                IsNextAnswerAvailable = false;
                Answer = "";
                return;
            }
            BitmapImage img = new BitmapImage();
            var array = _galleryQuestions[_answerIndex].PhotoAnswer != null &&
                        _galleryQuestions[_answerIndex].PhotoAnswer.Length > 0
                            ? _galleryQuestions[_answerIndex].PhotoAnswer
                            : _galleryQuestions[_answerIndex].Photo;
            MemoryStream ms = new MemoryStream(array) { Position = 0 };
            img.BeginInit();
            img.CacheOption = BitmapCacheOption.OnLoad;
            img.StreamSource = ms;
            img.EndInit();
            CurrentImage = img;
            Answer = _galleryQuestions[_answerIndex].Answer;
        }
    }
}

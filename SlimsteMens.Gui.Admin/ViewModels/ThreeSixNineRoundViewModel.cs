using System;
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
    public class ThreeSixNineRoundViewModel : ViewModelBase
    {
        private readonly IAdminService _adminService;
        private ScoreControlViewModel _scoreView;
        private ThreeSixNineQuestionInfo _question;

        public ScoreControlViewModel ScoreView
        {
            get { return _scoreView; }
            set
            {
                _scoreView = value;
                OnPropertyChanged("ScoreView");
            }
        }

        public ThreeSixNineQuestionInfo Question
        {
            get { return _question; }
            set
            {
                _question = value;
                OnPropertyChanged("Question");
                OnPropertyChanged("Photo");
            }
        }

        public BitmapImage Photo
        {
            get
            {
                if (_question == null || _question.Photo == null || _question.Photo.Length == 0)
                    return null;
                BitmapImage img = new BitmapImage();
                MemoryStream ms = new MemoryStream(_question.Photo) {Position = 0};
                img.BeginInit();
                img.CacheOption = BitmapCacheOption.OnLoad;
                img.StreamSource = ms;
                img.EndInit();
                return img;
            }
        }

        public ICommand StartRoundCommand { get; private set; }
        public bool IsStartRoundCommandEnabled
        {
            get { return ((RelayCommand) StartRoundCommand).IsEnabled; }
            set
            {
                var command = StartRoundCommand as RelayCommand;
                if (command != null) command.IsEnabled = value;
                OnPropertyChanged("IsStartRoundCommandEnabled");
            }
        }

        public ICommand CorrectCommand { get; private set; }
        public ICommand IncorrectCommand { get; private set; }
        public bool IsAnswerCommandsEnabled
        {
            get { return ((RelayCommand)CorrectCommand).IsEnabled; }
            set
            {
                var command = CorrectCommand as RelayCommand;
                if (command != null) command.IsEnabled = value;
                command = IncorrectCommand as RelayCommand;
                if (command != null) command.IsEnabled = value;
                OnPropertyChanged("IsAnswerCommandsEnabled");
            }
        }

        public ICommand NextRoundCommand { get; private set; }
        public bool IsNextRoundCommandEnabled
        {
            get { return ((RelayCommand)NextRoundCommand).IsEnabled; }
            set
            {
                var command = NextRoundCommand as RelayCommand;
                if (command != null) command.IsEnabled = value;
                OnPropertyChanged("IsNextRoundCommandEnabled");
            }
        }

        public ThreeSixNineRoundViewModel(IAdminService adminService)
        {
            _adminService = adminService;
            ScoreView = new ScoreControlViewModel();
            Question = new ThreeSixNineQuestionInfo();
            StartRoundCommand = new RelayCommand(StartRound){IsEnabled = true};
            CorrectCommand = new RelayCommand(Correct);
            IncorrectCommand = new RelayCommand(Incorrect);
            NextRoundCommand = new RelayCommand(NextRound);

            _adminService.AdminServiceCallback.Question369Received += OnQuestionReceived;
            _adminService.AdminServiceCallback.Round369Ended += Round369Ended;
        }
       
        #region Events
        public void OnQuestionReceived(object sender, EventArgs<ThreeSixNineQuestionInfo> eventArgs)
        {
            Dispatch(() =>
            { 
                Question = eventArgs.Value;
                IsAnswerCommandsEnabled = true;
            }, DispatcherPriority.DataBind);
        }

        void Round369Ended(object sender, EventArgs e)
        {
            Dispatch(() =>
            {
                Question = null;
                IsAnswerCommandsEnabled = false;
                IsNextRoundCommandEnabled = true;
            }, DispatcherPriority.DataBind);
        }

        #endregion

        #region Commands
        public void StartRound()
        {
            IsStartRoundCommandEnabled = false;
            _adminService.StartRound369();
        }

        public void Correct()
        {
            _adminService.Question369Correct();
        }

        public void Incorrect()
        {
            _adminService.Question369Incorrect();
        }

        public void NextRound()
        {
            _adminService.AdminServiceCallback.Question369Received -= OnQuestionReceived;
            _adminService.AdminServiceCallback.Round369Ended -= Round369Ended;
            _adminService.NextRound();
        }
        #endregion
    }
}

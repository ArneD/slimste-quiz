using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using SlimsteMens.Gui.Controls;
using SlimsteMens.Services.Info;
using SlimsteMens.Services.Interfaces;

namespace SlimsteMens.Gui.Admin.ViewModels
{
    public class AddVideoQuestionViewModel : ViewModelBase
    {
        private readonly IAdminService _adminService;
        private readonly MainWindowViewModel _mainWindowViewModel;
        private string _question;
        private string _answer1;
        private string _answer2;
        private string _answer3;
        private string _answer4;
        private string _answer5;
        private string _videoPath;

        /// <summary>
        /// Gets the save command.
        /// </summary>
        /// <value>
        /// The save command.
        /// </value>
        public ICommand SaveCommand { get; private set; }

        /// <summary>
        /// Gets the close command.
        /// </summary>
        /// <value>
        /// The close command.
        /// </value>
        public ICommand CloseCommand { get; private set; }

        /// <summary>
        /// Gets the select video path command.
        /// </summary>
        /// <value>
        /// The select video path command.
        /// </value>
        public ICommand SelectVideoPathCommand { get; private set; }

        public string Question
        {
            get { return _question; }
            set
            {
                _question = value;
                OnPropertyChanged("Question");
            }
        }

        public string VideoPath
        {
            get { return _videoPath; }
            set
            {
                _videoPath = value;
                OnPropertyChanged("VideoPath");
            }
        }

        public string Answer1
        {
            get { return _answer1; }
            set
            {
                _answer1 = value;
                OnPropertyChanged("Answer1");
            }
        }

        public string Answer2
        {
            get { return _answer2; }
            set
            {
                _answer2 = value;
                OnPropertyChanged("Answer2");
            }
        }

        public string Answer3
        {
            get { return _answer3; }
            set
            {
                _answer3 = value;
                OnPropertyChanged("Answer3");
            }
        }

        public string Answer4
        {
            get { return _answer4; }
            set
            {
                _answer4 = value;
                OnPropertyChanged("Answer4");
            }
        }

        public string Answer5
        {
            get { return _answer5; }
            set
            {
                _answer5 = value;
                OnPropertyChanged("Answer5");
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddVideoQuestionViewModel" /> class.
        /// </summary>
        public AddVideoQuestionViewModel(IAdminService adminService, MainWindowViewModel mainWindowViewModel)
        {
            _adminService = adminService;
            _mainWindowViewModel = mainWindowViewModel;
            SaveCommand = new RelayCommand(Save){IsEnabled = true};
            CloseCommand = new RelayCommand(Close) { IsEnabled = true };
            SelectVideoPathCommand = new RelayCommand(SelectPath) { IsEnabled = true };
        }

        public void Close()
        {
            _mainWindowViewModel.BackToStart();
        }

        public void Save()
        {
            if(_adminService.AddVideoQuestion(new VideoQuestionInfo
                {
                    Answer1 = Answer1,
                    Answer2 = Answer2,
                    Answer3 = Answer3,
                    Answer4 = Answer4,
                    Answer5 = Answer5,
                    Question = Question,
                    VideoPath = VideoPath,
                }))
            {
                Question = "";
                Answer1 = "";
                Answer2 = "";
                Answer3 = "";
                Answer4 = "";
                Answer5 = "";
                VideoPath = "";
                MessageBox.Show("Video vraag opgeslagen");
            }
            else
            {
                MessageBox.Show("Video niet opgeslagen, probeer later opnieuw");
            }
        }

        public void SelectPath()
        {
            var ofd = new OpenFileDialog { Title = "Selecteer een video", CheckFileExists = true };
            bool? b = ofd.ShowDialog();
            if (b.HasValue && b.Value)
            {
                VideoPath = ofd.FileName;
            }
        }
    }
}

using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using SlimsteMens.Gui.Controls;
using SlimsteMens.Services.Info;
using SlimsteMens.Services.Interfaces;

namespace SlimsteMens.Gui.Admin.ViewModels
{
    public class AddThreeSixNineQuestionViewModel : ViewModelBase
    {
        private readonly IAdminService _adminService;
        private readonly MainWindowViewModel _mainWindowViewModel;
        private string _answer;
        private string _question;
        private BitmapImage _imageQuestion;

        public string Question
        {
            get { return _question; }
            set
            {
                _question = value;
                OnPropertyChanged("Question");
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

        public BitmapImage ImageQuestion
        {
            get { return _imageQuestion; }
            set
            {
                _imageQuestion = value;
                OnPropertyChanged("ImageQuestion");
            }
        }

        public ICommand SaveQuestionCommand { get; private set; }
        public ICommand OpenImageCommand { get; private set; }
        public ICommand BackCommand { get; private set; }

        public AddThreeSixNineQuestionViewModel(IAdminService adminService, MainWindowViewModel mainWindowViewModel)
        {
            _adminService = adminService;
            _mainWindowViewModel = mainWindowViewModel;
            SaveQuestionCommand = new RelayCommand(Save){IsEnabled = true};
            OpenImageCommand = new RelayCommand(OpenImage){IsEnabled = true};
            BackCommand = new RelayCommand(Back){IsEnabled = true};
        }

        public void Save()
        {
            if(string.IsNullOrEmpty(Answer) || string.IsNullOrEmpty(Question))
                return;

            byte[] buffer = null;
            if (ImageQuestion != null)
            {
                buffer = new byte[ImageQuestion.StreamSource.Length];
                ImageQuestion.StreamSource.Seek(0, System.IO.SeekOrigin.Begin);
                //very important, it should be set to the start of the stream
                ImageQuestion.StreamSource.Read(buffer, 0, buffer.Length);
            }

            var result = _adminService.Add369Question(new ThreeSixNineQuestionInfo
                {
                    Answer = Answer,
                    Question = Question,
                    Photo = buffer,
                });

            if (result)
            {
                Answer = "";
                Question = "";
                ImageQuestion = null;
                MessageBox.Show("Vraag bewaard");
            }
            else
            {
                MessageBox.Show("Vraag kon niet bewaard worden");
            }
        }

        public void OpenImage()
        {
            var ofd = new OpenFileDialog {Title = "Selecteer een foto", CheckFileExists = true};
            bool? b = ofd.ShowDialog();
            if (b.HasValue && b.Value)
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = System.IO.File.OpenRead(ofd.FileName);
                image.EndInit();
                ImageQuestion = image;
            }
        }

        public void Back()
        {
            _mainWindowViewModel.BackToStart();
        }
    }
}

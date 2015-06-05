using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using SlimsteMens.Gui.Controls;
using SlimsteMens.Services.Info;
using SlimsteMens.Services.Interfaces;

namespace SlimsteMens.Gui.Admin.ViewModels
{
    public class AddGalleryViewModel : ViewModelBase
    {
        private readonly IAdminService _adminService;
        private readonly MainWindowViewModel _mainWindowViewModel;
        private string _name;
        private BitmapImage _image;
        private BitmapImage _answerImage;
        private string _answer;
        private bool _isReadyToSave;
        public ObservableCollection<GalleryQuestionInfo> Questions { get; private set; }

        public BitmapImage Image
        {
            get { return _image; }
            set
            {
                _image = value;
                OnPropertyChanged("Image");
            }
        }

        public BitmapImage AnswerImage
        {
            get { return _answerImage; }
            set
            {
                _answerImage = value;
                OnPropertyChanged("AnswerImage");
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

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public ICommand SelectImageCommand { get; private set; }
        public ICommand SelectAnswerImageCommand { get; private set; }
        public ICommand BackCommand { get; private set; }
        public ICommand NextCommand { get; private set; }
        public ICommand SaveCommand { get; set; }

        public bool IsReadyToSave
        {
            get { return _isReadyToSave; }
            set
            {
                _isReadyToSave = value;
                OnPropertyChanged("IsReadyToSave");
            }
        }

        public AddGalleryViewModel(IAdminService adminService, MainWindowViewModel mainWindowViewModel)
        {
            _adminService = adminService;
            _mainWindowViewModel = mainWindowViewModel;
            SelectImageCommand = new RelayCommand(SelectImage){IsEnabled = true};
            SelectAnswerImageCommand = new RelayCommand(SelectAnswerImage) {IsEnabled = true};
            BackCommand = new RelayCommand(Back) {IsEnabled = true};
            NextCommand = new RelayCommand(Next) {IsEnabled = true};
            SaveCommand = new RelayCommand(Save) {IsEnabled = true};
            IsReadyToSave = false;
            Questions = new ObservableCollection<GalleryQuestionInfo>();
        }

        public void SelectImage()
        {
            var ofd = new OpenFileDialog { Title = "Selecteer een foto", CheckFileExists = true };
            bool? b = ofd.ShowDialog();
            if (b.HasValue && b.Value)
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = File.OpenRead(ofd.FileName);
                image.EndInit();
                Image = image;
            }
        }

        public void SelectAnswerImage()
        {
            var ofd = new OpenFileDialog { Title = "Selecteer een foto", CheckFileExists = true };
            bool? b = ofd.ShowDialog();
            if (b.HasValue && b.Value)
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = File.OpenRead(ofd.FileName);
                image.EndInit();
                AnswerImage = image;
            }
        }

        public void Back()
        {
            _mainWindowViewModel.BackToStart();
        }

        public void Next()
        {
            var galleryQuestionInfo = new GalleryQuestionInfo { Answer = Answer};
            if (Image != null)
            {
                var buffer = new byte[Image.StreamSource.Length];
                Image.StreamSource.Seek(0, SeekOrigin.Begin);
                //very important, it should be set to the start of the stream
                Image.StreamSource.Read(buffer, 0, buffer.Length);
                galleryQuestionInfo.Photo = buffer;
            }
            else
            {
                return;
            }

            byte[] buffer2;
            if (AnswerImage != null)
            {
                buffer2 = new byte[AnswerImage.StreamSource.Length];
                AnswerImage.StreamSource.Seek(0, SeekOrigin.Begin);
                //very important, it should be set to the start of the stream
                AnswerImage.StreamSource.Read(buffer2, 0, buffer2.Length);
                galleryQuestionInfo.PhotoAnswer = buffer2;
            }

            Questions.Add(galleryQuestionInfo);
            if (Questions.Count == 10)
                IsReadyToSave = true;
            Answer = "";
            Image = null;
            AnswerImage = null;
        }

        public void Save()
        {
            var gallery = new GalleryInfo {Name = Name};
            gallery.Questions.AddRange(new List<GalleryQuestionInfo>(Questions.ToList()));
            if(_adminService.AddGallery(gallery))
            {
                MessageBox.Show("Galerij opgeslagen");
                IsReadyToSave = false;
                Questions.Clear();
                Name = "";
            }
            else
            {
                MessageBox.Show("Galerij niet opgeslagen, probeer later eens opnieuw");
            }
        }
    }
    
}

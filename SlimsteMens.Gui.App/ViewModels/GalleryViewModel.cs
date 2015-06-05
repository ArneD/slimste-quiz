using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using SlimsteMens.Gui.Controls.ViewModels;
using SlimsteMens.Services;
using SlimsteMens.Services.Info;
using SlimsteMens.Services.Interfaces;

namespace SlimsteMens.Gui.App.ViewModels
{
    public class GalleryViewModel : ViewModelBase
    {
        private readonly IAdminService _adminService;
        private ScoreControlViewModel _scoreView;
        private BitmapImage _currentImage;

        public ScoreControlViewModel ScoreView
        {
            get { return _scoreView; }
            set
            {
                _scoreView = value;
                OnPropertyChanged("ScoreView");
            }
        }

        private int _imageIndex;
        private int _answerIndex;
        public IList<GalleryQuestionInfo> GalleryQuestions { get; private set; }

        public BitmapImage CurrentImage
        {
            get { return _currentImage; }
            private set
            {
                _currentImage = value;
                OnPropertyChanged("CurrentImage");
            }
        }

        public GalleryViewModel(IAdminService adminService)
        {
            _adminService = adminService;
            adminService.AdminServiceCallback.QuestionGalleryReceived += AdminServiceCallbackOnQuestionGalleryReceived;
            adminService.AdminServiceCallback.NextGalleryQuestionShowed += AdminServiceCallbackOnNextGalleryQuestionShowed;
            adminService.AdminServiceCallback.NextGalleryAnswerShowed += AdminServiceCallbackOnNextGalleryAnswerShowed;
            adminService.AdminServiceCallback.RoundGalleryEnded += AdminServiceCallbackOnRoundGalleryEnded;

            GalleryQuestions = new List<GalleryQuestionInfo>();
        }

        

        #region Events

        private void AdminServiceCallbackOnRoundGalleryEnded(object sender, EventArgs eventArgs)
        {
            Dispatch(() =>
            {
                _adminService.AdminServiceCallback.QuestionGalleryReceived -= AdminServiceCallbackOnQuestionGalleryReceived;
                _adminService.AdminServiceCallback.NextGalleryAnswerShowed -= AdminServiceCallbackOnNextGalleryAnswerShowed;
                _adminService.AdminServiceCallback.NextGalleryQuestionShowed -= AdminServiceCallbackOnNextGalleryQuestionShowed;
                _adminService.AdminServiceCallback.RoundGalleryEnded -= AdminServiceCallbackOnRoundGalleryEnded;
            }, DispatcherPriority.DataBind);
        }

        private void AdminServiceCallbackOnNextGalleryQuestionShowed(object sender, EventArgs eventArgs)
        {
            Dispatch(SetNextImage, DispatcherPriority.DataBind);
        }

        private void AdminServiceCallbackOnNextGalleryAnswerShowed(object sender, EventArgs eventArgs)
        {
            Dispatch(SetNextAnswerImage, DispatcherPriority.DataBind);
        }

        private void AdminServiceCallbackOnQuestionGalleryReceived(object sender, EventArgs<GalleryInfo> eventArgs)
        {
            Dispatch(() =>
            {
                _imageIndex = -1;
                _answerIndex = -1;
                GalleryQuestions.Clear();
                foreach (var galleryQuestionInfo in eventArgs.Value.Questions)
                {
                    GalleryQuestions.Add(galleryQuestionInfo);
                }
                SetNextImage();
            }, DispatcherPriority.DataBind);
        }

        #endregion

        private void SetNextImage()
        {
            _imageIndex++;
            if (_imageIndex >= GalleryQuestions.Count)
            {
                CurrentImage = null;
                return;
            }
            BitmapImage img = new BitmapImage();
            MemoryStream ms = new MemoryStream(GalleryQuestions[_imageIndex].Photo) { Position = 0 };
            img.BeginInit();
            img.CacheOption = BitmapCacheOption.OnLoad;
            img.StreamSource = ms;
            img.EndInit();
            CurrentImage = img;
        }

        private void SetNextAnswerImage()
        {
            _answerIndex++;
            if (_answerIndex >= GalleryQuestions.Count)
            {
                CurrentImage = null;
                return;
            }
            BitmapImage img = new BitmapImage();
            var array = GalleryQuestions[_answerIndex].PhotoAnswer != null &&
                        GalleryQuestions[_answerIndex].PhotoAnswer.Length > 0
                            ? GalleryQuestions[_answerIndex].PhotoAnswer
                            : GalleryQuestions[_answerIndex].Photo;
            MemoryStream ms = new MemoryStream(array) { Position = 0 };
            img.BeginInit();
            img.CacheOption = BitmapCacheOption.OnLoad;
            img.StreamSource = ms;
            img.EndInit();
            CurrentImage = img;
        }
    }
}

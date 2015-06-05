using System;
using System.Windows;
using System.Windows.Controls;
using SlimsteMens.Gui.App.ViewModels;

namespace SlimsteMens.Gui.App.Views
{
    /// <summary>
    /// Interaction logic for VideoView.xaml
    /// </summary>
    public partial class VideoView : UserControl
    {
        public VideoView()
        {
            InitializeComponent();
            this.Loaded += VideoView_Loaded;
        }

        void VideoView_Loaded(object sender, RoutedEventArgs e)
        {
            var videoViewModel = DataContext as VideoViewModel;
            if (videoViewModel != null)
            {
                videoViewModel.OnPlayVideo += VideoViewModelOnPlayVideo;
                videoViewModel.OnPlayVideoCompleted += VideoCompletedViewModelOnPlayVideoCompleted;
            }
        }

        private void VideoViewModelOnPlayVideo(object sender, EventArgs eventArgs)
        {
            videoElement.Play();
        }

        private void VideoCompletedViewModelOnPlayVideoCompleted(object sender, EventArgs eventArgs)
        {
            videoElement.Stop();
        }

        private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            videoElement.Stop();
            var videoViewModel = DataContext as VideoViewModel;
            if (videoViewModel != null)
                videoViewModel.VideoEnded();
        }

        private void VideoView_OnLoaded(object sender, RoutedEventArgs e)
        {
            AudioElement.Play();
        }
    }
}

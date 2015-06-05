using System;
using System.IO;
using System.Timers;
using System.Windows.Media.Imaging;

namespace SlimsteMens.Gui.App.ViewModels
{
    public class ImageViewModel : ViewModelBase
    {
        private BitmapImage _image;
        public BitmapImage Image
        {
            get { return _image; }
            set
            {
                _image = value;
                OnPropertyChanged("Image");
            }
        }

        private readonly Timer _timer;

        public event EventHandler OnImageShown;

        public ImageViewModel()
        {
            _timer = new Timer {Interval = 5000};
            _timer.Elapsed += TimerElapsed;
        }

        void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            _timer.Stop();
            if (OnImageShown != null)
                OnImageShown(sender, e);
        }

        public void SetImage(byte[] imageInBytes)
        {
            if(imageInBytes.Length == 0)
                return;
            
            var img = new BitmapImage();
            var ms = new MemoryStream(imageInBytes) { Position = 0 };
            img.BeginInit();
            img.CacheOption = BitmapCacheOption.OnLoad;
            img.StreamSource = ms;
            img.EndInit();
            Image = img;
            _timer.Start();
        }
    }
}

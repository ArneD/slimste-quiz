using System;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using SlimsteMens.Gui.Controls.ViewModels;
using SlimsteMens.Services;
using SlimsteMens.Services.Info;
using SlimsteMens.Services.Interfaces;

namespace SlimsteMens.Gui.App.ViewModels
{
    public class ThreeSixNineViewModel : ViewModelBase
    {
        private readonly IAdminService _adminService;
        private ScoreControlViewModel _scoreView;
        private int _indexSelection = -1;
        private bool _introPlayed;

        /// <summary>
        /// Gets or sets the questions.
        /// </summary>
        /// <value>
        /// The questions.
        /// </value>
        public ObservableCollection<ThreeSixNineQuestionInfo> Questions { get; private set; }

        public ObservableCollection<bool> IsSelected { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether [intro played].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [intro played]; otherwise, <c>false</c>.
        /// </value>
        public bool IntroPlayed
        {
            get { return _introPlayed; }
            set
            {
                _introPlayed = value;
                OnPropertyChanged("IntroPlayed");
            }
        }

        /// <summary>
        /// Gets or sets the score view.
        /// </summary>
        /// <value>
        /// The score view.
        /// </value>
        public ScoreControlViewModel ScoreView
        {
            get { return _scoreView; }
            set
            {
                _scoreView = value;
                OnPropertyChanged("ScoreView");
            }
        }

        public event EventHandler<byte[]> OnSetImage;

        /// <summary>
        /// Initializes a new instance of the <see cref="ThreeSixNineViewModel" /> class.
        /// </summary>
        public ThreeSixNineViewModel(IAdminService adminService)
        {
            _adminService = adminService;
            Questions = new ObservableCollection<ThreeSixNineQuestionInfo>();
            IsSelected = new ObservableCollection<bool>();
            for (int i = 0; i < 15; i++)
                IsSelected.Add(false);

            adminService.AdminServiceCallback.Question369Received += AdminServiceCallbackOnQuestion369Received;
            adminService.AdminServiceCallback.Round369Ended += AdminServiceCallbackOnRound369Ended;
        }

        #region Events

        private void AdminServiceCallbackOnQuestion369Received(object sender, EventArgs<ThreeSixNineQuestionInfo> eventArgs)
        {
            Dispatch(() =>
            {
                if(_indexSelection >= 0)
                {
                    IsSelected[_indexSelection] = false;
                }
                IsSelected[++_indexSelection] = true;
                if(eventArgs.Value.Photo != null && eventArgs.Value.Photo.Length != 0)
                {
                    if (OnSetImage != null)
                        OnSetImage(sender, eventArgs.Value.Photo);
                }
            }, DispatcherPriority.DataBind);
        }

        private void AdminServiceCallbackOnRound369Ended(object sender, EventArgs eventArgs)
        {
            Dispatch(() =>
            {
                _adminService.AdminServiceCallback.Question369Received -= AdminServiceCallbackOnQuestion369Received;
                _adminService.AdminServiceCallback.Round369Ended -= AdminServiceCallbackOnRound369Ended;
            }, DispatcherPriority.DataBind);
        }

        #endregion
    }
}

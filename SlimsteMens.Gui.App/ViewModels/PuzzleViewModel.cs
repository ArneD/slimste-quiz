using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Threading;
using SlimsteMens.Gui.Controls.ViewModels;
using SlimsteMens.Model;
using SlimsteMens.Services;
using SlimsteMens.Services.Info;
using SlimsteMens.Services.Interfaces;

namespace SlimsteMens.Gui.App.ViewModels
{
    public class PuzzleViewModel : ViewModelBase
    {
        private readonly IAdminService _adminService;
        private ScoreControlViewModel _scoreView;
        private bool _isAnswer1Enabled;
        private bool _isAnswer2Enabled;
        private bool _isAnswer3Enabled;
        private PuzzleQuestionInfo _puzzle1;
        private PuzzleQuestionInfo _puzzle2;
        private PuzzleQuestionInfo _puzzle3;
        private ObservableCollection<TextBlock> _hints;


        public SolidColorBrush Puzzle1Brush
        {
            get {
                return IsAnswer1Enabled ? new SolidColorBrush(Color.FromRgb(0, 255, 0)) : new SolidColorBrush(Color.FromRgb(255, 255,255));
            }
        }

        public SolidColorBrush Puzzle2Brush
        {
            get {
                return IsAnswer2Enabled ? new SolidColorBrush(Color.FromRgb(255, 255, 0)) : new SolidColorBrush(Color.FromRgb(255, 255, 255));
            }
        }

        public SolidColorBrush Puzzle3Brush
        {
            get {
                return IsAnswer3Enabled ? new SolidColorBrush(Color.FromRgb(0, 0, 255)) : new SolidColorBrush(Color.FromRgb(255, 255, 255));
            }
        }

        public ObservableCollection<TextBlock> Hints
        {
            get { return _hints; }
            private set
            {
                _hints = value;
                OnPropertyChanged("Hints");
            }
        }

        public PuzzleQuestionInfo Puzzle1
        {
            get { return _puzzle1; }
            set
            {
                _puzzle1 = value;
                OnPropertyChanged("Puzzle1");
            }
        }

        public PuzzleQuestionInfo Puzzle2
        {
            get { return _puzzle2; }
            set
            {
                _puzzle2 = value;
                OnPropertyChanged("Puzzle2");
            }
        }

        public PuzzleQuestionInfo Puzzle3
        {
            get { return _puzzle3; }
            set
            {
                _puzzle3 = value;
                OnPropertyChanged("Puzzle3");
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

        #region Answers

        public bool IsAnswer1Enabled
        {
            get { return _isAnswer1Enabled; }
            set
            {
                _isAnswer1Enabled = value;
                OnPropertyChanged("IsAnswer1Enabled");
                OnPropertyChanged("Puzzle1Brush");
            }
        }

        public bool IsAnswer2Enabled
        {
            get { return _isAnswer2Enabled; }
            set
            {
                _isAnswer2Enabled = value;
                OnPropertyChanged("IsAnswer2Enabled");
                OnPropertyChanged("Puzzle2Brush");
            }
        }

        public bool IsAnswer3Enabled
        {
            get { return _isAnswer3Enabled; }
            set
            {
                _isAnswer3Enabled = value;
                OnPropertyChanged("IsAnswer3Enabled");
                OnPropertyChanged("Puzzle3Brush");
            }
        }

        #endregion

        public PuzzleViewModel(IAdminService adminService)
        {
            _adminService = adminService;
            _adminService.AdminServiceCallback.PuzzleCorrect += AdminServiceCallbackOnPuzzleCorrect;
            _adminService.AdminServiceCallback.PuzzleReceived += AdminServiceCallbackOnPuzzleReceived;
            _adminService.AdminServiceCallback.PuzzleAnswersShowed += AdminServiceCallbackOnPuzzleAnswersShowed;
            _adminService.AdminServiceCallback.PuzzleCompleted += AdminServiceCallbackOnPuzzleCompleted;
            _adminService.AdminServiceCallback.RoundPuzzleEnded += AdminServiceCallbackOnRoundPuzzleEnded;
            Hints = new ObservableCollection<TextBlock>();
        }

        #region Events
        private void AdminServiceCallbackOnPuzzleCompleted(object sender, EventArgs eventArgs)
        {
            //TODO
        }

        private void AdminServiceCallbackOnPuzzleAnswersShowed(object sender, EventArgs eventArgs)
        {
            Dispatch(() =>
            {
                IsAnswer1Enabled = true;
                IsAnswer2Enabled = true;
                IsAnswer3Enabled = true;
            }, DispatcherPriority.DataBind);
        }

        private void AdminServiceCallbackOnPuzzleCorrect(object sender, EventArgs<long> eventArgs)
        {
            Dispatch(() => {
                if(Puzzle1.Id == eventArgs.Value)
                    IsAnswer1Enabled = true;
                if(Puzzle2.Id == eventArgs.Value)
                    IsAnswer2Enabled = true;
                if(Puzzle3.Id == eventArgs.Value)
                    IsAnswer3Enabled = true;
            }, DispatcherPriority.DataBind);
        }

        private void AdminServiceCallbackOnPuzzleReceived(object sender, EventArgs<IList<PuzzleQuestionInfo>> eventArgs)
        {
            Dispatch(() =>
            {
                IsAnswer1Enabled = false;
                IsAnswer2Enabled = false;
                IsAnswer3Enabled = false;
                Puzzle1 = eventArgs.Value[0];
                Puzzle2 = eventArgs.Value[1];
                Puzzle3 = eventArgs.Value[2];

                Hints.Clear();
                var hints = new ObservableCollection<TextBlock>
                {
                    BuildHintTextBlock("Puzzle1.Hint1", "Puzzle1Brush"),
                    BuildHintTextBlock("Puzzle2.Hint1", "Puzzle2Brush"),
                    BuildHintTextBlock("Puzzle3.Hint1", "Puzzle3Brush"),
                    BuildHintTextBlock("Puzzle1.Hint2", "Puzzle1Brush"),
                    BuildHintTextBlock("Puzzle2.Hint2", "Puzzle2Brush"),
                    BuildHintTextBlock("Puzzle3.Hint2", "Puzzle3Brush"),
                    BuildHintTextBlock("Puzzle1.Hint3", "Puzzle1Brush"),
                    BuildHintTextBlock("Puzzle2.Hint3", "Puzzle2Brush"),
                    BuildHintTextBlock("Puzzle3.Hint3", "Puzzle3Brush"),
                    BuildHintTextBlock("Puzzle1.Hint4", "Puzzle1Brush"),
                    BuildHintTextBlock("Puzzle2.Hint4", "Puzzle2Brush"),
                    BuildHintTextBlock("Puzzle3.Hint4", "Puzzle3Brush")
                };
                Hints = new ObservableCollection<TextBlock>(hints.Shuffle());

            }, DispatcherPriority.DataBind);
        }

        private void AdminServiceCallbackOnRoundPuzzleEnded(object sender, EventArgs eventArgs)
        {
            Dispatch(() =>
            {
                _adminService.AdminServiceCallback.PuzzleCorrect -= AdminServiceCallbackOnPuzzleCorrect;
                _adminService.AdminServiceCallback.PuzzleReceived -= AdminServiceCallbackOnPuzzleReceived;
                _adminService.AdminServiceCallback.PuzzleAnswersShowed -= AdminServiceCallbackOnPuzzleAnswersShowed;
                _adminService.AdminServiceCallback.PuzzleCompleted -= AdminServiceCallbackOnPuzzleCompleted;
                _adminService.AdminServiceCallback.RoundPuzzleEnded -= AdminServiceCallbackOnRoundPuzzleEnded;
            }, DispatcherPriority.DataBind);
        }
        #endregion

        private static TextBlock BuildHintTextBlock(string hintProperty, string brushProperty)
        {
            var t = new TextBlock
                {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    TextWrapping = TextWrapping.Wrap,
                    FontSize = 18,
                };
            t.SetBinding(TextBlock.TextProperty, new Binding(hintProperty));
            t.SetBinding(TextBlock.ForegroundProperty, new Binding(brushProperty));
            return t;
        }
    }
}

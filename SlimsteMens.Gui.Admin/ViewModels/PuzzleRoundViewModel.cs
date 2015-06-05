using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Threading;
using SlimsteMens.Gui.Controls;
using SlimsteMens.Gui.Controls.ViewModels;
using SlimsteMens.Services;
using SlimsteMens.Services.Info;
using SlimsteMens.Services.Interfaces;

namespace SlimsteMens.Gui.Admin.ViewModels
{
    public class PuzzleRoundViewModel : ViewModelBase
    {
        private readonly IAdminService _adminService;
        private int _turn;
        private int _puzzles;
        private ScoreControlViewModel _scoreView;
        private bool _isCheckBox1Checked;
        private bool _isCheckBox2Checked;
        private bool _isCheckBox3Checked;
        private bool _isCheckBoxesEnabled;

        public ScoreControlViewModel ScoreView
        {
            get { return _scoreView; }
            set
            {
                _scoreView = value; 
                OnPropertyChanged("ScoreView");
            }
        }

        public ICommand StartRoundCommand { get; private set; }
        public bool IsStartRoundCommandEnabled
        {
            get { return (StartRoundCommand as RelayCommand).IsEnabled; }      
            set
            {
                var command = (StartRoundCommand as RelayCommand);
                if(command != null)
                    command.IsEnabled = value;
                OnPropertyChanged("IsStartRoundCommandEnabled");
            }
        }

        public bool IsCheckBoxesEnabled
        {
            get { return _isCheckBoxesEnabled; }
            set
            {
                _isCheckBoxesEnabled = value;
                OnPropertyChanged("IsCheckBoxesEnabled");
            }
        }

        public ObservableCollection<PuzzleQuestionInfo> Puzzles { get; private set; }
        public bool IsCheckBox1Checked
        {
            get { return _isCheckBox1Checked; }
            set
            {
                _isCheckBox1Checked = value;
                OnPropertyChanged("IsCheckBox1Checked");
            }
        }

        public bool IsCheckBox2Checked
        {
            get { return _isCheckBox2Checked; }
            set
            {
                _isCheckBox2Checked = value;
                OnPropertyChanged("IsCheckBox2Checked");
            }
        }

        public bool IsCheckBox3Checked
        {
            get { return _isCheckBox3Checked; }
            set
            {
                _isCheckBox3Checked = value;
                OnPropertyChanged("IsCheckBox3Checked");
            }
        }

        public ICommand PuzzleCorrectCommand { get; private set; }

        public ICommand StopTimerCommand { get; private set; }
        public bool IsStopTimerCommandEnabled
        {
            get { return (StopTimerCommand as RelayCommand).IsEnabled; }
            set
            {
                var command = (StopTimerCommand as RelayCommand);
                if (command != null)
                    command.IsEnabled = value;
                OnPropertyChanged("IsStopTimerCommandEnabled");
            }
        }

        //public ICommand StartTimerCommand { get; private set; }
        //public bool IsStartTimerCommandEnabled
        //{
        //    get { return (StartTimerCommand as RelayCommand).IsEnabled; }
        //    set
        //    {
        //        var command = (StartTimerCommand as RelayCommand);
        //        if (command != null)
        //            command.IsEnabled = value;
        //        OnPropertyChanged("IsStartTimerCommandEnabled");
        //    }
        //}

        public ICommand NextTurnCommand { get; private set; }
        public bool IsNextTurnCommandEnabled
        {
            get { return (NextTurnCommand as RelayCommand).IsEnabled; }
            set
            {
                var command = (NextTurnCommand as RelayCommand);
                if (command != null)
                    command.IsEnabled = value;
                OnPropertyChanged("IsNextTurnCommandEnabled");
            }
        }

        public ICommand NextPuzzleCommand { get; private set; }
        public bool IsNextPuzzleCommandEnabled
        {
            get { return (NextPuzzleCommand as RelayCommand).IsEnabled; }
            set
            {
                var command = (NextPuzzleCommand as RelayCommand);
                if (command != null)
                    command.IsEnabled = value;
                OnPropertyChanged("IsNextPuzzleCommandEnabled");
            }
        }

        public ICommand NextRoundCommand { get; private set; }
        public bool IsNextRoundCommandEnabled
        {
            get { return (NextRoundCommand as RelayCommand).IsEnabled; }
            set
            {
                var command = (NextRoundCommand as RelayCommand);
                if (command != null)
                    command.IsEnabled = value;
                OnPropertyChanged("IsNextRoundCommandEnabled");
            }
        }

        public ICommand ShowAnswersCommand { get; private set; }
        public bool IsShowAnswersCommandEnabled
        {
            get { return (ShowAnswersCommand as RelayCommand).IsEnabled; }
            set
            {
                var command = (ShowAnswersCommand as RelayCommand);
                if (command != null)
                    command.IsEnabled = value;
                OnPropertyChanged("IsShowAnswersCommandEnabled");
            }
        }

        public PuzzleRoundViewModel(IAdminService adminService, ScoreControlViewModel scoreView)
        {
            _adminService = adminService;
            ScoreView = scoreView;
            StartRoundCommand = new RelayCommand(StartRound) { IsEnabled = true };
            _adminService.AdminServiceCallback.PuzzleReceived += AdminServiceCallbackOnPuzzleReceived;
            _adminService.AdminServiceCallback.RoundPuzzleEnded += AdminServiceCallbackOnRoundPuzzleEnded;
            _adminService.AdminServiceCallback.PuzzleCompleted += AdminServiceCallbackOnPuzzleCompleted;
            Puzzles = new ObservableCollection<PuzzleQuestionInfo>();
            PuzzleCorrectCommand = new RelayCommand<long>(PuzzleCorrect){IsEnabled = true};
            StopTimerCommand = new RelayCommand(StopTimer);
            //StartTimerCommand = new RelayCommand(StartTimer);
            NextTurnCommand = new RelayCommand(NextTurn);
            NextPuzzleCommand = new RelayCommand(NextPuzzle);
            ShowAnswersCommand = new RelayCommand(ShowAnswers);
            NextRoundCommand = new RelayCommand(NextRound);
            IsCheckBoxesEnabled = false;
        }
        
        public void StartRound()
        {
            _adminService.StartRoundPuzzle();
            IsStartRoundCommandEnabled = false;
            IsStopTimerCommandEnabled = true;
        }

        public void ShowAnswers()
        {
            _adminService.ShowPuzzleAnswers();
            IsShowAnswersCommandEnabled = false;
        }

        public void PuzzleCorrect(long id)
        {
            _adminService.PuzzleCorrect(id);
        }

        public void StartTimer()
        {
            _adminService.StartTimer();
            //IsStartTimerCommandEnabled = false;
            IsStopTimerCommandEnabled = true;
        }

        public void NextTurn()
        {
            _turn++;
            _adminService.NextTurnForPuzzle();
            //IsStartTimerCommandEnabled = true;
            IsStopTimerCommandEnabled = true;
            IsNextTurnCommandEnabled = false;
            IsShowAnswersCommandEnabled = false;
        }

        public void StopTimer()
        {
            _adminService.StopTimer();
            if (_turn < 3)
            {
                IsNextTurnCommandEnabled = true;
            }
            else
            {
                if (_puzzles < 3)
                {
                    IsNextPuzzleCommandEnabled = true;
                }
                else
                {
                    IsNextRoundCommandEnabled = true;
                }
                IsShowAnswersCommandEnabled = true;
            }
            IsStopTimerCommandEnabled = false;
        }

        public void NextPuzzle()
        {
            _adminService.NextPuzzle();
            IsNextPuzzleCommandEnabled = false;
            IsShowAnswersCommandEnabled = false;
            //IsStartTimerCommandEnabled = true;
            IsStopTimerCommandEnabled = true;
            IsCheckBox1Checked = false;
            IsCheckBox2Checked = false;
            IsCheckBox3Checked = false;
        }

        public void NextRound()
        {
            _adminService.NextRound();
        }

        #region Events

        private void AdminServiceCallbackOnPuzzleReceived(object sender, EventArgs<IList<PuzzleQuestionInfo>> e)
        {
            Dispatch(() =>
            {
                Puzzles.Clear();
                e.Value.ToList().ForEach(p => Puzzles.Add(p));
                IsCheckBox1Checked = false;
                IsCheckBox2Checked = false;
                IsCheckBox3Checked = false;
                IsCheckBoxesEnabled = true;
                //IsStartTimerCommandEnabled = true;
                _turn = 1;
                _puzzles++;
            }, DispatcherPriority.DataBind);
        }

        private void AdminServiceCallbackOnRoundPuzzleEnded(object sender, EventArgs eventArgs)
        {
            Dispatch(() =>
            {
                _adminService.AdminServiceCallback.PuzzleReceived -=
                    AdminServiceCallbackOnPuzzleReceived;
                _adminService.AdminServiceCallback.PuzzleCompleted -=
                    AdminServiceCallbackOnPuzzleCompleted;
                _adminService.AdminServiceCallback.RoundPuzzleEnded -=
                    AdminServiceCallbackOnRoundPuzzleEnded;
                IsNextRoundCommandEnabled = true;
            }, DispatcherPriority.DataBind);
        }

        private void AdminServiceCallbackOnPuzzleCompleted(object sender, EventArgs eventArgs)
        {
            Dispatch(() =>
            {
                if (_puzzles < 3)
                {
                    IsNextPuzzleCommandEnabled = true;
                }
                else
                {
                    IsNextRoundCommandEnabled = true;
                }
                //IsStartTimerCommandEnabled = false;
                IsStopTimerCommandEnabled = false;
                    
            }, DispatcherPriority.DataBind);
        }


        #endregion
    }
}

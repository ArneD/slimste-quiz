using System.Windows;
using System.Windows.Input;
using SlimsteMens.Gui.Controls;
using SlimsteMens.Services.Info;
using SlimsteMens.Services.Interfaces;

namespace SlimsteMens.Gui.Admin.ViewModels
{
    public class AddPuzzleQuestionViewModel : ViewModelBase
    {
        private readonly IAdminService _adminService;
        private readonly MainWindowViewModel _mainWindowViewModel;
        private string _answer;
        private string _hint1;
        private string _hint2;
        private string _hint3;
        private string _hint4;

        public string Answer
        {
            get { return _answer; }
            private set
            {
                _answer = value;
                OnPropertyChanged("Answer");
            }
        }

        public string Hint1
        {
            get { return _hint1; }
            private set
            {
                _hint1 = value;
                OnPropertyChanged("Hint1");
            }
        }

        public string Hint2
        {
            get { return _hint2; }
            private set
            {
                _hint2 = value;
                OnPropertyChanged("Hint2");
            }
        }

        public string Hint3
        {
            get { return _hint3; }
            private set
            {
                _hint3 = value;
                OnPropertyChanged("Hint3");
            }
        }

        public string Hint4
        {
            get { return _hint4; }
            private set
            {
                _hint4 = value;
                OnPropertyChanged("Hint4");
            }
        }

        public ICommand BackCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }

        public AddPuzzleQuestionViewModel(IAdminService adminService, MainWindowViewModel mainWindowViewModel)
        {
            _adminService = adminService;
            _mainWindowViewModel = mainWindowViewModel;
            BackCommand = new RelayCommand(Back){IsEnabled = true};
            SaveCommand = new RelayCommand(Save){IsEnabled = true};
        }

        private void Back()
        {
            _mainWindowViewModel.BackToStart();
        }

        private void Save()
        {
            if(string.IsNullOrEmpty(Hint1) || string.IsNullOrEmpty(Hint2) || string.IsNullOrEmpty(Hint3) || string.IsNullOrEmpty(Hint4) || string.IsNullOrEmpty(Answer))
                return;

            var result = _adminService.AddPuzzleQuestion(new PuzzleQuestionInfo
            {
                Answer = Answer,
                Hint1 = Hint1,
                Hint2 = Hint2,
                Hint3 = Hint3,
                Hint4 = Hint4,
            });

            if (result)
            {
                Answer = "";
                Hint4 = "";
                Hint3 = "";
                Hint2 = "";
                Hint1 = "";
                MessageBox.Show("Puzzle bewaard");
            }
            else
            {
                MessageBox.Show("Puzzle kon niet bewaard worden");
            }
        }
    }
}

using System.Windows;
using System.Windows.Input;
using SlimsteMens.Gui.Controls;
using SlimsteMens.Services.Info;
using SlimsteMens.Services.Interfaces;

namespace SlimsteMens.Gui.Admin.ViewModels
{
    public class AddFinaleQuestionViewModel : ViewModelBase
    {
        private readonly IAdminService _adminService;
        private readonly MainWindowViewModel _mainWindowViewModel;

        #region Question
        private string _question;
        private string _answer1;
        private string _answer2;
        private string _answer3;
        private string _answer4;
        private string _answer5;

        public string Question
        {
            get { return _question; }
            set
            {
                _question = value;
                OnPropertyChanged("Question");
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
        #endregion

        public ICommand CloseCommand { get; set; }
        public ICommand SaveCommand { get; set; }

        public AddFinaleQuestionViewModel(IAdminService adminService, MainWindowViewModel mainWindowViewModel)
        {
            _adminService = adminService;
            _mainWindowViewModel = mainWindowViewModel;
            CloseCommand =new RelayCommand(Close){IsEnabled = true};
            SaveCommand = new RelayCommand(Save) {IsEnabled = true};
        }

        public void Save()
        {
            if (_adminService.AddFinaleQuestion(new FinaleQuestionInfo
            {
                Answer1 = Answer1,
                Answer2 = Answer2,
                Answer3 = Answer3,
                Answer4 = Answer4,
                Answer5 = Answer5,
                Question = Question,
            }))
            {
                Question = "";
                Answer1 = "";
                Answer2 = "";
                Answer3 = "";
                Answer4 = "";
                Answer5 = "";
                MessageBox.Show("Finale vraag opgeslagen");
            }
            else
            {
                MessageBox.Show("Finale vraag niet opgeslagen, probeer later opnieuw");
            }
        }

        public void Close()
        {
            _mainWindowViewModel.BackToStart();
        }
    }
}

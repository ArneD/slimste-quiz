using System.Windows.Input;
using System.Windows.Threading;
using SlimsteMens.Gui.Controls;
using SlimsteMens.Services.Info;
using SlimsteMens.Services.Interfaces;

namespace SlimsteMens.Gui.Admin.ViewModels
{
    public class StartViewModel : ViewModelBase
    {
        private readonly IAdminService _adminService;
        private readonly MainWindowViewModel _mainWindowViewModel;
        private readonly AddThreeSixNineQuestionViewModel _addThreeSixNineQuestionViewModel;
        private readonly AddPuzzleQuestionViewModel _addPuzzleQuestionViewModel;
        private readonly AddGalleryViewModel _addGalleryViewModel;
        private readonly AddVideoQuestionViewModel _addVideoQuestionViewModel;
        private readonly AddFinaleQuestionViewModel _addFinaleQuestionViewModel;
        private string _team1;
        private string _team2;
        private string _team3;
        private int _startSeconds;

        public RelayCommand StartGameCommand { get; private set; }
        public ICommand Add369QuestionCommand { get; private set; }
        public ICommand AddPuzzleCommand { get; private set; }
        public ICommand AddGalleryCommand { get; private set; }
        public ICommand AddVideoCommand { get; private set; }
        public ICommand AddFinaleQuestionCommand { get; private set; }

        public string Team1
        {
            get { return _team1; }
            set
            {
                _team1 = value;
                OnPropertyChanged("Team1");
            }
        }

        public string Team2
        {
            get { return _team2; }
            set
            {
                _team2 = value;
                OnPropertyChanged("Team2");
            }
        }

        public string Team3
        {
            get { return _team3; }
            set
            {
                _team3 = value;
                OnPropertyChanged("Team3");
            }
        }

        public int StartSeconds
        {
            get { return _startSeconds; }
            set
            {
                _startSeconds = value;
                OnPropertyChanged("StartSeconds");
            }
        }

        public StartViewModel(IAdminService adminService, MainWindowViewModel mainWindowViewModel)
        {
            _adminService = adminService;
            _mainWindowViewModel = mainWindowViewModel;
            _addThreeSixNineQuestionViewModel = new AddThreeSixNineQuestionViewModel(_adminService, _mainWindowViewModel);
            _addPuzzleQuestionViewModel = new AddPuzzleQuestionViewModel(_adminService, _mainWindowViewModel);
            _addGalleryViewModel = new AddGalleryViewModel(_adminService, _mainWindowViewModel);
            _addVideoQuestionViewModel = new AddVideoQuestionViewModel(_adminService, _mainWindowViewModel);
            _addFinaleQuestionViewModel = new AddFinaleQuestionViewModel(_adminService, _mainWindowViewModel);
            Team1 = "Team1";
            Team2 = "Team2";
            Team3 = "Team3";
            StartSeconds = 60;

            StartGameCommand = new RelayCommand(StartGame){IsEnabled = true};
            Add369QuestionCommand = new RelayCommand(Add369Question){IsEnabled = true};
            AddPuzzleCommand = new RelayCommand(AddPuzzle){IsEnabled = true};
            AddGalleryCommand = new RelayCommand(AddGallery){IsEnabled = true};
            AddVideoCommand = new RelayCommand(AddVideo){IsEnabled = true};
            AddFinaleQuestionCommand = new RelayCommand(AddFinaleQuestion){IsEnabled = true};
        }

        public void StartGame()
        {
            _adminService
                .StartGame(new GameInfo{Team1 = Team1, Team2 = Team2, Team3 = Team3, StartSeconds = StartSeconds});
            StartGameCommand.IsEnabled = false;
        }

        public void Add369Question()
        {
            Dispatch(()
                =>  _mainWindowViewModel.Show(_addThreeSixNineQuestionViewModel), DispatcherPriority.DataBind);
        }

        public void AddPuzzle()
        {
            Dispatch(()
                => _mainWindowViewModel.Show(_addPuzzleQuestionViewModel), DispatcherPriority.DataBind);
        }

        public void AddGallery()
        {
            Dispatch(()
                => _mainWindowViewModel.Show(_addGalleryViewModel), DispatcherPriority.DataBind);
        }

        public void AddVideo()
        {
            Dispatch(()
                => _mainWindowViewModel.Show(_addVideoQuestionViewModel), DispatcherPriority.DataBind);
        }

        public void AddFinaleQuestion()
        {
            Dispatch(()
                => _mainWindowViewModel.Show(_addFinaleQuestionViewModel), DispatcherPriority.DataBind);
        }
    }
}

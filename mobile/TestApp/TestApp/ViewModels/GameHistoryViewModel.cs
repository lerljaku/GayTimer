using System.Collections.ObjectModel;
using System.Windows.Input;
using GayTimer.Entities;
using GayTimer.Services;

namespace GayTimer.ViewModels
{
    public class GameHistoryViewModel : ScreenBase
    {
        private readonly IDataService m_dataService;

        public GameHistoryViewModel(IDataService dataService)
        {
            m_dataService = dataService;

            AddGameCommand = new RelayCommand(AddGame);
        }

        public ICommand AddGameCommand { get; }

        private ObservableCollection<Game> m_games = new ObservableCollection<Game>();
        public ObservableCollection<Game> Games
        {
            get => m_games;
            private set
            {
                m_games = value;
                NotifyPropertyChanged();
            }
        }

        public override async void Activated()
        {
            var games = await m_dataService.SelectGames();

            Games = new ObservableCollection<Game>(games);
        }

        private void AddGame()
        {
        }

    }
}
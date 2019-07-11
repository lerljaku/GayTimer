using System.Windows.Input;
using GayTimer.Views;

namespace GayTimer.ViewModels
{
    public class MainPageViewModel : ScreenBase
    {
        private readonly NewGameViewModel m_newGameVm;
        private readonly PlayerListViewModel m_playerListVm;
        private readonly GameHistoryViewModel m_gamesVm;
        private readonly StatisticsViewModel m_statsVm;

        public MainPageViewModel(NewGameViewModel newGameVm, PlayerListViewModel playerListVm, GameHistoryViewModel gamesVm, StatisticsViewModel statsVm)
        {
            m_newGameVm = newGameVm;
            m_playerListVm = playerListVm;
            m_gamesVm = gamesVm;
            m_statsVm = statsVm;

            NavigateToGameCommand = new RelayCommand(NavigateToGame);
            NavigateToPlayersCommand = new RelayCommand(NavigateToPlayers);
            NavigateToHistoryCommand = new RelayCommand(NavigateToHistory);
            NavigateToStatisticsCommand = new RelayCommand(NavigateToStatistics);
        }

        public ICommand NavigateToGameCommand { get; }
        public ICommand NavigateToPlayersCommand { get; }
        public ICommand NavigateToHistoryCommand { get; }
        public ICommand NavigateToStatisticsCommand { get; }

        private async void NavigateToGame()
        {
            await App.PushAsync<NewGameView>(m_newGameVm);
        }

        private async void NavigateToPlayers()
        {
            await App.PushAsync<PlayerListView>(m_playerListVm);
        }

        private async void NavigateToHistory()
        {
            await App.PushAsync<GameHistoryView>(m_gamesVm);
        }

        private async void NavigateToStatistics(object obj)
        {
            await App.PushAsync<StatisticsView>(m_statsVm);
        }
    }
}
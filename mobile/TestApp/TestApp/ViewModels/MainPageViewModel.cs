using System.Windows.Input;
using GayTimer.Views;

namespace GayTimer.ViewModels
{
    public class MainPageViewModel : ScreenBase
    {
        private readonly NewGameViewModel m_newGameVm;
        private readonly PlayerListViewModel m_playerListVm;
        private readonly GameHistoryViewModel m_gamesVm;

        public MainPageViewModel(NewGameViewModel newGameVm, PlayerListViewModel playerListVm, GameHistoryViewModel gamesVm)
        {
            m_newGameVm = newGameVm;
            m_playerListVm = playerListVm;
            m_gamesVm = gamesVm;

            NavigateToGameCommand = new RelayCommand(NavigateToGame);
            NavigateToPlayersCommand = new RelayCommand(NavigateToPlayers);
            NavigateToHistoryCommand = new RelayCommand(NavigateToHistory);
        }

        public ICommand NavigateToGameCommand { get; }
        public ICommand NavigateToPlayersCommand { get; }
        public ICommand NavigateToHistoryCommand { get; }

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
    }
}
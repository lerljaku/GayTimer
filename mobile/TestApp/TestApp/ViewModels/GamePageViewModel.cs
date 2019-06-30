using System.Linq;
using System.Windows.Input;
using GayTimer.Views;
using Xamarin.Forms.Internals;

namespace GayTimer.ViewModels
{
    public class GamePageViewModel : ScreenBase
    {
        private GameSummaryViewModel m_gameSummaryVm;

        public GamePageViewModel(GameSummaryViewModel gameSummaryVm)
        {
            m_gameSummaryVm = gameSummaryVm;
            PassActivePlayerCommand = new RelayCommand(PassActivePlayer);
            LoadGameCommand = new RelayCommand(LoadGame);
            ExitGameCommand = new RelayCommand(ExitGame);
        }

        public ICommand LoadGameCommand { get; }
        public ICommand ExitGameCommand { get; }
        public ICommand PassActivePlayerCommand { get; }

        private PlayerViewModel[] m_allPlayers;
        public PlayerViewModel[] AllPlayers
        {
            get => m_allPlayers;
            set
            {
                AllPlayersChanging(m_allPlayers, value);
                m_allPlayers = value;
                NotifyPropertyChanged();
            }
        }

        private void AllPlayersChanging(PlayerViewModel[] oldV, PlayerViewModel[] newV)
        {
            if (oldV != null)
            {
                foreach (var player in oldV)
                {
                    player.TimerToggled -= PlayerOnTimerToggled;
                }
            }

            if (newV != null)
            {
                foreach (var player in newV)
                {
                    player.TimerToggled += PlayerOnTimerToggled;
                }
            }
        }
        
        private void PlayerOnTimerToggled(PlayerViewModel start)
        {
            foreach (var player in AllPlayers)
            {
                if(player == start)
                    continue;
                
                player.StopTimer();
            }

            if (start.IsRunning)
                start.StopTimer();
            else start.StartTimer();
        }

        private void PassActivePlayer()
        {
            var activePlayer = AllPlayers.FirstOrDefault(d => d.IsRunning) ?? AllPlayers.First();

            var index = AllPlayers.IndexOf(activePlayer);

            var newIndex = (index + 1) % AllPlayers.Length;

            var newPlayer = AllPlayers.ElementAt(newIndex);

            PlayerOnTimerToggled(newPlayer);
        }

        private void LoadGame()
        {
            throw new System.NotImplementedException();
        }

        private async void ExitGame()
        {
            m_gameSummaryVm.Init(AllPlayers);

            await App.PushAsync<GameSummaryView>(m_gameSummaryVm);
        }
    }
}

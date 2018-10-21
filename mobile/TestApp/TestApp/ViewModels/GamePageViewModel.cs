namespace GayTimer.ViewModels
{
    public class GamePageViewModel : ScreenBase
    {
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
    }
}

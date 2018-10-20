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
                m_allPlayers = value;
                NotifyPropertyChanged();
            }
        }

        public void Init()
        {
            foreach (var player in AllPlayers)
            {
                player.TimerToggled += PlayerOnTimerToggled;
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

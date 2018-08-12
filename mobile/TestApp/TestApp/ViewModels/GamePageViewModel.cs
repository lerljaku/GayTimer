namespace GayTimer.ViewModels
{
    public class GamePageViewModel : ScreenBase
    {
        public GamePageViewModel()
        {
            Init();
        }
        
        public PlayerViewModel[] AllPlayers { get; set; }

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

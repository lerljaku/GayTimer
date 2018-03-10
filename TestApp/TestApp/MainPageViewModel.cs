using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace GayTimer
{
    public class MainPageViewModel : ScreenBase
    {
        public MainPageViewModel()
        {
            Init();
        }

        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public Player Player3 { get; set; }
        public Player Player4 { get; set; }

        public List<Player> AllPlayers { get; set; }

        public void Init()
        {
            Player1 = new Player { Health = 40, TimeSpent = TimeSpan.Zero };
            Player2 = new Player { Health = 40, TimeSpent = TimeSpan.Zero };
            Player3 = new Player { Health = 40, TimeSpent = TimeSpan.Zero };
            Player4 = new Player { Health = 40, TimeSpent = TimeSpan.Zero };

            AllPlayers = new List<Player>()
            {
                Player1,
                Player2,
                Player3,
                Player4,
            };

            foreach (var player in AllPlayers)
            {
                player.TimerToggled += PlayerOnTimerToggled;
            }
        }

        private void PlayerOnTimerToggled(Player start)
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

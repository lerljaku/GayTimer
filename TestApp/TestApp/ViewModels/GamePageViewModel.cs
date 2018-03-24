using System;
using System.Collections.Generic;
using System.Net;
using GayTimer;

namespace GayTimer.ViewModels
{
    public class GamePageViewModel : ScreenBase
    {
        public GamePageViewModel()
        {
            Init();
        }

        public PlayerViewModel Player1 { get; set; }
        public PlayerViewModel Player2 { get; set; }
        public PlayerViewModel Player3 { get; set; }
        public PlayerViewModel Player4 { get; set; }

        public List<PlayerViewModel> AllPlayers { get; set; }

        public void Init()
        {
            Player1 = new PlayerViewModel { Health = 40, TimeSpent = TimeSpan.Zero };
            Player2 = new PlayerViewModel { Health = 40, TimeSpent = TimeSpan.Zero };
            Player3 = new PlayerViewModel { Health = 40, TimeSpent = TimeSpan.Zero };
            Player4 = new PlayerViewModel { Health = 40, TimeSpent = TimeSpan.Zero };

            AllPlayers = new List<PlayerViewModel>()
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

        private void TestUpload()
        {
            var rxcui = "198440";
            var request = WebRequest.Create(string.Format(@"http://rxnav.nlm.nih.gov/REST/RxTerms/rxcui/{0}/allinfo", rxcui));

        }
    }
}

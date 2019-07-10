using System;
using System.Collections.Generic;
using SQLite;

namespace GayTimer.Entities
{
    public class Game : EntityIdentityKeyBase<int>
    {
        public string Note { get; set; }

        [Ignore]
        public List<PlayerToGame> Players { get; set; }

        public DateTimeOffset Created { get; set; }
    }

    public class PlayerToGame
    {
        public int GameId { get; set; }

        public int PlayerId { get; set; }

        public TimeSpan TimeSpent { get; set; }

        public string Note { get; set; }

        public int DeckId { get; set; }

        public bool IsWinner { get; set; }
    }
}
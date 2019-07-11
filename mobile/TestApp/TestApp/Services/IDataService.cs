using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GayTimer.Entities;

namespace GayTimer.Services
{
    public interface IDataService
    {
        Task<List<Player>> SelectPlayers();
        Task<int> SaveAsync(Player player);
        Task<int> Delete(Player player);

        Task<List<Game>> SelectGames(int? playerId = null);
        Task SaveAsync(Game game);
        Task Delete(Game game);

        Task<List<Deck>> SelectDecks(int? playerId = null);
        Task<int> SaveAsync(Deck deck);
    }
}
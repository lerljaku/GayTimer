using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GayTimer.Services;
using SQLite;

namespace GayTimer.Entities
{
    public class SqlDataService : SQLiteAsyncConnection, IDataService
    {
        public SqlDataService(string databasePath) : base(databasePath)
        {
            InitTables();
        }
        
        private void InitTables()
        {
            CreateTableAsync<Player>().Wait();
            CreateTableAsync<Deck>().Wait();
            CreateTableAsync<Game>().Wait();
            CreateTableAsync<PlayerToGame>().Wait();
        }

        public Task<List<Player>> SelectPlayers()
        {
            return Table<Player>().ToListAsync();
        }

        public Task<int> Delete(Player player)
        {
            return DeleteAsync(player);
        }

        public Task<int> SaveAsync(Player player)
        {
            return player.Id == 0 ? InsertAsync(player) : UpdateAsync(player);
        }
        
        public async Task<List<Game>> SelectGames(int? playerId = null)
        {
            var games = await Table<Game>().ToListAsync();

            var query = Table<PlayerToGame>();

            if (playerId.HasValue)
                query = query.Where(d => d.PlayerId == playerId.Value);

            var players = await query.ToListAsync();

            foreach (var game in games)
            {
                game.Players = players.Where(d => d.GameId == game.Id).ToList();
            }

            return games;
        }

        public Task SaveAsync(Game game)
        {
            return RunInTransactionAsync((c) =>
            {
                if (game.Id == 0)
                {
                    c.Insert(game);

                    foreach (var playerToGame in game.Players)
                    {
                        playerToGame.GameId = game.Id;
                    }

                    c.InsertAll(game.Players);
                }
                else
                {
                    foreach (var playerToGame in game.Players)
                    {
                        playerToGame.GameId = game.Id;
                    }

                    c.Table<PlayerToGame>().Where(d => d.GameId == game.Id).Delete();
                    c.Update(game);
                    c.InsertAll(game.Players);
                }
            });
        }

        public Task Delete(Game game)
        {
            return RunInTransactionAsync((c) =>
            {
                c.Table<PlayerToGame>().Where(d => d.GameId == game.Id).Delete();
                c.Delete(game);
            });
        }
        
        public Task<List<Deck>> SelectDecks(int? playerId = null)
        {
            var query = Table<Deck>();

            if (playerId.HasValue)
                query = query.Where(d => d.PlayerId == playerId.Value);

            return query.ToListAsync();
        }

        public Task<int> SaveAsync(Deck deck)
        {
            return deck.Id == 0 ? InsertAsync(deck) : UpdateAsync(deck);
        }
    }
}
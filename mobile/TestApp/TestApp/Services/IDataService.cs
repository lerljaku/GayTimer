﻿using System;
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
        Task Insert(Player player);
        Task Update(Player player);
        Task Delete(Player player);

        Task<List<Game>> SelectGames();
        Task Insert(Game player);

        Task<List<Deck>> SelectDecks(int playerId);
        Task Insert(Deck deck);
    }

    public abstract class DataServiceBase
    {
        protected readonly ISerializerProvider Serializer;

        protected DataServiceBase(ISerializerProvider serializer)
        {
            Serializer = serializer;
        }

        protected async Task<List<TEntity>> SelectAll<TEntity>(string path)
        {
            if (File.Exists(path))
                return await Task.Run(() => Serializer.Deserialize<List<TEntity>>(File.ReadAllText(path)));

            return new List<TEntity>();
        }
    }

    public class DataService : DataServiceBase, IDataService
    {
        public DataService(ISerializerProvider serializer) : base(serializer)
        {
            var storagePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            m_playersPath = Path.Combine(storagePath, nameof(Player));
            m_gamesPath = Path.Combine(storagePath, nameof(Game));
            m_deckPath = Path.Combine(storagePath, nameof(Deck));
        }

        #region Players

        private List<Player> m_players;
        private readonly string m_playersPath;

        public async Task<List<Player>> SelectPlayers()
        {
            if (m_players != null)
                return m_players;

            return m_players = await SelectAll<Player>(m_playersPath);
        }

        public async Task Insert(Player player)
        {
            var players = await SelectPlayers();

            player.Id = players.Any() ? players.Max(d => d.Id) : 0;
            player.Id++;

            players.Add(player);

            await SavePlayers();
        }

        public async Task Update(Player player)
        {
            var toUpdate = m_players.First(d => d.Id == player.Id);
            m_players.Remove(toUpdate);
            m_players.Add(player);

            await SavePlayers();
        }

        public async Task Delete(Player player)
        {
            var toUpdate = m_players.First(d => d.Id == player.Id);
            m_players.Remove(toUpdate);

            await SavePlayers();
        }

        private Task SavePlayers()
        {
            var data = Serializer.Serialize(m_players);

            return Task.Run(() => File.WriteAllText(m_playersPath, data));
        }

        #endregion

        #region Games

        private List<Game> m_games;
        private readonly string m_gamesPath;

        public async Task<List<Game>> SelectGames()
        {
            if (m_games != null)
                return m_games;

            return m_games = await SelectAll<Game>(m_gamesPath);
        }

        public async Task Insert(Game game)
        {
            var games = await SelectGames();

            game.Id = games.Any() ? games.Max(d => d.Id) : 0;
            game.Id++;

            m_games.Add(game);

            await SaveGames();
        }

        private Task SaveGames()
        {
            var data = Serializer.Serialize(m_games);

            return Task.Run(() => File.WriteAllText(m_gamesPath, data));
        }

        #endregion

        #region Decks 

        private readonly Dictionary<int, List<Deck>> m_decks = new Dictionary<int, List<Deck>>();
        private readonly string m_deckPath;

        public async Task<List<Deck>> SelectDecks(int playerId)
        {
            if (!m_decks.ContainsKey(playerId))
            {
                m_decks.Add(playerId, null);
            }

            if (m_decks[playerId] == null)
                m_decks[playerId] = await SelectAll<Deck>(FormatDeckPath(playerId));

            return m_decks[playerId].Where(d => d.PlayerId == playerId).ToList();
        }

        public async Task Insert(Deck deck)
        {
            var playerDecks = await SelectDecks(deck.PlayerId);

            deck.Id = playerDecks.Any() ? playerDecks.Max(d => d.Id) : 0;
            deck.Id++;

            m_decks[deck.PlayerId].Add(deck);

            var data = Serializer.Serialize(m_decks[deck.PlayerId]);

            await Task.Run(() => File.WriteAllText(FormatDeckPath(deck.PlayerId), data));
        }

        private string FormatDeckPath(int playerId)
        {
            return $"{m_deckPath}_{playerId}";
        }

        #endregion
    }
}
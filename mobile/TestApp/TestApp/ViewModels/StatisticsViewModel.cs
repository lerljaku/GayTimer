using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using GayTimer.Entities;
using GayTimer.Services;
using XF.Material.Forms.UI.Dialogs;

namespace GayTimer.ViewModels
{
    public class StatisticsViewModel : ScreenBase
    {
        private readonly IDataService m_dataService;

        private static string m_defaultFrom = "All time";
        private static Dictionary<string, DateTime?> m_froms = new Dictionary<string, DateTime?>()
        {
            {"1 Month", DateTime.Now.AddMonths(-1) },
            {"3 Months", DateTime.Now.AddMonths(-3) },
            {"6 Months", DateTime.Now.AddMonths(-6) },
            {"1 Year", DateTime.Now.AddYears(-1) },
            {m_defaultFrom, null },
        };

        private List<Game> m_allGames = new List<Game>();
        private List<Deck> m_decks = new List<Deck>();

        public StatisticsViewModel(IDataService dataService)
        {
            m_dataService = dataService;

            SelectPlayerCountCommand = new RelayCommand(SelectPlayerCount);
            ClearFilterCommand = new RelayCommand(ClearFilter);
        }

        public ICommand SelectPlayerCountCommand { get; }
        public ICommand ClearFilterCommand { get; }

        public List<string> Froms => m_froms.Keys.ToList();

        private string m_selectedFrom = m_defaultFrom;
        public string SelectedFrom
        {
            get => m_selectedFrom;
            set
            {
                if (m_selectedFrom == value)
                    return;

                m_selectedFrom = value;
                NotifyPropertyChanged();
                FilterChanged();
            }
        }
        
        private List<RatingInfo> m_ratings = new List<RatingInfo>();
        public List<RatingInfo> Ratings
        {
            get => m_ratings;
            set
            {
                m_ratings = value;
                NotifyPropertyChanged();
            }
        }

        private List<Player> m_players = new List<Player>();
        public List<Player> Players
        {
            get => m_players;
            set
            {
                m_players = value;
                NotifyPropertyChanged();
            }
        }

        private Player m_selectedPlayer;
        public Player SelectedPlayer
        {
            get => m_selectedPlayer;
            set
            {
                m_selectedPlayer = value;
                NotifyPropertyChanged();
                FilterChanged();
            }
        }

        public int TotalGamesCount => m_allGames.Count;

        public double AverageRating => Ratings.Any() ? Ratings.Average(d => d.Rating) : 0;

        public List<int> PlayerCounts { get; set; } = new List<int>{ 2, 3, 4, 5 };

        private List<int> m_selectedPlayerCounts = new List<int>();
        public List<int> SelectedPlayerCounts
        {
            get => m_selectedPlayerCounts;
            set
            {
                m_selectedPlayerCounts = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(PlayerCountsStr));
                FilterChanged();
            }
        }

        public string PlayerCountsStr => SelectedPlayerCounts.Any() ? string.Join(", ", SelectedPlayerCounts.Select(d => d.ToString())) : "all"; 

        public override async void Activated()
        {
            IsBusy = true;

            Players = await m_dataService.SelectPlayers();
            m_allGames = await m_dataService.SelectGames();
            m_decks = await m_dataService.SelectDecks();

            FilterChanged();

            IsBusy = false;
        }

        private async void SelectPlayerCount()
        {
            var result = await MaterialDialog.Instance.SelectChoicesAsync(title: "Select player count", choices: PlayerCounts.Select(d => d.ToString()).ToList());
            if (result != null)
                SelectedPlayerCounts = result.Select(d => d + 2).ToList();
        }

        private void ClearFilter()
        {
            SelectedPlayer = null;
            SelectedFrom = m_defaultFrom;
            SelectedPlayerCounts = new List<int>();
        }

        private void FilterChanged()
        {
            var ratings = new Dictionary<(int, int), RatingInfo>();

            IEnumerable<Game> games = m_allGames;

            if (SelectedPlayerCounts.Any())
                games = games.Where(d => SelectedPlayerCounts.Contains(d.Players.Count));

            var from = m_froms[SelectedFrom];
            if (from != null)
                games = games.Where(d => d.Created > from.Value);

            foreach (var game in games)
            {
                foreach (var playerToGame in game.Players)
                {
                    if (SelectedPlayer != null && playerToGame.PlayerId != SelectedPlayer.Id)
                        continue;

                    if (!ratings.ContainsKey((playerToGame.DeckId, playerToGame.PlayerId)))
                    {
                        var deck = m_decks.FirstOrDefault(d => d.Id == playerToGame.DeckId)?.Name;
                        var player = m_players.FirstOrDefault(d => d.Id == playerToGame.PlayerId)?.Nick;
                        
                        ratings.Add((playerToGame.DeckId, playerToGame.PlayerId), new RatingInfo(deck, player));
                    }

                    var ratingInfo = ratings[(playerToGame.DeckId, playerToGame.PlayerId)];
                    ratingInfo.GamesCount++;
                    if (playerToGame.IsWinner)
                        ratingInfo.GamesWon++;
                }
            }

            Ratings = ratings.Values.OrderByDescending(d => d.GamesCount).ToList();

            NotifyPropertyChanged(nameof(TotalGamesCount));
        }

    }

    public class RatingInfo
    {
        public RatingInfo(string deckName, string playerName)
        {
            DeckName = deckName;
            PlayerName = playerName;
        }

        public string PlayerName { get; }
        public string DeckName { get; }
        public int GamesWon { get; set; }
        public int GamesCount { get; set; }
        public double Rating => GamesWon / (double)GamesCount;
    }
}
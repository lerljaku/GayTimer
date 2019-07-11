using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Input;
using GayTimer.Entities;
using GayTimer.Services;
using GayTimer.Views;

namespace GayTimer.ViewModels
{
    public class GameHistoryViewModel : ScreenBase
    {
        private readonly IDataService m_dataService;
        private readonly GameSummaryViewModel m_gameSummaryVm;
        private readonly ISelectPlayer m_playerSelector;

        public GameHistoryViewModel(IDataService dataService, GameSummaryViewModel gameSummaryVm, ISelectPlayer playerSelector)
        {
            m_dataService = dataService;
            m_gameSummaryVm = gameSummaryVm;
            m_playerSelector = playerSelector;

            AddGameCommand = new RelayCommand(AddGame);
            DeleteGameCommand = new RelayCommand(DeleteGame);
            GameDetailCommand = new RelayCommand(GameDetail);
            SelectPlayerCommand= new RelayCommand(SelectPlayer);
            ClearPlayerCommand = new RelayCommand(ClearPlayer);
        }

        public ICommand SelectPlayerCommand { get; }
        public ICommand ClearPlayerCommand { get; }
        public ICommand AddGameCommand { get; }
        public ICommand DeleteGameCommand { get; }
        public ICommand GameDetailCommand { get; }

        private List<GameWrapper> m_allGames;

        private ObservableCollection<GameWrapper> m_games = new ObservableCollection<GameWrapper>();
        public ObservableCollection<GameWrapper> Games
        {
            get => m_games;
            set
            {
                m_games = value;
                NotifyPropertyChanged();
            }
        }

        public bool CanClear => SelectedPlayer != null;

        public string SelectedPlayerNick => SelectedPlayer?.Nick ?? "Select Gay";

        private Player m_selectedPlayer;
        public Player SelectedPlayer
        {
            get => m_selectedPlayer;
            set
            {
                m_selectedPlayer = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(SelectedPlayerNick));
                NotifyPropertyChanged(nameof(CanClear));

                FilterGames();
            }
        }

        private void ClearPlayer()
        {
            SelectedPlayer = null;
        }

        private void FilterGames()
        {
            IEnumerable<GameWrapper> games = m_allGames;

            if (SelectedPlayer != null)
            {
                games = games.Where(d => d.Game.Players.Any(p => p.PlayerId == m_selectedPlayer.Id));
            }

            Games = new ObservableCollection<GameWrapper>(games);
        }

        public override async void Activated()
        {
            var games = await m_dataService.SelectGames();
            var decks = await m_dataService.SelectDecks();

            m_allGames = games.OrderByDescending(d => d.Created).Select(d => new GameWrapper(d, decks)).ToList();

            Games = new ObservableCollection<GameWrapper>(m_allGames);
        }

        private void AddGame()
        {
        }

        private async void DeleteGame(object arg)
        {
            var game = (GameWrapper)arg;

            await m_dataService.Delete(game.Game);

            Games.RemoveAt(Games.IndexOf(game));
        }

        private async void SelectPlayer()
        {
            if (await m_playerSelector.Start())
                SelectedPlayer = m_playerSelector.Selected;
        }

        private async void GameDetail(object arg)
        {
            var game = (GameWrapper) arg;

            m_gameSummaryVm.Init(game.Game);

            await App.PushAsync<GameSummaryView>(m_gameSummaryVm);
        }

        public class GameWrapper
        {
            public GameWrapper(Game game, IReadOnlyCollection<Deck> decks)
            {
                Game = game;
                PlayersStr = string.Join(", ", Game.Players.OrderByDescending(d => d.IsWinner).Select(d => FormatDeckName(decks, d) ?? "???"));
            }

            public Game Game { get; }

            public string CreatedStr => Game.Created.LocalDateTime.ToString("dd. MM. yyyy HH:mm");
            public string PlayersStr { get; }

            private static string FormatDeckName(IEnumerable<Deck> decks, PlayerToGame d)
            {
                var name = decks.FirstOrDefault(p => p.Id == d.DeckId)?.Name;

                return d.IsWinner ? $"w:{name}" : name;
            }
        }
    }
}
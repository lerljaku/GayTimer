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

        public GameHistoryViewModel(IDataService dataService, GameSummaryViewModel gameSummaryVm)
        {
            m_dataService = dataService;
            m_gameSummaryVm = gameSummaryVm;

            AddGameCommand = new RelayCommand(AddGame);
            DeleteGameCommand = new RelayCommand(DeleteGame);
            GameDetailCommand = new RelayCommand(GameDetail);
        }

        public ICommand AddGameCommand { get; }
        public ICommand DeleteGameCommand { get; }
        public ICommand GameDetailCommand { get; }

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

        public override async void Activated()
        {
            var games = await m_dataService.SelectGames();
            var decks = await m_dataService.SelectDecks();

            var gw = games.Select(d => new GameWrapper(d, decks)).ToList();

            Games = new ObservableCollection<GameWrapper>(gw);
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
                PlayersStr = string.Join(", ", Game.Players.Select(d => decks.FirstOrDefault(p => p.Id == d.DeckId)?.Name ?? "???"));
            }

            public Game Game { get; }

            public string CreatedStr => Game.Created.LocalDateTime.ToString("dd. MM. yyyy HH:mm");
            public string PlayersStr { get; }
        }
    }
}
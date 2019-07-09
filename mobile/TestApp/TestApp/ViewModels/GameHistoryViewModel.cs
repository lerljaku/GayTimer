using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Input;
using GayTimer.Entities;
using GayTimer.Services;

namespace GayTimer.ViewModels
{
    public class GameHistoryViewModel : ScreenBase
    {
        private readonly IDataService m_dataService;

        public GameHistoryViewModel(IDataService dataService)
        {
            m_dataService = dataService;

            AddGameCommand = new RelayCommand(AddGame);
        }

        public ICommand AddGameCommand { get; }

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

        public class GameWrapper
        {
            private readonly Game m_game;

            public GameWrapper(Game game, IReadOnlyCollection<Deck> decks)
            {
                m_game = game;
                PlayersStr = string.Join(", ", m_game.Players.Select(d => decks.FirstOrDefault(p => p.Id == d.DeckId)?.Name ?? "???"));
            }

            public string CreatedStr => m_game.Created.LocalDateTime.ToString("dd. MM. yyyy HH:mm");
            public string PlayersStr { get; }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using GayTimer.Entities;
using GayTimer.Services;
using Xamarin.Forms;
using XF.Material.Forms.UI;
using XF.Material.Forms.UI.Dialogs;

namespace GayTimer.ViewModels
{
    public class GameSummaryViewModel : ScreenBase
    {
        private readonly IDataService m_dataService;

        public GameSummaryViewModel(IDataService dataService)
        {
            m_dataService = dataService;

            SelectWinnersCommand = new RelayCommand(SelectWinners);
            SaveCommand = new RelayCommand(Save);
            DiscardCommand = new RelayCommand(Discard);
            SelectPlayerCommand = new RelayCommand(SelectPlayer);
            SelectDeckCommand = new RelayCommand(SelectDeck);
        }

        public ICommand SelectPlayerCommand { get; }
        public ICommand SelectDeckCommand { get; }
        public ICommand SelectWinnersCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand DiscardCommand { get; }

        public string Winners
        {
            get
            {
                var winnerNames = PlayerResults.Where(d => d.IsWinner).Select(d => d.Player.Nick).ToList();

                return winnerNames.Any() ? $"W: {string.Join(", ", winnerNames)}" : "Select winners";
            }
        }

        public MaterialButtonType WinnersButtonType => PlayerResults.Any(d => d.IsWinner) ? MaterialButtonType.Text : MaterialButtonType.Elevated;

        private string m_note;
        public string Note
        {
            get => m_note;
            set
            {
                m_note = value;
                NotifyPropertyChanged();
            }
        }
        
        private PlayerResult[] m_playerResults;
        public PlayerResult[] PlayerResults
        {
            get => m_playerResults;
            set
            {
                m_playerResults = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(Winners));
                NotifyPropertyChanged(nameof(WinnersButtonType));
            }
        }

        public void Init(IEnumerable<PlayerViewModel> players)
        {
            PlayerResults = players.Select((p, i) => new PlayerResult()
            {
                Player = p.Player,
                Deck = new Deck(){Name = $"Dick {i}"},
                TimeSpent = p.TimeSpent,
            }).ToArray();
        }

        private async void SelectPlayer(object obj)
        {
            var playerResult = (PlayerResult) obj;

            var players = await m_dataService.SelectPlayers();

            //Create actions
            var actions = players.Select(d => d.Nick).ToArray();

            //Show simple dialog
            var result = await MaterialDialog.Instance.SelectActionAsync(actions: actions);
            if (result >= 0)
            {
                playerResult.Player = players.ElementAt(result);
            }
        }

        private async void SelectDeck(object obj)
        {
            var playerResult = (PlayerResult)obj;

            var playerDecks = (await m_dataService.SelectDecks()).OrderByDescending(d => d.PlayerId == playerResult.Player.Id).ThenBy(d => d.Name).ToList();

            if (!playerDecks.Any())
                return;

            var result = await MaterialDialog.Instance.SelectActionAsync(actions: playerDecks.Select(pd => pd.Name).ToArray());
            if (result >= 0)
            {
                playerResult.Deck = playerDecks.ElementAt(result);
            }
        }

        private async void SelectWinners()
        {
            var playerNames = PlayerResults.Select(d => d.Player.Nick).ToList();

            var result = await MaterialDialog.Instance.SelectChoicesAsync(title: "Select winners", choices: playerNames);
            if (result == null)
                return;
            
            for (int i = 0; i < PlayerResults.Length; i++)
            {
                PlayerResults[i].IsWinner = result.Contains(i);
            }

            NotifyPropertyChanged(nameof(Winners));
            NotifyPropertyChanged(nameof(WinnersButtonType));
        }

        private async void Save()
        {
            var game = new Game()
            {
                Players = PlayerResults.Select(d => new PlayerToGame()
                {
                    DeckId = d.Deck.Id,
                    IsWinner = d.IsWinner,
                    PlayerId = d.Player.Id,
                    TimeSpent = d.TimeSpent,
                }).ToList(),
                Note = Note,
                Created = DateTimeOffset.Now,
            };

            await m_dataService.Insert(game);

            await Application.Current.MainPage.NavigationProxy.PopToRootAsync();

            await MaterialDialog.Instance.SnackbarAsync(message: "Game was saved.", actionButtonText: "Got it", msDuration: 3000);
        }

        private void Discard()
        {
            App.PopAsync();
        }

    }

    public class PlayerResult : ScreenBase
    {
        public MaterialButtonType PlayerButtonType => Player?.Id == 0 ? MaterialButtonType.Elevated : MaterialButtonType.Text;
        public MaterialButtonType DeckButtonType => Deck?.Id == 0 ? MaterialButtonType.Elevated : MaterialButtonType.Text;

        private Player m_player;
        public Player Player
        {
            get => m_player;
            set
            {
                m_player = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(PlayerButtonType));
            }
        }

        private Deck m_deck;
        public Deck Deck
        {
            get => m_deck;
            set
            {
                m_deck = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(DeckButtonType));
            }
        }

        public TimeSpan TimeSpent { get; set; }

        private bool m_isWinner;
        public bool IsWinner
        {
            get => m_isWinner;
            set
            {
                m_isWinner = value;
                NotifyPropertyChanged();
            }
        }
    }
}
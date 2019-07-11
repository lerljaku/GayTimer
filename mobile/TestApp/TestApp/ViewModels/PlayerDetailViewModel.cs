using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GayTimer.Entities;
using GayTimer.Events;
using GayTimer.Services;
using GayTimer.Views;
using Xamarin.Forms;

namespace GayTimer.ViewModels
{
    public class PlayerDetailViewModel : ScreenBase
    {
        private Player m_player;
        private readonly IDataService m_dataService;
        private readonly PlayerDecksViewModel m_playerDecksVm;

        public PlayerDetailViewModel(IDataService dataService, PlayerDecksViewModel playerDecksVm)
        {
            m_dataService = dataService;
            m_playerDecksVm = playerDecksVm;

            SaveCommand = new RelayCommand(Save);
            DiscardCommand = new RelayCommand(Discard);
            NavigateToDecksCommand = new RelayCommand(NavigateToDecks);

            MessagingCenter.Subscribe<DeckDetailViewModel, DeckInserted>(this, nameof(DeckInserted), DeckInserted);
        }

        public ICommand DiscardCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand NavigateToDecksCommand { get; }

        private ObservableCollection<Deck> m_decks = new ObservableCollection<Deck>();
        public ObservableCollection<Deck> Decks
        {
            get => m_decks;
            set
            {
                m_decks = value;
                NotifyPropertyChanged();
            }
        }

        private string m_nick;
        public string Nick
        {
            get => m_nick;
            set
            {
                m_nick = value;
                NotifyPropertyChanged();
            }
        }

        private string m_color;
        public string Color
        {
            get => m_color;
            set
            {
                m_color = value;
                NotifyPropertyChanged();
            }
        }

        public async void Init(Player player)
        {
            m_player = player;

            Nick = m_player.Nick;

            Title = string.IsNullOrWhiteSpace(player.Nick) ? $"New user" : $"User - {player.Nick}";

            if (player.Id != 0)
            {
                var decks = await m_dataService.SelectDecks(player.Id);

                Decks = new ObservableCollection<Deck>(decks.OrderByDescending(d => d.Id).Take(3));
            }
            else Decks = new ObservableCollection<Deck>();
        }

        private async void NavigateToDecks()
        {
            if (m_player.Id == 0)
                await m_dataService.SaveAsync(m_player);
            
            m_playerDecksVm.Init(m_player);

            await App.PushAsync<PlayerDecksView>(m_playerDecksVm);
        }

        private async void Save()
        {
            m_player.Nick = Nick;

            await m_dataService.SaveAsync(m_player);

            await App.PopAsync();

            MessagingCenter.Send(this, nameof(PlayerInserted), m_player);
        }

        private void DeckInserted(DeckDetailViewModel senderVm, DeckInserted deckArg)
        {
            Decks.Add(deckArg.Deck);

            Decks = new ObservableCollection<Deck>(Decks);//notifikace
        }

        private void Discard()
        {
            App.PopAsync();
        }
    }
}
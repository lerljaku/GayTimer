using System;
using System.Collections.Generic;
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
        }

        public ICommand DiscardCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand NavigateToDecksCommand { get; }

        private List<Deck> m_decks = new List<Deck>();
        public List<Deck> Decks
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

        public void Init(Player player)
        {
            m_player = player;

            Nick = m_player.Nick;

            Title = string.IsNullOrWhiteSpace(player.Nick) ? $"New user" : $"User - {player.Nick}";
        }

        private async void NavigateToDecks()
        {
            m_playerDecksVm.Init(m_player);

            await App.PushAsync<PlayerDecksView>(m_playerDecksVm);
        }

        private void Save()
        {
            m_player.Nick = Nick;

            m_dataService.Insert(m_player);

            App.PopAsync();

            MessagingCenter.Send(this, nameof(PlayerInserted), m_player);
        }

        private void Discard()
        {
            App.PopAsync();
        }
    }
}